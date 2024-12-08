using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class ObjectiveEventHandler : MonoBehaviour
{
    public static event Action<Star> OnStarInitEvent;

    public static void OnStarInitEventCaller(Star star)
    {
        Debug.Log("THIS IS CALLED");
        OnStarInitEvent?.Invoke(star);
    }


    public static event Action<Star> OnStarCollectedEvent;

    public static void OnStarCollectedEventCaller(Star star)
    {
        OnStarCollectedEvent?.Invoke(star);
    }


    public static event Action OnStarObjectiveCompleted;

    public static void OnStarObjectiveCompletedEventCaller()
    {
        OnStarObjectiveCompleted?.Invoke();
    }


    public static event Action OnTimerObjectiveComplete;
    public static void OnTimerObjectiveCompleteEventCaller()
    {
        OnTimerObjectiveComplete?.Invoke();
    }

    public static event Action OnTimerObjectiveFailed;
    public static void OnTimerObjectiveFailedEventCaller()
    {
        OnTimerObjectiveFailed?.Invoke();
    }


    public static event Action OnLifeLostEvent;

    public static void OnLifeLostEventCaller()
    {
        OnLifeLostEvent?.Invoke();
    }

    public static event Action OnLifeObjectiveFailed;
    public static void OnLifeObjectiveFailedEventCaller()
    {
        OnLifeObjectiveFailed?.Invoke();
    }


}
