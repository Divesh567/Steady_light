using System.Collections;
using UnityEngine;


public abstract class Menu<T> : Menu where T : Menu<T>
{
    private static T _instance;
    public static T Instance { get { return _instance; } }

    protected virtual void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = (T)this;
            DontDestroyOnLoad(gameObject);
        }
    }
    protected void OnDestroy()
    {
        _instance = null;
    }
}
public abstract class Menu : MonoBehaviour
{
    public virtual void OnQuitPressed()
    {
        Application.Quit();
    }

    public virtual void MenuClose()
    {
    }
    public virtual void MenuOpen()
    {
    }

    public virtual void OnMainMenuButtonPressed()
    {
        if (MenuManager.Instance != null && GameMenu.Instance != null
                       && MainMenu.Instance != null && SettingsMenu.Instance != null)
        {
            MenuManager.Instance.OpenMenu(MainMenu.Instance);
        }
        if (LevelLoader.Instance != null)
        {
            LevelLoader.Instance.LoadMainMenu();
        }

    }

    public virtual IEnumerator PauseGame()
    {
        yield return new WaitForSeconds(0.25f);
        Time.timeScale = 0f;
    }

    public virtual IEnumerator ResumeGame()
    {
        yield return new WaitForSeconds(0f);
        Time.timeScale = 1f;
    }
}
