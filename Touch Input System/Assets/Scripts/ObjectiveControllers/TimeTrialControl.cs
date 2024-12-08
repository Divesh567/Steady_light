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

    private void Start()
    {
        _timeOut = false;
        _currentTime = _defaultTime;

        ObjectiveEventHandler.OnTimerObjectiveCompleteEventCaller();



        startAnim.StartAnim(() => GameMenu.Instance.InitObjectiveUI(this), () => MyGameManager.gameState = MyGameManager.GameState.GameRunning);
    }

    private void LateUpdate()
    {
        if (!_timeOut && MyGameManager.gameState == MyGameManager.GameState.GameRunning)
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
