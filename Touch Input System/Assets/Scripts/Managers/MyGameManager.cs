using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MyGameManager : MonoBehaviour // Should be a static class
{

    public enum GameState
    {
        GameNotStarted,
        GameRunning,
        GameEnded
    }


    private static MyGameManager _instance;
    public static MyGameManager Instance { get { return _instance; } }

    public static GameState gameState;

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
    }


    public void LevelWon()
    {
        Debug.Log($"This level is Completed: {Path.GetFileName(RuntimeGameData.levelSelectedName)}");

        DataManager.Instance.SaveCompletedLevel(RuntimeGameData.levelSelectedName, new List<int>());

        MenuManager.Instance.OpenMenu(WinScreen.Instance);
        MenuManager.Instance.CloseMenu(GameMenu.Instance);

        gameState = GameState.GameEnded;
    }

    public void LevelLost()
    {
        
    }


}
