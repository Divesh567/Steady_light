using FirebaseUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeTrialControl : MonoBehaviour
{
    public float _defaultTime = 5f;
    public static float _currentTime;
    private bool _timeOut = false;

    public GameStartAnim startAnim;

    public bool isTesting = false;
    
    private void Awake()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PitchChangeTimeTrail();
        }
    }

    private void OnEnable()
    {
        SceneTransitionManager.Instance.OnSceneTransitionAnimComplete.AddListener(InitLevel);
    }

    private void OnDisable()
    {
        SceneTransitionManager.Instance.OnSceneTransitionAnimComplete.RemoveListener(InitLevel);
    }

    private void InitLevel()
    {

        _timeOut = false;
        
        string currentScene = SceneManager.GetActiveScene().name;
        
        if (FirebaseRemoteConfigController.Instance.GameBalance.RemoteLevelTimers.
            Exists(x =>
                x.levelName == currentScene))
        {
            float time =
                (FirebaseRemoteConfigController.Instance.GameBalance.RemoteLevelTimers.
                    Find(x =>
                    x.levelName == currentScene)).time;
            _currentTime = time;

        }
        else
        {
            _currentTime = _defaultTime;
        }
            
        // get current scene name 
        // check if exists in remote
        // if yes use that else use _defaultTime
        
       

        ObjectiveEventHandler.OnTimerObjectiveCompleteEventCaller();

        startAnim.StartAnim(() =>
        {

            GameMenu.Instance.InitObjectiveUI(this);
            MyGameManager.Instance.StateChanged(MyGameManager.GameState.GameNotStarted);



        }, () => MyGameManager.Instance.StateChanged(MyGameManager.GameState.GameRunning));
    }

    private void LateUpdate()
    {
        if (!_timeOut && MyGameManager.Instance.gameState == MyGameManager.GameState.GameRunning)
        {
            TimeTrialCountDown();
        }
    }

    private void TimeTrialCountDown()
    {

        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0f)
        {
            if (MyGameManager.Instance != null)
            {
                _timeOut = true;
                ObjectiveEventHandler.OnTimerObjectiveFailedEventCaller();
             
            }
        }
    }


   public void AddTimeUpgrades(float _time)
   {
        _currentTime += _time;
   }

    
}
