using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SceneTransitionSlideInOut", menuName = "SceneTransitions/SceneTransitionSlideInOut")]
public class SceneTransitionSlideInOut : SceneTransitionBaseSO
{
    public RectTransform rectTransform;
    public float SlideDuration;
    public Vector2 startPos;
    public Vector2 inPos;
    public Vector2 outPos;

    private string ButtonName = "Anim Test Button";


    [Button("$ButtonName")]
    private void TestAction()
    {
        rectTransform = SceneTransitionManager.Instance.TestAnimation().GetComponent<RectTransform>();

        rectTransform.DOAnchorPos(startPos, 0).OnComplete(() =>
        {
            rectTransform.DOAnchorPos(inPos, SlideDuration).OnComplete(() =>
            {
                rectTransform.DOAnchorPos(outPos, SlideDuration);
            });

        });

    }

    public override void PlayAnimationPart1(GameObject target, UnityAction onCompleteAction)
    {
        rectTransform = target.GetComponent<RectTransform>();


        rectTransform.anchoredPosition = startPos;

        AudioControllerMono audioController = target.GetComponent<AudioControllerMono>();
        audioController.PlayAudioClip(transitionClip1);
        rectTransform.DOAnchorPos(inPos, SlideDuration).OnComplete( () => 
        {
            onCompleteAction.Invoke();
        });
    }

    public override void PlayAnimationPart2(GameObject target)
    {
     

        AudioControllerMono audioController = target.GetComponent<AudioControllerMono>();
        audioController.PlayAudioClip(transitionClip2);

        rectTransform.anchoredPosition = inPos;
        rectTransform.DOAnchorPos(outPos, SlideDuration).OnComplete(() => 
        {
            OnAnimComplete();
        });
    }

    public override void OnSceneLoaded()
    {
        PlayAnimationPart2(rectTransform.gameObject);
    }

}