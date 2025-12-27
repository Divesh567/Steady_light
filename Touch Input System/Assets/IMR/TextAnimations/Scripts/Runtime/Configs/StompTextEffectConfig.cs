using IMR.TextAnimations.Scripts.Runtime;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "StompTextEffectConfig", menuName = "Text Effects/Stomp Effect")]
public class StompTextEffectConfig : TextEffectConfig
{
    [Header("Stomp Settings")]
    public float charDelay = 0.05f;
    public float stompDuration = 0.3f;
    public float startScale = 1.5f;

    public override ITextEffect CreateEffect( TMP_Text textComponent, string fullText)
    {
        return new StompTextEffect(textComponent, fullText, charDelay, stompDuration, startScale);
    }
}