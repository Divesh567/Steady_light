using System;
using UnityEngine.AddressableAssets;

[Serializable]
public static class RuntimeGameData 
{
    public static AssetReference levelSelected; 
    public static string levelSelectedName;
    public static WorldSO.WorldType worldType;
    public static WorldSO.LevelType levelType;


    public static void SetRuntimeLevelData(WorldSO.LevelType levelType, AssetReference sceneAddress)
    {
        RuntimeGameData.levelSelected = sceneAddress;
        RuntimeGameData.levelType = levelType;
    }

}
