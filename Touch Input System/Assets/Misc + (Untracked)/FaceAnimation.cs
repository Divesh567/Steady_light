using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FaceAnimation : MonoBehaviour
{
    public UnityAction<BallState> OnStateChange;
    
    public BallState currentState;
    
    public void BallStateChange(BallState state)
    {
        currentState = state;
        OnStateChange?.Invoke(state); 
    }
    
}

public enum BallState
{
    Normal,
    Smile,
    Angry
}

