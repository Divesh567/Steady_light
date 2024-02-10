using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private static MenuManager _instance;
    public static MenuManager Instance { get { return _instance; } }

    [SerializeField]
    private MainMenu _mainMenuPrefab;
    [SerializeField]
    private SettingsMenu _settingsMenuPrefab;
    [SerializeField]
    private LevelSelectorMenu _levelSelectorMenuPrefab;
    [SerializeField]
    private GameMenu _gameMenuPrefab;
    [SerializeField]
    private WinScreen _winScreenPrefab;
    [SerializeField]
    private LoseScreen _loseScreenPrefab;
    [SerializeField]
    private AnotherChanceScript _anotherChancePrefab;
    [SerializeField]
    private UpgradeMenu _shopMenuPrefab;
    [SerializeField]
    private CreditMenu _creditMenuPrefab;
    [SerializeField]
    private InternetCheck _noInternetMenu;
    [SerializeField]
    private UnlockedWorldScreen _unlockScreens;

    [SerializeField]
    private DestroySelf _loadingScreen;


    public static bool _settingsMenuSwitch = false;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            InitializeMenus();
            DontDestroyOnLoad(gameObject);
        }
    }

    private void InitializeMenus()
    {
        Menu[] menus = { _mainMenuPrefab,_settingsMenuPrefab, _levelSelectorMenuPrefab,
                            _gameMenuPrefab, _winScreenPrefab, _loseScreenPrefab, _anotherChancePrefab,
                                _shopMenuPrefab, _creditMenuPrefab,_noInternetMenu,_unlockScreens};

        foreach (Menu menu in menus)
        {
            if (menu != null)
            {
                Menu newMenu = Instantiate(menu);
            }
        }
    }

    public void OpenMenu(Menu newmenu)
    {
        newmenu.MenuOpen();
    }
    public void CloseMenu(Menu newmenu)
    {
        newmenu.MenuClose();
    }

    public void ShowLoadingScreen()
    {
        Instantiate(_loadingScreen.gameObject, transform.position, transform.rotation);
    }
}
