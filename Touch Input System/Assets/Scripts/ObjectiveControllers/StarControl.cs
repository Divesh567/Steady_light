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
    }

    private void OnDisable()
    {
        ObjectiveEventHandler.OnStarInitEvent -= AddStars;
        ObjectiveEventHandler.OnStarCollectedEvent -= OnStarCollected;
    }
    private void Start()
    {
        stars.Clear();

        startAnim.StartAnim(() => 
        {

            GameMenu.Instance.InitObjectiveUI(this);
            MyGameManager.gameState = MyGameManager.GameState.GameNotStarted;


        }, () => MyGameManager.gameState = MyGameManager.GameState.GameRunning);
    }


    private void AddStars(Star star)
    {
        Debug.Log("TRIGGERED ADDED");
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
