using UnityEngine;
using DG.Tweening;

public class GameSpeedController : MonoBehaviour
{
    public IntEventChannel OnStateChangeEventTrigger;

    private Tween timeTween;
    private Tween pitchTween;

    [SerializeField] private float transitionDuration = 0.5f;

    private void Awake()
    {
        OnStateChangeEventTrigger.OnEventRaised += SetGameSpeed;
    }

    void SetGameSpeed(int state)
    {
        float targetTimeScale = (state == 2) ? 0.4f : 1f;
        float targetPitch = (state == 2) ? 0.6f : 1f;

        AnimateSpeed(targetTimeScale, targetPitch);
    }

    void AnimateSpeed(float targetTimeScale, float targetPitch)
    {
        // Kill previous tweens so mid-transition changes are smooth
        timeTween?.Kill();
        pitchTween?.Kill();

        // Time scale tween
        timeTween = DOTween.To(
            () => Time.timeScale,
            x => Time.timeScale = x,
            targetTimeScale,
            transitionDuration
        ).SetEase(Ease.OutQuad).SetUpdate(true); // important: ignore timescale

        // Audio pitch tween
        pitchTween = DOTween.To(
            () => SoundManager.Instance.GetMusicPitch(),
            x => SoundManager.Instance.SetMusicPitch(x),
            targetPitch,
            transitionDuration
        ).SetEase(Ease.OutQuad);
    }
}