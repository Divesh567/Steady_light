using System.Collections;
using TMPro;
using UnityEngine;

namespace IMR.TextAnimations.Scripts.Runtime
{
    public class SlideRevealTextEffect : ITextEffect
    {
        private TMP_Text _textComponent;
        private string _fullText;
        private float _charDelay;
        private float _slideDuration;
        private float _slideDistance;
        private bool _alternateSides;

        private TMP_TextInfo _textInfo;
        private float[] _revealTimes;
        private Vector3[][] _originalVertices;

        private bool _started;
        private bool _complete;

        public bool IsComplete => _complete;

        public SlideRevealTextEffect(
            TMP_Text textComponent,
            string fullText,
            float charDelay = 0.05f,
            float slideDuration = 0.3f,
            float slideDistance = 20f,
            bool alternateSides = true)
        {
            _textComponent = textComponent;
            _fullText = fullText;
            _charDelay = charDelay;
            _slideDuration = slideDuration;
            _slideDistance = slideDistance;
            _alternateSides = alternateSides;
        }

        public void Setup(TMP_Text text)
        {
            _textComponent = text;
            _textComponent.text = _fullText;
            _textComponent.ForceMeshUpdate();

            _textInfo = _textComponent.textInfo;
            _revealTimes = new float[_textInfo.characterCount];

            // Store original vertices
            _originalVertices = new Vector3[_textInfo.meshInfo.Length][];
            for (int i = 0; i < _textInfo.meshInfo.Length; i++)
            {
                var verts = _textInfo.meshInfo[i].vertices;
                _originalVertices[i] = new Vector3[verts.Length];
                System.Array.Copy(verts, _originalVertices[i], verts.Length);
            }

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

            // Wait for the last character's animation to finish
            yield return new WaitForSeconds(_slideDuration);
            _complete = true;
        }

        public void UpdateEffect(float deltaTime)
        {
            if (!_started || _complete) return;

            _textInfo = _textComponent.textInfo;

            for (int i = 0; i < _textInfo.characterCount; i++)
                AnimateCharacter(i);

            // Apply all geometry changes
            for (int i = 0; i < _textInfo.meshInfo.Length; i++)
            {
                var meshInfo = _textInfo.meshInfo[i];
                meshInfo.mesh.vertices = meshInfo.vertices;
                meshInfo.mesh.colors32 = meshInfo.colors32;
                _textComponent.UpdateGeometry(meshInfo.mesh, i);
            }
        }

        private void AnimateCharacter(int index)
        {
            if (!_textInfo.characterInfo[index].isVisible) return;

            int matIndex = _textInfo.characterInfo[index].materialReferenceIndex;
            int vertIndex = _textInfo.characterInfo[index].vertexIndex;
            Vector3[] verts = _textInfo.meshInfo[matIndex].vertices;
            Color32[] colors = _textInfo.meshInfo[matIndex].colors32;

            if (_revealTimes[index] == float.MaxValue)
            {
                for (int j = 0; j < 4; j++)
                    colors[vertIndex + j].a = 0;
                return;
            }

            float elapsed = Time.time - _revealTimes[index];
            float t = Mathf.Clamp01(elapsed / _slideDuration);

            float easedT = 1 - Mathf.Pow(1 - t, 3);
            float dir = (_alternateSides && index % 2 == 1) ? 1f : -1f;

            Vector3 offset = new Vector3(Mathf.Lerp(_slideDistance * dir, 0, easedT), 0, 0);

            // Reset to original position before applying offset
            for (int j = 0; j < 4; j++)
            {
                verts[vertIndex + j] = _originalVertices[matIndex][vertIndex + j] + offset;
                colors[vertIndex + j].a = (byte)Mathf.Lerp(0, 255, t);
            }
        }

        public void StopEffect()
        {
            _complete = true;
        }
    }
}
