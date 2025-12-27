using TMPro;
using UnityEngine;

namespace IMR.TextAnimations.Scripts.Runtime.Configs
{
    [CreateAssetMenu(fileName = "SlideRevealConfig", menuName = "Text Effects/Slide Effect", order = 0)]
    public class SlideTextRevealConfig : TextEffectConfig
    {
        [Header("Reveal Settings")]
        public float _charDelay;
        public float _slideDuration;
        public float _slideDistance;
        public bool _alternateSides;
        
        public override ITextEffect CreateEffect(TMP_Text textComponent, string fullText)
        {
            return new SlideRevealTextEffect(textComponent, fullText, _charDelay, _slideDuration, _slideDistance, _alternateSides);
        }
    }
}