using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using Random = UnityEngine.Random;

public class FaceRenderer : MonoBehaviour
{
    [SerializeField] private FaceAnimation faceAnimation;
    [SerializeField] private FaceGenConfig config;

    [SerializeField] private SpriteRenderer baseRenderer;
    [SerializeField] private SpriteRenderer eyesRenderer;
    [SerializeField] private SpriteRenderer mouthRenderer;

    private FaceSet faceSet;
    private CancellationTokenSource blinkCTS;

    private void Awake()
    {
        faceAnimation.OnStateChange += UpdateMouthSprite;
        GenerateFace();
    }

    private void OnEnable() => StartBlinkLoop().Forget();
    private void OnDisable() => StopBlinkLoop();

    public void GenerateFace()
    {
        faceSet = config.GenerateFace();
        ApplyFace();
    }

    private void ApplyFace()
    {
        baseRenderer.sprite = faceSet.baseColor;
        eyesRenderer.sprite = faceSet.eye.open;
        mouthRenderer.sprite = faceSet.mouth.normal;
    }

    private void UpdateMouthSprite(BallState state)
    {
        mouthRenderer.sprite = state switch
        {
            BallState.Normal => faceSet.mouth.normal,
            BallState.Smile => faceSet.mouth.smile,
            BallState.Angry => faceSet.mouth.angry,
            _ => faceSet.mouth.normal
        };
    }

    private async UniTaskVoid StartBlinkLoop()
    {
        StopBlinkLoop();
        blinkCTS = new CancellationTokenSource();

        try
        {
            while (!blinkCTS.Token.IsCancellationRequested)
            {
                await UniTask.Delay(Random.Range(1500, 3500), cancellationToken: blinkCTS.Token);
                eyesRenderer.sprite = faceSet.eye.blink;
                await UniTask.Delay(150, cancellationToken: blinkCTS.Token);
                eyesRenderer.sprite = faceSet.eye.open;
            }
        }
        catch (OperationCanceledException) { }
    }

    private void StopBlinkLoop()
    {
        blinkCTS?.Cancel();
        blinkCTS?.Dispose();
        blinkCTS = null;
    }

   
    // === Visibility Control ===
    public void DisableFace()
    {
        baseRenderer.enabled = false;
        mouthRenderer.enabled = false;
        eyesRenderer.enabled = false;
    }

    public void EnableFace()
    {
        baseRenderer.enabled = true;
        mouthRenderer.enabled = true;
        eyesRenderer.enabled = true;
    }
    
}