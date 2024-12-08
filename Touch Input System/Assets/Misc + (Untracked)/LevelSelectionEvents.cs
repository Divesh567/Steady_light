using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using System;

public class LevelSelectionEvents : MonoBehaviour
{
    public static event Action OnLevelSelectedEvent;

    public static void OnLevelSelectedEventCaller()
    {
        OnLevelSelectedEvent?.Invoke();
    }
} 
