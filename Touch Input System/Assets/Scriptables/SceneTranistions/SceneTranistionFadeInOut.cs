using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "SceneTransitionFadeInOut", menuName = "SceneTransitions/SceneTransitionFadeInOut")]

public class SceneTranistionFadeInOut : SceneTransitionBaseSO
{
    public Image image;
    public float fadeDuration;

    private string ButtonName = "Anim Test Button";


    [Button("$ButtonName")]
    private void TestAction()
    {
        image = SceneTransitionManager.Instance.TestAnimation().GetComponent<Image>();

        image.DOFade(0, 0).OnComplete(() =>
        {
            image.DOFade(1, fadeDuration).OnComplete(() =>
            {
                
            });

        });
      
    }


    public override void PlayAnimationPart1(GameObject target)
    {
        image = target.GetComponent<Image>();

        image.DOFade(0, 0).OnComplete(() =>
        {
            image.DOFade(1, fadeDuration).OnComplete(() =>
            {
                LevelLoader.Instance.LoadNextLevel();
            });
        });
       
    }

    public override void PlayAnimationPart2(GameObject target)
    {
        Debug.Log("Scene is loaded completing trasition");

        image.DOFade(1,0).OnComplete(() =>
        {
            image.DOFade(0, fadeDuration);
        });

       
    }

    public override void OnSceneLoaded()
    {
        PlayAnimationPart2(image.gameObject);
    }
}
