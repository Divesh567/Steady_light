using UnityEngine;
using UnityEngine.Events;

public abstract class SceneTransitionBaseSO : ScriptableObject, ISceneTransitionBaseSO
{
    public GameObject trasnitionObject;

    public AudioClip transitionClip1;
    public AudioClip transitionClip2;
    public virtual void OnSceneLoaded()
    {
       
    }

    public virtual void PlayAnimationPart1(GameObject target, UnityAction onCompleteAction)
    {
       

    }

    public virtual void PlayAnimationPart2(GameObject target)
    {
       
    }

    public void OnAnimComplete()
    {
        SceneTransitionManager.Instance.OnSceneTransitionAnimComplete?.Invoke();

    }
}
