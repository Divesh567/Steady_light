using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyGameManager : MonoBehaviour // Should be a static class
{

    public enum GameState
    {
        GameNotStarted,
        GameRunning,
        GameEnded
    }

    [System.Serializable]
    public class OnGameStateChanged : UnityEvent<GameState> { }

    public OnGameStateChanged GameStateChanged = new OnGameStateChanged();

   

    private static MyGameManager _instance;
    public static MyGameManager Instance { get { return _instance; } }

    public GameState gameState;


    public DeathEvent DeathEvent;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
          
            DontDestroyOnLoad(gameObject);
        }

        Debug.Log("ApplicationPersistentDataPath = " + Application.persistentDataPath);
    }

    private LevelTimer levelTimer;
    private bool hasWon; // Should only be used in this class
    public void StateChanged(GameState newState)
    {
       gameState = newState;
       GameStateChanged?.Invoke(gameState);

        if(gameState == GameState.GameRunning)
        {
            levelTimer = new LevelTimer();
            levelTimer.StartTimer();
        }

        if(gameState == GameState.GameEnded)
        {
            if (levelTimer == null) { Debug.LogError("Game Ended but level Timer was not initailzed"); return; }

            levelTimer.StopTimer(hasWon);
        }
    }

    public void LevelWon()
    {
      
        DataManager.Instance.SaveCompletedLevel(RuntimeGameData.levelSelectedName, new List<int>());


        StateChanged(GameState.GameEnded);
        hasWon = true;

        FindAnyObjectByType<WinSequencePlayer>().PlayWinSequence();
       // SequencePlayer.Instance.PlaySequenceAsync();
    }

    public void LevelLost()
    {
        StateChanged(GameState.GameEnded);
        hasWon = false;
    }

    public void OnApplicationFocus(bool focus)
    {
        Debug.Log("Foucs == " + focus);
        if (focus && gameState == GameState.GameRunning)
        {
            MenuManager.Instance.OpenMenu(PauseMenu.Instance);
            Time.timeScale = 0;
        }
    }




}

[System.Serializable]
public class DeathEvent : UnityEvent<Transform>
{
    public new void Invoke(Transform target)
    {
        Debug.Log($"[DeathEvent] Invoked for: {target?.name}");
        base.Invoke(target); // still call the original UnityEvent logic
    }
}

