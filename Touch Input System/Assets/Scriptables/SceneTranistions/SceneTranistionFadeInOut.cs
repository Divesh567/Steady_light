using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
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


    public override void PlayAnimationPart1(GameObject target, UnityAction onCompleteAction)
    {
        image = target.GetComponent<Image>();

        image.DOFade(0, 0).OnComplete(() =>
        {
            AudioControllerMono audioController = target.GetComponent<AudioControllerMono>();
            audioController.PlayAudioClip(transitionClip1);

            image.DOFade(1, fadeDuration).OnComplete(() =>
            {
                onCompleteAction.Invoke();
            });
        });
       
    }

    public override void PlayAnimationPart2(GameObject target)
    {
        Debug.Log("Scene is loaded completing trasition");

        image.DOFade(1,0).OnComplete(() =>
        {
            AudioControllerMono audioController = target.GetComponent<AudioControllerMono>();
            audioController.PlayAudioClip(transitionClip2);

            image.DOFade(0, fadeDuration).OnComplete(() => OnAnimComplete());

          

        });

       
    }

    public override void OnSceneLoaded()
    {
        PlayAnimationPart2(image.gameObject);
    }
}
