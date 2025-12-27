using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Text;

[CustomEditor(typeof(LevelLoader))] // 👈 replace with your MonoBehaviour / SO
public class CreateLevelProgressionJson : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(10);

        if (GUILayout.Button("Generate Firebase Progression JSON"))
        {
            GenerateJson();
        }
    }

    private void GenerateJson()
    {
        var source = (LevelLoader)target;
        
        if (source.levelHolder.worldSO == null || source.levelHolder.worldSO.Count == 0)
        {
            Debug.LogError("worldSO is empty");
            return;
        }

        if (source.levelHolder.LevelCatalog == null || source.levelHolder.LevelCatalog.levels == null)
        {
            Debug.LogError("LevelCatalog is missing");
            return;
        }

        var config = new RemoteProgressionConfig
        {
            worlds = new List<RemoteProgressionData>()
        };

        foreach (var world in source.levelHolder.worldSO)
        {
            var worldData = new RemoteProgressionData
            {
                worldIndex = (int)world.worldType,
                levelIndices = new List<int>()
            };

            foreach (var level in world.levels)
            {
                int index = source.levelHolder.LevelCatalog.levels.FindIndex(l =>
                    l.scene.AssetGUID == level.sceneAddress.AssetGUID
                );

                if (index < 0)
                {
                    Debug.LogError(
                        $"Scene not found in LevelCatalog: {level.sceneAddress.RuntimeKey}");
                    continue;
                }

                worldData.levelIndices.Add(index);
            }

            config.worlds.Add(worldData);
        }

        string json = JsonUtility.ToJson(config, true);

        EditorGUIUtility.systemCopyBuffer = json;

        Debug.Log("Firebase Progression JSON generated and copied to clipboard:");
        Debug.Log(json);
    }
}
