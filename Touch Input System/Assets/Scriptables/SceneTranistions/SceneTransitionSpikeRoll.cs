using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SceneTransitionSpikeRoll", menuName = "SceneTransitions/SceneTransitionSpikeRoll")]
public class SceneTransitionSpikeRoll : SceneTransitionBaseSO
{
    public Transform transform;
    public float scaleInDuration;


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

    public override void PlayAnimationPart1(GameObject target)
    {
        transform = target.GetComponent<RectTransform>();
        transform.DOScale(0, 0).OnComplete(() =>
        {
            transform.DOScale(3, scaleInDuration).OnComplete(() =>
            {
                LevelLoader.Instance.LoadNextLevel();
            });
        });

    }

    public override void PlayAnimationPart2(GameObject target)
    {
        Debug.Log("Scene is loaded completing trasition");

        transform.DOScale(3, 0).OnComplete(() =>
        {
            transform.DOScale(0, scaleInDuration);
        });
      
    }

    public override void OnSceneLoaded()
    {
        PlayAnimationPart2(transform.gameObject);
    }
}
