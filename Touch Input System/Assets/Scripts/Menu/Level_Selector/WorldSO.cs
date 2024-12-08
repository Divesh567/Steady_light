using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "World", menuName = "Worlds/New World")]
public class WorldSO : ScriptableObject
{
    public enum WorldType
    {
        Basics,
        Caves,
        Factory,
        Mircochips,
        Planets,
        Stage
    }
    public enum LevelType
    {
        Stars,
        TimeTrail,
        OneLife
    }


    [Serializable]
    public class LevelClass
    {
        [Header("SCENE FILE")]
        public  AssetReference sceneAddress;

        [Header("Level Details")]
        public LevelType levelType;

    }

    public List<LevelClass> levels;

    public WorldType worldType;
}
