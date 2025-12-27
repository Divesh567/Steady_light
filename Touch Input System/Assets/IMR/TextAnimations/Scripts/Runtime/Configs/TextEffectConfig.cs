using IMR.TextAnimations.Scripts.Runtime;
using TMPro;
using UnityEngine;

public abstract class TextEffectConfig : ScriptableObject
{
    public abstract ITextEffect CreateEffect(TMP_Text textComponent, string fullText);
}