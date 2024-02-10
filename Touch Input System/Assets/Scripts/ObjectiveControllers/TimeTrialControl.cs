using UnityEngine;

public class TimeTrialControl : MonoBehaviour
{
    public float _defaultTime = 5f;
    public float _currentTime;
    private bool _timeOut = false;
    
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
        if (MyGameManager.Instance != null && GameMenu.Instance != null &&
                    AnotherChanceScript.Instance != null)
        {
            MyGameManager.Instance.GetTimeTrail(GetComponent<TimeTrialControl>());
            MyGameManager.Instance._levelType = "tt";
            MyGameManager.Instance.TimeTrialObjective(true);
            MyGameManager.Instance.ResetAllValues();
            GameMenu.Instance.EnableTimeTrailPanel();
            MyGameManager.Instance.SetLifeInfinte();
            AnotherChanceScript.Instance._timetrail = true;
        }
    }

    private void Update()
    {
        if (!_timeOut && MyGameManager.GameStarted)
        {
            TimeTrialCountDown();
        }
    }

    private void TimeTrialCountDown()
    {
        _currentTime -= Time.deltaTime;
        GameMenu.Instance.TimerUIFeedBack((_currentTime * 100) / 100);
        if (_currentTime <= 0f && MyGameManager.Instance._won == false)
        {
            if (MyGameManager.Instance != null)
            {
                _timeOut = true;
                MyGameManager.Instance.TimeTrialObjective(false);
            }
        }
    }

    public void SecondChance()
    {
        _timeOut = false;
        _currentTime = 31f;
        MyGameManager.Instance.TimeTrialObjective(true);
    }

   public void AddTimeUpgrades(float _time)
   {
        _currentTime += _time;
   }
}
