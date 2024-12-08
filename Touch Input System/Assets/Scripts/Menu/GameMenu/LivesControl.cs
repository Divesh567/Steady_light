using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesControl : MonoBehaviour
{

    int totalLife = 3;
    int currentLife = 3;

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
    }

    private void OnLifeLost()
    {
        currentLife--;

        if(currentLife == 0)
        {
            currentLife = totalLife;
            ObjectiveEventHandler.OnLifeObjectiveFailedEventCaller();
        }
    }



}
