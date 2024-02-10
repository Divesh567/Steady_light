using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager _instance;
    public static DataManager Instance { get { return _instance; } }

    SaveData _saveData;
    JsonSaver _jsonSaver;
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
        _saveData = new SaveData();
        _jsonSaver = new JsonSaver();
        
    }

    

    private void Start()
    {
        LoadData();
        MyGameManager.Instance.GetData();
        UpGradesManager.Instance.GetData();
        LevelSelectorMenu.Instance.GetData();
        MainMenu.Instance.DisplayCompletionBar(unlockedLevels);
    }
    public bool[] unlockedLevels
    {
        get { return _saveData._allLevels; }
        set { _saveData._allLevels = value; }
    }
    public bool[] unlockedWorlds
    {
        get { return _saveData.unlockedWorlds; }
        set { _saveData.unlockedWorlds = value; }
    }

    public bool[] diamondData
    {
        get { return _saveData._diamondData; }
        set { _saveData._diamondData = value; }
    }

    public int diamonds
    {
        get { return _saveData._Diamonds; }
        set { _saveData._Diamonds = value; }
    }

    public int lifes
    {
        get { return _saveData._lifes; }
        set { _saveData._lifes = value; }
    }

    public float time
    {
        get { return _saveData._time; }
        set { _saveData._time = value; }
    }

    public int powerUp
    {
        get { return _saveData._powerUp; }
        set { _saveData._powerUp = value; }
    }

    public bool _lifeMaximum
    {
        get { return _saveData._lifeMaximun; }
        set { _saveData._lifeMaximun = value; }
    }
    public bool _timeMaximum
    {
        get { return _saveData._timeMaximun; }
        set { _saveData._timeMaximun = value; }
    }

    public bool _powerupMaximum
    {
        get { return _saveData._powerUpMaximum; }
        set { _saveData._powerUpMaximum = value; }
    }
    public string hashValue
    {
        get { return _saveData.hashValue; }
        set { _saveData.hashValue = value; }
    }

   

    public void LoadData()
    {
        _jsonSaver.Load(_saveData);
    }

    public void SaveData()
    {
        _jsonSaver.Save(_saveData);
    }

    public void DeleteSaveData()
    {
        _jsonSaver.Delete();
    }

}
