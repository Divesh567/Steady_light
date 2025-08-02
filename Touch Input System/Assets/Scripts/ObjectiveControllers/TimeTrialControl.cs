using UnityEngine;

public class TimeTrialControl : MonoBehaviour
{
    public float _defaultTime = 5f;
    public static float _currentTime;
    private bool _timeOut = false;

    public GameStartAnim startAnim;
    
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
        _currentTime = _defaultTime;

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
