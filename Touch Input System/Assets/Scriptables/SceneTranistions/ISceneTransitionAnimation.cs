using UnityEngine;
using UnityEngine.Events;

public interface ISceneTransitionBaseSO
{
    void OnSceneLoaded();
    void PlayAnimationPart1(GameObject target, UnityAction onCompleteAction);
    void PlayAnimationPart2(GameObject target);
}