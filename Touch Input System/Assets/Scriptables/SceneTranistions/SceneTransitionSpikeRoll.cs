using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SceneTransitionSpikeRoll", menuName = "SceneTransitions/SceneTransitionSpikeRoll")]
public class SceneTransitionSpikeRoll : SceneTransitionBaseSO
{
    public Transform transform;
    public float scaleInDuration;
    public float rotDuration;
    public float scaleSize;
    public Vector3 rotValue;



    private string ButtonName = "Anim Test Button";


    [Button("$ButtonName")]
    private void TestAction()
    {
        transform = SceneTransitionManager.Instance.TestAnimation().GetComponent<Transform>();

        transform.DOScale(0, 0).OnComplete(() =>
        {
            transform.DOScale(3, scaleInDuration).OnComplete(() =>
            {
                transform.DOScale(0, scaleInDuration);
            });

        });

    }

    public override void PlayAnimationPart1(GameObject target, UnityAction onCompleteAction)
    {
        transform = target.GetComponent<RectTransform>();
        transform.DOScale(0, 0).OnComplete(() =>
        {
            Sequence scaleAndRotate = DOTween.Sequence();

            AudioControllerMono audioController = target.GetComponent<AudioControllerMono>();
            audioController.PlayAudioClip(transitionClip1);

            scaleAndRotate
                .Join(transform.DOScale(scaleSize, scaleInDuration))
                .Join(transform.DORotate(rotValue, scaleInDuration, RotateMode.FastBeyond360))
                .OnComplete(() =>
                {
                    onCompleteAction?.Invoke();
                });
        });

    }

    public override void PlayAnimationPart2(GameObject target)
    {
        Debug.Log("Scene is loaded completing trasition");

        transform.DOScale(scaleSize, 0).OnComplete(() =>
        {
            Sequence scaleAndRotate = DOTween.Sequence();

            AudioControllerMono audioController = target.GetComponent<AudioControllerMono>();
            audioController.PlayAudioClip(transitionClip2);

            scaleAndRotate
                .Join(transform.DOScale(0, scaleInDuration))
                .Join(transform.DORotate(-rotValue, scaleInDuration, RotateMode.FastBeyond360))
                .OnComplete(() => OnAnimComplete());

            transform.DOScale(0, scaleInDuration).OnComplete(() => OnAnimComplete());
        });
      
    }

    public override void OnSceneLoaded()
    {
        PlayAnimationPart2(transform.gameObject);
    }
}
