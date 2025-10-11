using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class ProgressAnimationController : SequenceStepBase
{
    public List<ProgressAnimationModel> worldSprites;
    public SpriteRenderer progressionSprite;

    public UnityAction startAction = null;
    public UnityAction endAction = null;

    public Material _progressMat;
    public float duration;

    [ColorUsage(true, true)]
    public Color startGlowIntensity;

    [ColorUsage(true, true)]
    public Color endGlowIntensity;


    public override void Start()
    {
        base.Start();
       
    }
    public float GetWorldProgress()
    {
        var world = LevelLoader.Instance.levelHolder.worldSO.Find(x => x.worldType == RuntimeGameData.worldType);
        int total = world.levels.Count;
        int currentIndex = world.levels.FindIndex(x => x.sceneName == RuntimeGameData.levelSelectedName) + 1;
        return (float)currentIndex / total;
    }

    public float GetPreviousProgress()
    {
        var world = LevelLoader.Instance.levelHolder.worldSO.Find(x => x.worldType == RuntimeGameData.worldType);
        int total = world.levels.Count;
        int currentIndex = world.levels.FindIndex(x => x.sceneName == RuntimeGameData.levelSelectedName);
        return (float)currentIndex / total;
    }

    public void SetProgressSprite()
    {

        float previousProgress = GetPreviousProgress();

        int worldIndex = LevelLoader.Instance.levelHolder.worldSO.FindIndex(x => x.worldType == RuntimeGameData.worldType);
        var spriteData = worldSprites[worldIndex];

        progressionSprite.sprite = spriteData.sprite;

        startGlowIntensity = spriteData.startGlowIntensity;
        endGlowIntensity = spriteData.endGlowIntensity;

        _progressMat.SetFloat("_FillAmount", previousProgress);
        _progressMat.SetColor("_Color", startGlowIntensity);
        _progressMat.SetTexture("_MainTex", spriteData.sprite.texture);

    }

    public void ShowWorldProgression(UnityAction startAction, UnityAction endAction)
    {
        this.startAction = startAction;
        this.endAction = endAction;

        float previousProgress = GetPreviousProgress();
        float currentProgress = GetWorldProgress();

        int worldIndex = LevelLoader.Instance.levelHolder.worldSO.FindIndex(x => x.worldType == RuntimeGameData.worldType);
        var spriteData = worldSprites[worldIndex];

        progressionSprite.sprite = spriteData.sprite;

        if (_progressMat != null)
        {
            _progressMat.SetFloat("_FillAmount", previousProgress);

            DOTween.To(() => previousProgress, x =>
            {
                _progressMat.SetFloat("_FillAmount", x);
            }, currentProgress, 1f).OnComplete(() =>
            {
                endAction?.Invoke();
            });
        }
    }

    public async UniTask Animate()
    {
        SetProgressSprite();

        float from = GetPreviousProgress();
        float to = GetWorldProgress();

        Debug.Log($"Previous progress was {from}, Current progress is {to}");

        var tcs = new UniTaskCompletionSource();

        DOTween.To(() => from, x => _progressMat.SetFloat("_Cutoff", x), to, duration)
            .SetEase(Ease.Linear)
            .OnComplete
            (
                () => tcs.TrySetResult()
            );

        await tcs.Task;

        _progressMat.SetFloat("_Cutoff", to);
        _progressMat.DOColor(endGlowIntensity, "_Color", duration);

        MenuManager.Instance.OpenMenu(WinScreen.Instance);
        MenuManager.Instance.CloseMenu(GameMenu.Instance);
    }

    public async override UniTask Execute()
    {
        await Animate();
    }
}

[System.Serializable]
public class ProgressAnimationModel
{
    public Sprite sprite;
    [ColorUsage(true, true)]
    public Color startGlowIntensity;

    [ColorUsage(true, true)]
    public Color endGlowIntensity;
}


