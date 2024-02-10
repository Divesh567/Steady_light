using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLevelObjective : MonoBehaviour
{
    [SerializeField]
    private float _time;
    private bool _timeOut = false;

    private void Start()
    {
        if (MyGameManager.Instance != null && GameMenu.Instance != null &&
                 AnotherChanceScript.Instance != null)
        {
            MyGameManager.Instance.ResetAllValues();
            GameMenu.Instance.EnableTimeTrailPanel();
            MyGameManager.Instance.SetLifeInfinte();
           
        }
    }
    private void Update()
    {
        if (!_timeOut)
        {
            _time -= Time.deltaTime;
            GameMenu.Instance.TimerUIFeedBack((_time * 100) / 100);

            if (_time <= 0 && MyGameManager.Instance._won == false)
            {
                _timeOut = true;
                MyGameManager.Instance.LevelWon();
            }
        }
       
    }
}
