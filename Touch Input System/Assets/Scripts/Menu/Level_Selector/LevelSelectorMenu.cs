using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//TODO - Seperate into different scripts according to the world "World > type of world"
public class LevelSelectorMenu : Menu<LevelSelectorMenu>
{
    [Header("Parent Panel")]
    [SerializeField]
    private GameObject _mainPanel;

    [Space(20)]
    [Header("Background")]
    [SerializeField]
    private Image _backGroundImage;

    [Space(10)]
    [Header("Navigation Buttons")]
    [SerializeField]
    private Button _backButton;

    [SerializeField]
    private List<Sprite> _worldBackgroundImages; // Should be in a class

    private int _currentWorldSelected;

    [Space(10)]
    [Header("Worlds List")]
    [SerializeField]
    private List<Button> _allWorlds;// TODO - Create a class for functionality

    [Space(10)]
    [Header("Levels List")]
    [SerializeField]
    private Button[] _allLevels; // TODO - Create a class for this functionality


    [Header("Sprites for Worlds")] // TODO - Create a class for this functionality
    [SerializeField]
    private Sprite _worldLocked;
    [SerializeField]
    private Sprite _worldUnlocked;
    [SerializeField]
    [Header("Sprites for Levels")] // TODO - Create a class for this functionality
    private Sprite _levelLocked;
    [SerializeField]
    private Sprite _levelUnlocked;
    [SerializeField]

    [Header("Sprites for selected Worlds")] // TODO - Create a class for this functionality
    private Sprite _selectedWorldImage;
    [SerializeField]
    private Sprite _unSelectedWorldImage;

    [SerializeField]
    private List<GameObject> _worldList; // TODO - Create a class for this functionality

    private DataManager _dataManager;

    protected override void Awake()
    {
        base.Awake();
    }
    public override void Start()
    {
        base.Start();

        _backButton.onClick.AddListener(() => OnBackButonPressed()); // Add UI sfx

        SoundManager.Instance.PlayMusic();
    }

    public void GetData()
    {
        _dataManager = DataManager.Instance;
    }

    public void OnPlayLevelPressed(int levelindex)
    {
        TutorialSwitch.TurorialOn = true;
        if (MenuManager.Instance != null && GameMenu.Instance != null)
        {
            
            MenuClose();
            MenuManager.Instance.CloseMenu(MainMenu.Instance);
        }
    }


    public override void MenuOpen()
    {
        _currentWorldSelected = 1;
        StartCoroutine(ResumeGame());
        transform.GetChild(_currentWorldSelected).gameObject.SetActive(true);
        _backButton.gameObject.SetActive(true);
        _mainPanel.SetActive(true);
    }

    public override void MenuClose()
    {

        StartCoroutine(ResumeGame());
        _backButton.gameObject.SetActive(false);
        _mainPanel.SetActive(false);
        _worldList[_currentWorldSelected].SetActive(false);
    }

   


    private void OnBackButonPressed()
    {
        if (MainMenu.Instance != null)
        {
            MenuManager.Instance.OpenMenu(MainMenu.Instance);
        }
    }


}
