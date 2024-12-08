using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGradesManager : MonoBehaviour
{
    private static UpGradesManager _instance;
    public static UpGradesManager Instance { get { return _instance; } }

    private int _lifesBought;
    private float _timeBought;
    private int _powerUpBought;

    private DataManager _dataManager;

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
    public void GetData()
    {
        _dataManager = DataManager.Instance;
        LoadAllUpGrades();
    }

   
     public void LoadAllUpGrades()
     {
       
     }

    public void OnLifeBought()
    {
        _lifesBought++;
        /*if(_lifesBought == 3)
        {
            _dataManager._lifeMaximum = true;
            UpgradeMenu.Instance._lifemax = true;

        }*/
        _dataManager.SaveData();
        LoadAllUpGrades();
    }

    public void OnTimeBought()
    {
        _timeBought += 4f;
       /* if(_timeBought == 12f)
        {
            _dataManager._timeMaximum = true;
            UpgradeMenu.Instance._lifemax = true;
        }*/
        _dataManager.SaveData();
        LoadAllUpGrades();
    }

    public void OnPowerUpBought()
    {
        _powerUpBought++;
        _dataManager.SaveData();
        LoadAllUpGrades();
    }
    
    public float SetTimeUpgrades()
    {
        return _timeBought;
    }
}
