using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "SceneTransitionSlideInOut", menuName = "SceneTransitions/SceneTransitionSlideInOut")]
public class SceneTransitionSlideInOut : SceneTransitionBaseSO
{
    public RectTransform rectTransform;
    public float SlideDuration;


    private string ButtonName = "Anim Test Button";


    [Button("$ButtonName")]
    private void TestAction()
    {
        rectTransform = SceneTransitionManager.Instance.TestAnimation().GetComponent<RectTransform>();

        rectTransform.DOAnchorPosX(1920, 0).OnComplete(() =>
        {
            rectTransform.DOAnchorPosX(0, SlideDuration).OnComplete(() =>
            {
                rectTransform.DOAnchorPosX(-1920, SlideDuration);
            });

        });

    }

    public override void PlayAnimationPart1(GameObject target)
    {
        rectTransform = target.GetComponent<RectTransform>();

        rectTransform.anchoredPosition = new Vector2(1920, 0);

        rectTransform.DOAnchorPosX(0, SlideDuration).OnComplete( () => 
        {
            LevelLoader.Instance.LoadNextLevel();
        });
    }

    public override void PlayAnimationPart2(GameObject target)
    {
        Debug.Log("Scene is loaded completing trasition");
        rectTransform.anchoredPosition = new Vector2(0, 0);
        rectTransform.DOAnchorPosX(-1920, SlideDuration);
    }

    public override void OnSceneLoaded()
    {
        PlayAnimationPart2(rectTransform.gameObject);
    }

}