using System;
using UnityEngine;
using Unity;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;

[Serializable]
public class SaveData 
{

    
    public bool isFirstTextAnimShown = false;
    public string hashValue = String.Empty;
    public List<WorldData> worldDatas;
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
        public bool unlocked;
        public bool completed;



        public Level(string name,List<int> diamondsCollected, bool  isUnlocked = false, bool isCompleted = false)
        {
            LevelName = name;
            completed = isCompleted;
            unlocked = isUnlocked;
        }
    }

    [Serializable]
    public class SoundSettings
    {
        public bool isSfxMuted = false;
        public bool isMusicMuted = false;
    }
}
