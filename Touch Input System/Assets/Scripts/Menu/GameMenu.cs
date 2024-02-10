using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameMenu : Menu<GameMenu>
{
    private GameObject _pausePanel;
    private GameObject _gamePanel;
    private GameObject _starPanel;
    private GameObject _livesPanel;
    private GameObject _infiniteLivesPanel;
    private GameObject _timeTrailPanel;
    private GameObject _diamondPanel;
    private Animator _animator;
    private bool _paused = false;
    [SerializeField]
    private Sprite _musicOn;
    [SerializeField]
    private Sprite _musicOff;
    [SerializeField]
    private Sprite _sfxOn;
    [SerializeField]
    private Sprite _sfxOff;

    [SerializeField]
    private Image _musicToggle;
    [SerializeField]
    private Image _sfxToggle;

    [SerializeField]
    private Sprite _ballSprite;
    [SerializeField]
    private Sprite _lostBallSprite;

    private TextMeshProUGUI _timeText;

    public int lifesBought;
    private void Start()
    {
        _paused = false;
        DontDestroyOnLoad(gameObject);
        _animator = GetComponent<Animator>();
        _pausePanel = transform.GetChild(1).gameObject;
        _gamePanel = transform.GetChild(0).gameObject;
        _starPanel = transform.GetChild(2).gameObject;
        _livesPanel = transform.GetChild(3).gameObject;
        _infiniteLivesPanel = transform.GetChild(4).gameObject;
        _timeTrailPanel = transform.GetChild(5).gameObject;
        _diamondPanel = transform.GetChild(6).gameObject;

        GameObject _timer = _timeTrailPanel.transform.GetChild(1).gameObject;
        _timeText = _timer.GetComponent<TextMeshProUGUI>();
    }

    public override void MenuOpen()
    {
        _gamePanel.gameObject.SetActive(true);
        UpdateImages();
    }

    public override void MenuClose()
    {
        _pausePanel.gameObject.SetActive(false);
        _gamePanel.gameObject.SetActive(false);
        _starPanel.gameObject.SetActive(false);
        _livesPanel.gameObject.SetActive(false);
        _infiniteLivesPanel.gameObject.SetActive(false);
        _timeTrailPanel.gameObject.SetActive(false);
        _diamondPanel.SetActive(false);
    }

    public void GamePanelClose()
    {
        _gamePanel.gameObject.SetActive(false);
    }

    public void GamePanelOpen()
    {
        _gamePanel.gameObject.SetActive(true);
    }

    public void GetAndSetBallSprite(Sprite _currentBallSprite)
    {
        /*_ballSprite = _currentBallSprite;
        Image[] life = _livesPanel.transform.GetComponentsInChildren<Image>();
        foreach (Image _life in life)
        {
            if (_life.gameObject != _livesPanel.gameObject)
            {
                _life.sprite = _ballSprite;
                _life.color = new Color(255, 255, 255, 255);
            }
        }*/

    }

    public void EnableStarPanel()
    {
        _starPanel.gameObject.SetActive(true);
    }
    public void StarCollectedUIFeedBack(int starIndex)
    {
        GameObject star = _starPanel.transform.GetChild(starIndex).gameObject;
        star.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public void EnableTimeTrailPanel()
    {
        _timeTrailPanel.gameObject.SetActive(true);
        _infiniteLivesPanel.gameObject.SetActive(true);
    }

    public void TimerUIFeedBack(float timeLeft)
    {
         _timeText.text = timeLeft.ToString("F0");
    }

    public void TimerAddUpgradeVisualFeedBack(float _time)
    {
        _timeTrailPanel.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "+" + _time.ToString();
        _animator.SetTrigger("CheckPoint_Vf");
    }
    public void EnableLivesPanel()
    {
        _livesPanel.gameObject.SetActive(true);
        EnableLifeUpgrades();
    }
    
    public void LostLifeUIFeedback(int lifeIndex)
    {
        if (_livesPanel.activeInHierarchy == true)
        {
            GameObject life = _livesPanel.transform.GetChild(lifeIndex).gameObject;
            life.GetComponent<Image>().sprite = _lostBallSprite;
            life.GetComponent<Image>().color = new Color(225, 75, 75, 255);
        }
    }

    public void ResetAllFeedbackPanels()
    {
        // Reset StarPanel
        Image[] star = _starPanel.transform.GetComponentsInChildren<Image>();
        foreach (Image _star in star)
        {
            if (_star.gameObject != _starPanel.gameObject)
            {
                _star.color = new Color32(0, 0, 0, 255);
            }
        }
        //Reset LivesPanel
        Image[] life = _livesPanel.transform.GetComponentsInChildren<Image>();
        foreach (Image _life in life)
        {
            if (_life.gameObject != _livesPanel.gameObject)
            {
                _life.sprite = _ballSprite;
                _life.color = new Color(255, 255, 255, 255);
            }
        }
    }

    public void OnPauseButtonPressed()
    {
        if (!_paused)
        {
            MyGameManager.GameStarted = false;
            _pausePanel.gameObject.SetActive(true);
            _animator.SetTrigger("On");
            StartCoroutine(PauseGame());
            GamePanelClose();
            _paused = true;
        }
    }

    public void OnResumeButtonPressed()
    {
        if (_paused)
        {
            MyGameManager.GameStarted = true;
            StartCoroutine(ResumeGame());
            _pausePanel.gameObject.SetActive(false);
            _animator.SetTrigger("Off");
            GamePanelOpen();
            _paused = false;
        }
    }

    public void OnRestartButtonPressed()
    {
        StartCoroutine(ResumeGame());
        ResetAllFeedbackPanels();
        TutorialSwitch.TurorialOn = false;
        if (LevelLoader.Instance != null)
        {
            LevelLoader.Instance.ReloadLevel();
        }
        _animator.SetTrigger("Off");
        _pausePanel.gameObject.SetActive(false);
        _paused = false;
        MyGameManager.Instance.ResetAllValues();
        MyGameManager.Instance._replaying = true;
    }
    public override void OnMainMenuButtonPressed()
    {
        ResetAllFeedbackPanels();
        MyGameManager.Instance.ResetAllValues();
        MenuClose();
        _paused = false;
        base.OnMainMenuButtonPressed();
    }

    public void OnResetButtonPressed()
    {
        if (MyGameManager.Instance._currentPowers == 1)
        {
            GameObject _ball = FindObjectOfType<BallCollisions>().gameObject;

            _ball.GetComponent<BallCollisions>().Reset();
            MyGameManager.Instance._currentPowers = 0;
        }
    }

    public void OnMusicButtonPressed()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.MusicSfx = !SoundManager.MusicSfx;
            SoundManager.Instance.PlayAndStopMusic();
            if (SoundManager.MusicSfx)
            {
                _musicToggle.sprite = _musicOn;
            }
            else
            {
                _musicToggle.sprite = _musicOff;
            }
        }
    }

    public void OnSfxButtonPressed()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.SoundSfx = !SoundManager.SoundSfx;

            if (SoundManager.SoundSfx)
            {
                _sfxToggle.sprite = _sfxOn;
            }
            else
            {
                _sfxToggle.sprite = _sfxOff;
            }
        }
    }

    public void UpdateImages()
    {
        if (SoundManager.Instance != null)
        {
            if (SoundManager.SoundSfx)
            {
                _sfxToggle.sprite = _sfxOn;
            }
            else
            {
                _sfxToggle.sprite = _sfxOff;
            }

            if (SoundManager.MusicSfx)
            {
                _musicToggle.sprite = _musicOn;
            }
            else
            {
                _musicToggle.sprite = _musicOff;
            }
        }
    }

    public void SetLifeOne(int lifeIndex, int lifeIndex1)
    {
        if (_livesPanel.activeInHierarchy == true)
        {
            GameObject life = _livesPanel.transform.GetChild(lifeIndex).gameObject;
            life.gameObject.SetActive(false);
            life = _livesPanel.transform.GetChild(lifeIndex1).gameObject;
            life.gameObject.SetActive(false);
        }
    }
    public void SetLifeAll()
    {
        if (_livesPanel.activeInHierarchy == true)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject life = _livesPanel.transform.GetChild(i).gameObject;
                life.gameObject.SetActive(true);
            }
        }
    }

    public void SetLifeTwo()
    {
        if (_livesPanel.activeInHierarchy == true)
        {
            for (int i = 0; i < _livesPanel.transform.childCount - 1; i++)
            {
                if (i <= 1)
                {
                    _livesPanel.transform.GetChild(i).gameObject.SetActive(true);
                }
                else 
                {
                    _livesPanel.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }

    public void EnableLifeUpgrades()
    {
        for(int i = 0; i <= lifesBought; i++)
        {
            _livesPanel.transform.GetChild(2 + i).gameObject.SetActive(true);
        }
    }

    public void EnableDiamondUI(int total, int collected)
    {
        _diamondPanel.SetActive(true);
        TextMeshProUGUI _text = _diamondPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        _text.text = collected.ToString() + "/" + total.ToString();
    }

}
