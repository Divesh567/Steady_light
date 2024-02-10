using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour
{
    private static LevelLoader _instance;
    private int _mainMenuIndex = 0;

    public static LevelLoader Instance { get { return _instance; } }

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

    public void LoadNextLevel()
    {
        MenuManager.Instance.ShowLoadingScreen();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }

    public void LoadLevel(int levelindex)
    {
        MenuManager.Instance.ShowLoadingScreen();
        SceneManager.LoadScene(levelindex);
    }

    public void LoadMainMenu()
    {
        MenuManager.Instance.ShowLoadingScreen();
        SceneManager.LoadScene(_mainMenuIndex);
    }

    public void ReloadLevel()
    {
        if (MenuManager.Instance != null && GameMenu.Instance != null)
        {
            MenuManager.Instance.OpenMenu(GameMenu.Instance);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

  

}
