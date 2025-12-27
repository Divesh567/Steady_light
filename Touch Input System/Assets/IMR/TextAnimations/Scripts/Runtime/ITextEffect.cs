using TMPro;
using UnityEngine;

namespace IMR.TextAnimations.Scripts.Runtime
{
    public interface ITextEffect
    {
        bool IsComplete { get; }
        void StartEffect();
        void UpdateEffect(float deltaTime);
        void StopEffect();
    }
}