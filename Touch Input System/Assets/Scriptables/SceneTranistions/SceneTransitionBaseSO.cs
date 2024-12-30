using UnityEngine;


public abstract class SceneTransitionBaseSO : ScriptableObject, ISceneTransitionBaseSO
{
    public GameObject trasnitionObject;

    public virtual void OnSceneLoaded()
    {
       
    }

    public virtual void PlayAnimationPart1(GameObject target)
    {
       
    }

    public virtual void PlayAnimationPart2(GameObject target)
    {
       
    }
}
