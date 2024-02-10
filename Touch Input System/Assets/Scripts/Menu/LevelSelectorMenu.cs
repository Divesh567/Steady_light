using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelSelectorMenu : Menu<LevelSelectorMenu>
{
    private GameObject _backButton;
    private GameObject _backgroundImage;
    private GameObject _nextWorldButton;
    private GameObject _previousWorldButton;

    [SerializeField]
    private List<Sprite> _worldBackgroundImages;

    private int _currentWorldSelected;

    [SerializeField]
    private List<Button> _allWorlds;
    [SerializeField]
    private Button[] _allLevels;

    [SerializeField]
    private Sprite _worldLocked;
    [SerializeField]
    private Sprite _worldUnlocked;
    [SerializeField]
    private Sprite _levelLocked;
    [SerializeField]
    private Sprite _levelUnlocked;
    [SerializeField]
    private Sprite _selectedWorldImage;
    [SerializeField]
    private Sprite _unSelectedWorldImage;

    [SerializeField]
    private List<GameObject> _worldList;

    private DataManager _dataManager;

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        _backButton = transform.GetChild(1).gameObject;
        _backgroundImage = transform.GetChild(0).gameObject;
        Debug.Log(Application.persistentDataPath);
    }

    public void GetData()
    {
        _dataManager = DataManager.Instance;
    }
    public void CheckWorldStatus()
    {
        for (int i = 0; i < _allWorlds.Count; i++)
        {
            if (_dataManager.unlockedWorlds[i] == false)
            {
                _allWorlds[i].interactable = false;
                var _worldLockImage = _allWorlds[i].transform.GetChild(1).gameObject.GetComponent<Image>();
                _worldLockImage.color = new Color32(255, 255, 255, 255);
            }
            else
            {
                _allWorlds[i].interactable = true;
                var _worldLockImage = _allWorlds[i].transform.GetChild(1).gameObject.GetComponent<Image>();
                _worldLockImage.color = new Color32(0,0,0,0);
            }
        }
        CheckLevelStatus();
    }
    public void CheckLevelStatus()
    {
        for (int i = 0; i < _allLevels.Length; i++)
        {
            if (_dataManager.unlockedLevels[i] == false)
            {
                Button lockedLevel = _allLevels[i];
                lockedLevel.enabled = false;
                lockedLevel.gameObject.GetComponent<Image>().sprite = _levelLocked;
            }
            else
            {
                Button lockedLevel = _allLevels[i];
                lockedLevel.enabled = true;
                lockedLevel.gameObject.GetComponent<Image>().sprite = _levelUnlocked;
            }
        }
    }
    public void OnPlayLevelPressed(int levelindex)
    {
        TutorialSwitch.TurorialOn = true;
        if (MenuManager.Instance != null && GameMenu.Instance != null)
        {
            
            MenuClose();
            MenuManager.Instance.CloseMenu(MainMenu.Instance);
        }
        if (LevelLoader.Instance != null)
        {
            LevelLoader.Instance.LoadLevel(levelindex);
        }
    }


    public override void MenuOpen()
    {
        _currentWorldSelected = 1;
        StartCoroutine(ResumeGame());
        transform.GetChild(_currentWorldSelected).gameObject.SetActive(true);
        _backButton.gameObject.SetActive(true);
        _backgroundImage.SetActive(true);
        LoadUnlockedWorlds();
        OnWorldButtonPressed(0);
    }

    public override void MenuClose()
    {

        StartCoroutine(ResumeGame());
        _backButton.gameObject.SetActive(false);
        _backgroundImage.SetActive(false);
        _worldList[_currentWorldSelected].SetActive(false);
    }

   
    public void LoadUnlockedWorlds()
    {
        CheckWorldStatus();
    }

    public void OnBackButonPressed()
    {
        if (MainMenu.Instance != null && SettingsMenu.Instance != null && MenuManager.Instance != null)
        {
            MenuManager.Instance.OpenMenu(MainMenu.Instance);
            MenuManager.Instance.CloseMenu(LevelSelectorMenu.Instance);
        }
    }

    public void OnWorldButtonPressed(int _worldindex)
    {
        for(int i = 0; i < _allWorlds.Count; i++)
        {
            if(i == _worldindex)
            {

                var worldImage = _allWorlds[i].GetComponent<Image>();
                worldImage.sprite = _selectedWorldImage;
                worldImage.color = new Color32(255, 255, 255, 255);
                _worldList[_currentWorldSelected].gameObject.SetActive(false);
                _worldList[_worldindex].gameObject.SetActive(true);
                _currentWorldSelected = _worldindex;
                _backgroundImage.GetComponent<Image>().sprite = _worldBackgroundImages[_worldindex];

            }
            else
            {
               var worldImage =  _allWorlds[i].GetComponent<Image>();
                worldImage.sprite = _unSelectedWorldImage;
                worldImage.color = new Color32(100, 100, 100, 100);
            }
        }
        
    }

    public void OnNextWorldPressed()
    {
        transform.GetChild(_currentWorldSelected).gameObject.SetActive(false);
        if (_currentWorldSelected < 6)
        {
            _currentWorldSelected++;
            if (_currentWorldSelected == 6)
            {
                _nextWorldButton.gameObject.SetActive(false);

            }
            if (_currentWorldSelected == 2)
            {
                _previousWorldButton.gameObject.SetActive(true);
            }

            transform.GetChild(_currentWorldSelected).gameObject.SetActive(true);
        }
    }

    public void OnPreviousWorldPressed()
    {
        transform.GetChild(_currentWorldSelected).gameObject.SetActive(false);
        if (_currentWorldSelected <= 6)
        {
            _currentWorldSelected--;
            if (_currentWorldSelected == 1)
            {
                _previousWorldButton.gameObject.SetActive(false);
            }
            if (_currentWorldSelected == 5)
            {
                _nextWorldButton.gameObject.SetActive(true);
            }

            transform.GetChild(_currentWorldSelected).gameObject.SetActive(true);
        }
    }

}
