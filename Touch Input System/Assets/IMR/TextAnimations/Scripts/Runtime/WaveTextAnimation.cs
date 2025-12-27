using UnityEngine;
using TMPro;
using System.Collections;
using IMR.TextAnimations.Scripts.Runtime;

using UnityEngine;
using TMPro;
using System.Collections;

public class WaveTextEffect : ITextEffect
{
    private TMP_Text textComponent;
    private string fullText;
    private float charDelay;
    private float waveSpeed;
    private float waveHeight;
    private MonoBehaviour runner;

    private TMP_TextInfo textInfo;
    private float[] revealTimes;
    private bool isStarted;
    private bool isComplete;

    public void Setup(TMP_Text text)
    {
        StartEffect();
        //throw new System.NotImplementedException();
    }

    public void AnimateCharacter(int index, TMP_TextInfo textInfo, float[] revealTimes)
    {
       // throw new System.NotImplementedException();
    }

    public bool IsComplete => isComplete;

    public WaveTextEffect(MonoBehaviour runner, TMP_Text textComponent, string fullText, float charDelay, float waveSpeed, float waveHeight)
    {
        this.runner = runner;
        this.textComponent = textComponent;
        this.fullText = fullText;
        this.charDelay = charDelay;
        this.waveSpeed = waveSpeed;
        this.waveHeight = waveHeight;
    }

    public void StartEffect()
    {
        textComponent.text = fullText;
        textComponent.ForceMeshUpdate();

        textInfo = textComponent.textInfo;
        revealTimes = new float[textInfo.characterCount];

        for (int i = 0; i < revealTimes.Length; i++)
            revealTimes[i] = float.MaxValue;

        runner.StartCoroutine(RevealCoroutine());
        isStarted = true;
    }

    private IEnumerator RevealCoroutine()
    {
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible) continue;
            revealTimes[i] = Time.time;
            yield return new WaitForSeconds(charDelay);
        }

        isComplete = true;
    }

    public void UpdateEffect(float deltaTime)
    {
        if (!isStarted) return;

        textComponent.ForceMeshUpdate();
        textInfo = textComponent.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible || revealTimes[i] == float.MaxValue)
                continue;

            int matIndex = textInfo.characterInfo[i].materialReferenceIndex;
            int vertIndex = textInfo.characterInfo[i].vertexIndex;

            Vector3[] verts = textInfo.meshInfo[matIndex].vertices;
            float wave = Mathf.Sin(Time.time * waveSpeed + i * 0.2f) * waveHeight;
            Vector3 offset = new Vector3(0, wave, 0);

            verts[vertIndex + 0] += offset;
            verts[vertIndex + 1] += offset;
            verts[vertIndex + 2] += offset;
            verts[vertIndex + 3] += offset;
        }

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }

    public void StopEffect()
    {
        isComplete = true;
    }
}

