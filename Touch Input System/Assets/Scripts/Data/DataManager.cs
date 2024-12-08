using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager _instance;
    public static DataManager Instance { get { return _instance; } }

    SaveData _saveData;
    JsonSaver _jsonSaver;

    public SaveDataSO saveDataSO;
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
        Debug.Log(Application.persistentDataPath);
        LoadData();
      //  MainMenu.Instance.DisplayCompletionBar(unlockedLevels);
    }

    public bool isSfxMuted
    {
        get { return _saveData.soundSettings.isSfxMuted; }
        set { _saveData.soundSettings.isSfxMuted = value; }
    }
    public SaveData.ResourcesData resourcesData
    {
        get { return _saveData.resourcesData; }
        set { _saveData.resourcesData = value; }
    }

    public SaveData.UpgradeData upgradeData
    {
        get { return _saveData.upgradeData; }
        set { _saveData.upgradeData= value; }
    }

    public List<SaveData.WorldData> worldDatas
    {
        get { return _saveData.worldDatas; }
        set { _saveData.worldDatas = value; }
    }

    public bool isMuiscMuted
    {
        get { return _saveData.soundSettings.isMusicMuted; }
        set { _saveData.soundSettings.isMusicMuted = value; }
    }


    public string hashValue
    {
        get { return _saveData.hashValue; }
        set { _saveData.hashValue = value; }
    }

   
    public void SaveCompletedLevel(string levelName ,List<int> diamondsCollected)
    {
        var data = worldDatas;
        var worldData = data.Find(x => x.worldType == RuntimeGameData.worldType);

        if (worldData == null)
        {
            worldData = new SaveData.WorldData(RuntimeGameData.worldType);
            data.Add(worldData);
        }

        if(!worldData.levelsList.Exists(l => l.LevelName == levelName))
        {
            worldData.levelsList.Add(new SaveData.Level(levelName, diamondsCollected, isCompleted : true, isUnlocked : true));
        }
            

        worldDatas = data;
        SaveData();
    }


    public void SaveUnlockedLevel(string levelName, List<int> diamondsCollected)
    {
        var data = worldDatas;
        var worldData = data.Find(x => x.worldType == RuntimeGameData.worldType);

        if (worldData == null)
        {
            worldData = new SaveData.WorldData(RuntimeGameData.worldType);
            data.Add(worldData);
        }

        if (!worldData.levelsList.Exists(l => l.LevelName == levelName))
        {
            worldData.levelsList.Add(new SaveData.Level(levelName, diamondsCollected, isUnlocked: true, isCompleted: false));
        }


        worldDatas = data;
        SaveData();
    }



    public void LoadData()
    {
        if (!_jsonSaver.Load(_saveData))
        {
            SaveData();
        }

        saveDataSO.saveData = _saveData;
    }

    public void SaveData()
    {
        _jsonSaver.Save(_saveData);

        _jsonSaver.Load(_saveData);
    }

    public Task SaveLoadData()
    {
       return Task.Delay(2000);
    }

    public void DeleteSaveData()
    {
        _jsonSaver.Delete();
    }

}
