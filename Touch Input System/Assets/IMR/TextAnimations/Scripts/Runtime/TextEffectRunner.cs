using System;
using UnityEngine;
using System.Collections.Generic;

namespace IMR.TextAnimations.Scripts.Runtime
{
    public class TextEffectRunner : MonoBehaviour
    {
        private readonly List<ITextEffect> _effects = new List<ITextEffect>();

        public static TextEffectRunner Instance;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }

        public void PlayEffect(ITextEffect effect)
        {
            
            effect.StartEffect();
            _effects.Add(effect);
        }

        private void Update()
        {
            float dt = Time.deltaTime;

            for (int i = _effects.Count - 1; i >= 0; i--)
            {
                var e = _effects[i];
                if (e.IsComplete)
                {
                    _effects.RemoveAt(i);
                    continue;
                }

                e.UpdateEffect(dt);
            }
        }
    }
}