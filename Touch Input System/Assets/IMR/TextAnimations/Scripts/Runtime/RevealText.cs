using TMPro;
using UnityEngine;

namespace IMR.TextAnimations.Scripts.Runtime
{
    public class RevealText : MonoBehaviour
    {
        [TextArea]
        public string textToReveal;
        public TMP_Text text;
        public TextEffectConfig textEffectConfig;

        [ContextMenu("Reveal")]
        public void PlayEffect()
        {
            var effect = textEffectConfig.CreateEffect(text, textToReveal);
            TextEffectRunner.Instance.PlayEffect(effect);
        }
        

       
    }
}