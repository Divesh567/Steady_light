using System;
using UnityEngine;
using Unity;
using System.Collections.Generic;

[Serializable]
public class SaveData 
{

    

    public int _lifes = 0;
    public float _time = 0;
    public int _powerUp = 0;


    public int _Diamonds = 5;
    public string hashValue = String.Empty;


    public List<WorldData> worldDatas;
    public ResourcesData resourcesData;
    public UpgradeData upgradeData;
    public SoundSettings soundSettings;


    [Serializable]
    public class WorldData
    {
        public WorldSO.WorldType worldType;
        public List<Level> levelsList;

        public WorldData(WorldSO.WorldType type)
        {
            worldType = type;
            levelsList = new List<Level>();
        }
    }


    [Serializable]
    public class Level
    {
        public string LevelName;
        public int totalDiamondsCollected;
        public bool unlocked;
        public bool completed;

        public List<int> diamondCollectedIndex;

        public Level(string name,List<int> diamondsCollected, bool  isUnlocked = false, bool isCompleted = false)
        {
            LevelName = name;
            diamondCollectedIndex = diamondsCollected;
            completed = isCompleted;
            unlocked = isUnlocked;
        }
    }

    [Serializable]
    public class UpgradeData
    {
        public int lifeUpgrade;
        public int timerUpgrade;
        public int powerUpgrade;
    }

    [Serializable]
    public class SoundSettings
    {
        public bool isSfxMuted = false;
        public bool isMusicMuted = false;
    }


    [Serializable]
    public class ResourcesData
    {
        public int totalDiamondsCollected;
    }
}
