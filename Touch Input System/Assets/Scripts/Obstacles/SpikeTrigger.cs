using UnityEngine;
using DG.Tweening;

public class SpikeTrigger : MonoBehaviour
{
    [SerializeField]
    private Transform spikeTransform;

    public Vector2 spikeInitalOffset;
    public Vector2 spikeOriginalPos;
    public float duration;
    public Ease ease = Ease.OutBack;

    private void Start()
    {
        spikeOriginalPos = spikeTransform.localPosition;
        SetSpikeInitalPos();
    }
    void SetSpikeInitalPos()
    {
        spikeTransform.transform.localPosition = spikeOriginalPos - spikeInitalOffset;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
            TriggerSpike();
    }

    public void TriggerSpike()
    {
        spikeTransform.DOLocalMove(spikeOriginalPos, duration).SetEase(ease);
    }
}
