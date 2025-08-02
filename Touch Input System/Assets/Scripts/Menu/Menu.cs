using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

        graphicRaycaster = GetComponent<GraphicRaycaster>();
    }
    protected void OnDestroy()
    {
        _instance = null;
    }
}
public abstract class Menu : MonoBehaviour
{
    [SerializeField]
    private AudioController audioController;


    [Header("UI COMPONENTS")]
    public MenuMainPanel MainPanel;
    public GraphicRaycaster graphicRaycaster;
    public Canvas canvas;


        

    public virtual void Start()
    {
        MenuClose();
    }
    public virtual void OnQuitPressed()
    {
        Application.Quit();
    }

    public virtual void MenuClose()
    {
        graphicRaycaster.enabled = false;
    }
    public virtual void MenuOpen()
    {
        graphicRaycaster.enabled = true;
        MenuManager.Instance.SetSortingOrder(this);
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
}
