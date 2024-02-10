using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MyGameManager : MonoBehaviour
{
    private static MyGameManager _instance;
    public static MyGameManager Instance { get { return _instance; } }

    public static  bool GameStarted;
    private DataManager _dataManager;
    public GameObject _currentPowerUp;
    public int _anotherChance = 1;
    public string _levelType;
    public GameObject _ratingScreen;
    private PortalControl _exitPortal;
    private BallCollisions _ball;
    private TimeTrialControl _timeTrail;
    public float _timeToAdd;
    public bool _replaying;
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

    public static bool _objective = false;
    public bool _won = false;
    public bool _Wrapper;
    private int _currentStars = 0;
    private int _totalStars = 3;
    private int _totalLifes = 3;
    private int _currentLifes = 3;
    private int _totalPowers = 1;
    public int _currentPowers = 1;
    public int _diamonds;

    private IARManager _reviewManager;


    private void Start()
    {
        _reviewManager = GetComponent<IARManager>();
    }
    public void GetData()
    {
        //Debug.Log(Application.persistentDataPath);        //Do Not Remove
        _dataManager = DataManager.Instance;
        GetDiamonds();
    }

    public void GetExitPortal(PortalControl _portal)
    {
        _exitPortal = _portal;
    }

    public void GetBall(BallCollisions _ballobject)
    {
        _ball = _ballobject;
    }
    public void GetTimeTrail(TimeTrialControl _timeObject)
    {
        _timeTrail = _timeObject;
    }

    public int GetDiamonds()
    {
       _diamonds = _dataManager.diamonds;
       return _dataManager.diamonds;
    }
    private void Update()
    {
        if (_objective)
        {
            PortalOpen();
            _objective = false;
        }
    }

    public void ResetAllValues()
    {
        _currentStars = 0;
        _currentLifes = _totalLifes;
        _won = false;
        _currentPowers = _totalPowers;
        _anotherChance = 1;
    }
    public void LevelWon()
    {
        if (_won == false)
        {
            GameStarted = false;
            _won = true;
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayLevelComplete();
            }
            //Reset Ojective Related Values
            _currentStars = 0;
            _currentLifes = _totalLifes;
            StartCoroutine(WonEffect());
        }
    }

    IEnumerator WonEffect()
    {
        GameMenu.Instance.ResetAllFeedbackPanels();
        MenuManager.Instance.CloseMenu(GameMenu.Instance);
        Time.timeScale = 0.25f;
        yield return new WaitForSeconds(0.25f);
        Time.timeScale = 1;
        if  (MenuManager.Instance != null)
        {
             if (SceneManager.GetActiveScene().buildIndex == 7)
             {
                Instantiate(_ratingScreen, transform.position, transform.rotation);
                _reviewManager.StartReviewRequest();
                FireBaseInit.Instance.LogEventLevelPlaying();
            }
            UnlockLevel();
        }
    }
    public void UnlockLevel()
    {
        int _levelScore = 0;
        // this is level number not index number
        int currentlevelnumber = SceneManager.GetActiveScene().buildIndex;// index will be equal to the next level number
        FireBaseInit.Instance.LogEventLevelComplete(currentlevelnumber);
        SetDiamonds();
        _dataManager.unlockedLevels[currentlevelnumber] = true;
        _dataManager.unlockedLevels[currentlevelnumber++] = true;
        for (int i = 0; i < _dataManager.unlockedLevels.Length - 1; i++)
        {
            if (_dataManager.unlockedLevels[i] == true)
            {
                _levelScore++;
            }
        }
        UnlockNextWorld(_levelScore);
        ResetAllValues();

    }
    public void UnlockNextWorld(int levelScore)
    {
        switch (levelScore)
        {
            case 7:
                _dataManager.unlockedWorlds[1] = true;
                _dataManager.unlockedLevels[8] = true;
                UnlockedWorldScreen.Instance.EnableScreen(0);
                break;
            case 11:
                _dataManager.unlockedWorlds[2] = true;
                _dataManager.unlockedLevels[14] = true;
                UnlockedWorldScreen.Instance.EnableScreen(1);
                break;
            case 15:
                _dataManager.unlockedWorlds[3] = true;
                _dataManager.unlockedLevels[20] = true;
                UnlockedWorldScreen.Instance.EnableScreen(2);
                break;
            case 19:
                _dataManager.unlockedWorlds[4] = true;
                _dataManager.unlockedLevels[27] = true;
                UnlockedWorldScreen.Instance.EnableScreen(3);
                break;
            case 23:
                _dataManager.unlockedWorlds[5] = true;
                _dataManager.unlockedLevels[32] = true;
                UnlockedWorldScreen.Instance.EnableScreen(4);
                break;
        }
        _dataManager.SaveData();
        MenuManager.Instance.OpenMenu(WinScreen.Instance);
    }

    public void LevelLost()
    {
        int currentlevelnumber = SceneManager.GetActiveScene().buildIndex;
        FireBaseInit.Instance.LogEventLevelFailed(currentlevelnumber);
        GameStarted = false;
        if (MenuManager.Instance != null && AnotherChanceScript.Instance != null
                             && GameMenu.Instance != null && !_objective && _won == false)
        {
            GameMenu.Instance.ResetAllFeedbackPanels();
            MenuManager.Instance.CloseMenu(GameMenu.Instance);
           
            if (SceneManager.GetActiveScene().buildIndex > 4)
            {
                if (_anotherChance == 1)
                {
                    MenuManager.Instance.OpenMenu(AnotherChanceScript.Instance);
                }
                else
                {
                    MenuManager.Instance.OpenMenu(LoseScreen.Instance);
                }
            }
            else
            {
                MenuManager.Instance.OpenMenu(LoseScreen.Instance);
            }
        }

    }

    public void SetDiamonds()
    {
        _dataManager.diamonds = _diamonds;
    }
    public void DiamondCollected()
    {
        _diamonds++;
    }

    private void PortalOpen()
    {
        _exitPortal.OpenPortal();
    }

    private void PortalClose()
    {
        _exitPortal.ClosePortal();
    }

    public void StarsObjectiveStatus(bool status)
    {
        _objective = status;
    }

    public void StarCollected()
    {
        _currentStars += 1;
        GameMenu.Instance.StarCollectedUIFeedBack(_currentStars - 1);
        if (_currentStars >= _totalStars)
        {
            StarsObjectiveStatus(true);
        }
    }

    public void TimeTrialObjective(bool status)
    {
        _objective = status;
        if (_objective == false)
        {
            PortalClose();
            LevelLost();
        }
    }

    public void EnduranceObjective(bool status)
    {
        _objective = status;
        if (_objective == false)
        {
            PortalClose();
            LevelLost();
        }
    }

    public void LifeLost()
    {
        _currentLifes -= 1;
        GameMenu.Instance.LostLifeUIFeedback(_currentLifes);
        if (_currentLifes <= 0)
        {
            EnduranceObjective(false);
        }
    }

    public void SetLifeInfinte()
    {
        _currentLifes = 100;
    }

    public void SetLifeOne()
    {
        _currentLifes = 1;
    }

    public void RewardADWatch()
    {
        _anotherChance = 0;
        if (_levelType == "tt")
        {
            TimeTrailReward();
        }
        else if (_levelType == "eo")
        {
            EnduranceReward();
        }
        else if (_levelType == "sl")
        {
            SkipLevelReward();
        }
    }

    private void TimeTrailReward()
    {
        _ball.Reset();
        FindObjectOfType<TimeTrialControl>().SecondChance();
        _currentPowers = 1;
        if (_currentPowerUp != null)
        {
            _currentPowerUp.GetComponent<ButtonVisualFeedBack>().PowerUpRegainedVisualFeedBack();
        }
        if (GameMenu.Instance != null && MenuManager.Instance != null)
        {
            MenuManager.Instance.OpenMenu(GameMenu.Instance);
            GameMenu.Instance.EnableTimeTrailPanel();
        }
    }

    private void EnduranceReward()
    {
        _ball.Reset();
        _currentLifes = 2;
        _currentPowers = _currentPowers + 1;
        if (_currentPowerUp != null)
        {
            _currentPowerUp.GetComponent<ButtonVisualFeedBack>().PowerUpRegainedVisualFeedBack();
        }
        if (GameMenu.Instance != null && MenuManager.Instance != null)
        {
            MenuManager.Instance.OpenMenu(GameMenu.Instance);
            GameMenu.Instance.EnableStarPanel();
            GameMenu.Instance.EnableLivesPanel();
            GameMenu.Instance.SetLifeTwo();
        }
        if (_currentStars != 0)
        {
            GameMenu.Instance.StarCollectedUIFeedBack(_currentStars - 1);
        }
        if (_currentStars >= _totalStars)
        {
            StarsObjectiveStatus(true);
        }

    }

    public void GetCurrentPowerup(GameObject powerup)
    {
        _currentPowerUp = powerup;
    }

    public void SkipLevelReward()
    {
       LevelWon();
    }

    public void AddLifeUpGrades(int _lifes)
    {
        _totalLifes = 3 + _lifes;
    }

    public void OnAnUpgradeBought(int _cost)
    {
        _diamonds -= _cost;
        SetDiamonds();
        _dataManager.SaveData();
    }

    public void AddTimeFromUpgrades()
    {
        if (_timeTrail != null)
        {
            _timeToAdd = UpGradesManager.Instance.SetTimeUpgrades();
            _timeTrail.AddTimeUpgrades(_timeToAdd);
            GameMenu.Instance.TimerAddUpgradeVisualFeedBack(_timeToAdd);
        }
    }

    public void AddPowerUpgrades(int _powerUps)
    {
        _totalPowers = 1 + _powerUps;
    }

    public int SetPowerUpText()
    {
       return _currentPowers;
    }

    public void CheckUpgrades()
    {
        UpGradesManager.Instance.LoadAllUpGrades();
    }
}
