using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeEffectWin : SequenceStepBase
{
    [Header("Volume Settings")]
    [SerializeField] private Volume globalVolume;
    [SerializeField] private float transitionDuration = 1f;

    [Header("Time Settings")]
    [SerializeField] private float slowTimeScale = 0.5f;
    [SerializeField] private float slowDuration = 2f;

    public override async UniTask Execute()
    {
        if (globalVolume == null)
        {
            Debug.LogWarning("Global Volume not assigned.");
            return;
        }

        // Save original values
        float originalTimeScale = Time.timeScale;
        float originalWeight = globalVolume.weight;

        // Step 1: Slow down time
        Time.timeScale = slowTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        // Step 2: Fade in volume weight (0 → 1)
        float t = 0f;
        while (t < transitionDuration)
        {
            t += Time.unscaledDeltaTime;
            float lerpT = Mathf.Clamp01(t / transitionDuration);
            globalVolume.weight = Mathf.Lerp(originalWeight, 1f, lerpT);
            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        globalVolume.weight = 1f;

        // Step 3: Wait during full slow effect
        await UniTask.Delay((int)(slowDuration * 1000), DelayType.UnscaledDeltaTime);


        // Step 5: Restore time scale
        Time.timeScale = originalTimeScale;
        Time.fixedDeltaTime = 0.02f;
    }
}
