using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Scriptables.Worlds
{
    [CreateAssetMenu(fileName = "levelcatalog", menuName = "Scriptables/Worlds/LevelCatalogSO")]
    public class LevelCatalogSO : ScriptableObject
    {
        public List<FlatLevel>  levels = new List<FlatLevel>();
    }
}

[Serializable]
public class FlatLevel
{
    public AssetReference scene;
}