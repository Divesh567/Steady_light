using System;
using UnityEngine.AddressableAssets;

[Serializable]
public static class RuntimeGameData 
{
    public static AssetReference levelSelected; 
    public static string levelSelectedName;
    public static WorldSO.WorldType worldType;
    public static WorldSO.LevelType levelType;

}
