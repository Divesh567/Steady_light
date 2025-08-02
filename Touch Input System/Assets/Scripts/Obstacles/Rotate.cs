using DG.Tweening;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    [SerializeField]
    private float delay;
    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private Vector3 RotationValue;

    public Ease ease;

    public AudioControllerMono audioController;
    private Renderer _renderer;

    private void Start()
    {
        RotateSpikeAnchor();
        _renderer = GetComponent<Renderer>();
    }

    private void RotateSpikeAnchor()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendCallback(() => PlayRotateSfx());

        sequence.Append(transform.DOLocalRotate(RotationValue, rotationSpeed)
        .SetEase(ease).SetRelative());

        sequence.AppendInterval(delay); // Apply delay between actions

        sequence.AppendCallback(() => PlayRotateSfx());

        sequence.Append(transform.DOLocalRotate(-RotationValue, rotationSpeed)
        .SetEase(ease).SetRelative());

        sequence.AppendInterval(delay); // Apply delay between actions

        sequence.SetLoops(-1, LoopType.Restart);
        sequence.Play();

    }

    private void PlayRotateSfx()
    {
        if (_renderer.isVisible)
            audioController.PlayAudioClip();
    }

}
