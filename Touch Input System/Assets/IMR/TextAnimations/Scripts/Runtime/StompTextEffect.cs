using System.Collections;
using TMPro;
using UnityEngine;

namespace IMR.TextAnimations.Scripts.Runtime
{
    public class StompTextEffect : ITextEffect
    {
        private TMP_Text _textComponent;
        private string _fullText;
        private float _charDelay;
        private float _stompDuration;
        private float _startScale;

        private TMP_TextInfo _textInfo;
        private float[] _revealTimes;

        private bool _started;
        private bool _complete;

        public bool IsComplete => _complete;

        public StompTextEffect( TMP_Text textComponent, string fullText, float charDelay, float stompDuration, float startScale)
        {
            _textComponent = textComponent;
            _fullText = fullText;
            _charDelay = charDelay;
            _stompDuration = stompDuration;
            _startScale = startScale;
        }

        public void Setup(TMP_Text text)
        {
            _textComponent = text;
            _textComponent.text = _fullText;
            _textComponent.ForceMeshUpdate();

            _textInfo = _textComponent.textInfo;
            _revealTimes = new float[_textInfo.characterCount];

            for (int i = 0; i < _revealTimes.Length; i++)
                _revealTimes[i] = float.MaxValue;
        }

        public void StartEffect()
        {
            Setup(_textComponent);
            TextEffectRunner.Instance.StartCoroutine(RevealCoroutine());
            _started = true;
        }

        private IEnumerator RevealCoroutine()
        {
            for (int i = 0; i < _textInfo.characterCount; i++)
            {
                if (!_textInfo.characterInfo[i].isVisible) continue;
                _revealTimes[i] = Time.time;
                yield return new WaitForSeconds(_charDelay);
            }

            _complete = true;
        }

        public void UpdateEffect(float deltaTime)
        {
            if (!_started) return;

            _textComponent.ForceMeshUpdate();
            _textInfo = _textComponent.textInfo;

            for (int i = 0; i < _textInfo.characterCount; i++)
                AnimateCharacter(i, _textInfo, _revealTimes);

            for (int i = 0; i < _textInfo.meshInfo.Length; i++)
            {
                var meshInfo = _textInfo.meshInfo[i];
                meshInfo.mesh.vertices = meshInfo.vertices;
                meshInfo.mesh.colors32 = meshInfo.colors32;
                _textComponent.UpdateGeometry(meshInfo.mesh, i);
            }
        }

        public void AnimateCharacter(int index, TMP_TextInfo textInfo, float[] revealTimes)
        {
            if (!textInfo.characterInfo[index].isVisible) return;

            int matIndex = textInfo.characterInfo[index].materialReferenceIndex;
            int vertIndex = textInfo.characterInfo[index].vertexIndex;

            Vector3[] verts = textInfo.meshInfo[matIndex].vertices;
            Color32[] colors = textInfo.meshInfo[matIndex].colors32;

            if (revealTimes[index] == float.MaxValue)
            {
                // Hide unrevealed chars
                for (int j = 0; j < 4; j++)
                    colors[vertIndex + j].a = 0;
                return;
            }

            float elapsed = Time.time - revealTimes[index];
            float t = Mathf.Clamp01(elapsed / _stompDuration);
            float scale = Mathf.Lerp(_startScale, 1f, Mathf.SmoothStep(0, 1, t));

            Vector2 charMid = (verts[vertIndex + 0] + verts[vertIndex + 2]) / 2f;

            for (int j = 0; j < 4; j++)
            {
                verts[vertIndex + j] = (verts[vertIndex + j] - (Vector3)charMid) * scale + (Vector3)charMid;
                byte alpha = (byte)Mathf.Lerp(0, 255, t);
                colors[vertIndex + j].a = alpha;
            }
        }

        public void StopEffect()
        {
            _complete = true;
        }
    }
}
