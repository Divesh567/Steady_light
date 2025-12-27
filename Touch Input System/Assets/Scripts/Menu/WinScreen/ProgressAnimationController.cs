using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class ProgressAnimationController : SequenceStepBase
{
    public List<ProgressAnimationModel> worldSprites;
    public SpriteRenderer progressionSprite;
    public SpriteRenderer bgSprite;

    public UnityAction startAction = null;
    public UnityAction endAction = null;

    public Material _progressMat;
    public float duration;

    [ColorUsage(true, true)]
    public Color startGlowIntensity;

    [ColorUsage(true, true)]
    public Color endGlowIntensity;

    public DrawLine lineDraw;


    public override void Start()
    {
        base.Start();
        bgSprite.gameObject.SetActive(false);
        progressionSprite.gameObject.SetActive(false);
    }



    
    private void EnableBG()
    {
        bgSprite.gameObject.SetActive(true);
    }
    public void SetProgressSprite()
    {

        float previousProgress = 0;

        int worldIndex = LevelLoader.Instance.levelHolder.currentWorldSO.FindIndex(x => x.worldType == RuntimeGameData.worldType);
        var spriteData = worldSprites[worldIndex];

        progressionSprite.sprite = spriteData.sprite;
        progressionSprite.gameObject.SetActive(true);
        
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

        float previousProgress = 0;
        float currentProgress = 1;

        int worldIndex = LevelLoader.Instance.levelHolder.currentWorldSO.FindIndex(x => x.worldType == RuntimeGameData.worldType);
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
        CameraUtilities.SetTransfromPosition(transform);
        EnableBG();
        SetProgressSprite();

        float from = 0;
        float to = 1;

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
        bgSprite.gameObject.SetActive(true);
        await lineDraw.Execute();
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


