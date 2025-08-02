using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarControl : MonoBehaviour
{
    private List<Star> stars = new List<Star>();

    public int starsCollected = 0;

    public GameStartAnim startAnim;


    private void OnEnable()
    {
        ObjectiveEventHandler.OnStarInitEvent += AddStars;
        ObjectiveEventHandler.OnStarCollectedEvent += OnStarCollected;
        SceneTransitionManager.Instance.OnSceneTransitionAnimComplete.AddListener(InitLevel);
    }

    private void OnDisable()
    {
        ObjectiveEventHandler.OnStarInitEvent -= AddStars;
        ObjectiveEventHandler.OnStarCollectedEvent -= OnStarCollected;
        SceneTransitionManager.Instance.OnSceneTransitionAnimComplete.RemoveListener(InitLevel);
    }

    private void InitLevel()
    {
        stars.Clear();

        startAnim.StartAnim(() =>
        {

            GameMenu.Instance.InitObjectiveUI(this);
            MyGameManager.Instance.StateChanged(MyGameManager.GameState.GameNotStarted);


        }, () => MyGameManager.Instance.StateChanged(MyGameManager.GameState.GameRunning));
    }

    private void AddStars(Star star)
    {
        stars.Add(star);
    }

    private void OnStarCollected(Star star)
    {
        starsCollected++;

        if(starsCollected >= stars.Count)
        {
            ObjectiveEventHandler.OnStarObjectiveCompletedEventCaller();
        }
    }
}
