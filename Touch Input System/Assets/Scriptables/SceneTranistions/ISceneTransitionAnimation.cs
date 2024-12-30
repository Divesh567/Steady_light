using UnityEngine;

public interface ISceneTransitionBaseSO
{
    void OnSceneLoaded();
    void PlayAnimationPart1(GameObject target);
    void PlayAnimationPart2(GameObject target);
}