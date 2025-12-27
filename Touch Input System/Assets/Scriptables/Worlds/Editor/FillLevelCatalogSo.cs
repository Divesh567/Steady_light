using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;
using System.IO;
using Scriptables.Worlds;

[CustomEditor(typeof(LevelCatalogSO))]
public class FillLevelCatalogSo : Editor
{
    private static readonly string[] WorldFolders =
    {
        "Assets/Scenes/Basics",
        "Assets/Scenes/Caves",
        "Assets/Scenes/Factory",
        "Assets/Scenes/Microchips",
        "Assets/Scenes/Planets",
        "Assets/Scenes/Stage"
    };

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(10);

        if (GUILayout.Button("Auto Fill Levels From Scene Folders"))
        {
            FillLevels();
            
        }
    }

    private void FillLevels()
    {
        LevelCatalogSO catalog = (LevelCatalogSO)target;
        catalog.levels.Clear();
        foreach (string folder in WorldFolders)
        {
            if (!Directory.Exists(folder))
            {
                Debug.LogWarning($"Folder not found: {folder}");
                continue;
            }

            string[] sceneGuids = AssetDatabase.FindAssets("t:Scene", new[] { folder });

            foreach (string guid in sceneGuids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);

                var assetRef = new AssetReference(guid);

                catalog.levels.Add(new FlatLevel
                {
                    scene = assetRef
                });
            }
        }

        EditorUtility.SetDirty(catalog);
        AssetDatabase.SaveAssets();

        Debug.Log($"FlatLevelCatalog populated with {catalog.levels.Count} levels.");
    }
}