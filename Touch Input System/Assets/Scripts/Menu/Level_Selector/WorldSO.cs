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
        [NonSerialized]
        public string sceneName;

        [Header("SCENE FILE")]
        public  AssetReference sceneAddress;

        [Header("Level Details")]
        public LevelType levelType;

    }

    public List<LevelClass> levels;

    public WorldType worldType;

    public void InitializeLevelNames()
    {
        foreach (var level in levels)
        {
#if UNITY_EDITOR
            if (level.sceneAddress.editorAsset != null)
            {
                level.sceneName = level.sceneAddress.editorAsset.name;
            }
#else
        // Use address (this only works if address == scene name)
        level.sceneName = level.sceneAddress.RuntimeKey.ToString();
#endif
        }
    }

    public AssetReference GetSceneByName(string name)
    {
        foreach (var level in levels)
        {
            if (level.sceneName.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                return level.sceneAddress;
            }
        }

        Debug.LogError($"Couldn't find level with name: {name}");
        return null;
    }
}
