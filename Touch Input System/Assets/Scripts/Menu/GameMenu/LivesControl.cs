using System.Collections;
using System.Collections.Generic;
using FirebaseUtilities;
using UnityEngine;

public class LivesControl : MonoBehaviour
{

    int totalLife = 3;
    int currentLife = 3;

    public bool isTesting = false;
    public bool infiniteLives = false;
    
    public IntEventChannel OnStateChangeEventTrigger;

    private void OnEnable()
    {
        ObjectiveEventHandler.OnLifeLostEvent += OnLifeLost;
    }

    private void OnDisable()
    {
        ObjectiveEventHandler.OnLifeLostEvent -= OnLifeLost;
    }

    private void Start()
    {
        GameMenu.Instance.InitObjectiveUI(this);


        SetCurrentLives();
        // check remote data for no. of lives
        // if value is -1 lives are infinite else set lives to the value

    }

    private void SetCurrentLives()
    {
        int livesValue = FirebaseRemoteConfigController.Instance.GameBalance.lives;

        if (livesValue == -1)
        {
            infiniteLives = true;
        }
        else if(livesValue == 0)
        {
            currentLife = totalLife;
        }
        else
        {
            currentLife = livesValue;
        }
    }

    private void OnLifeLost()
    {
        OnStateChangeEventTrigger.RaiseEvent(2);
        
        if (isTesting) return;
        
        if (infiniteLives) return;

        currentLife--;

        if(currentLife == 0)
        {
            SetCurrentLives();
            ObjectiveEventHandler.OnLifeObjectiveFailedEventCaller();
        }
        
       
    }



}
