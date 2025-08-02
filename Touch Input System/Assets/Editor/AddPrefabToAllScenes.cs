using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddPrefabToBuildScenes : EditorWindow
{
    private GameObject prefabToAdd;

    [MenuItem("Tools/Add Prefab To Build Scenes")]
    public static void ShowWindow()
    {
        GetWindow<AddPrefabToBuildScenes>("Add Prefab To Build Scenes");
    }

    private void OnGUI()
    {
        GUILayout.Label("Select a prefab to add to build scenes", EditorStyles.boldLabel);
        prefabToAdd = (GameObject)EditorGUILayout.ObjectField("Prefab", prefabToAdd, typeof(GameObject), false);

        if (prefabToAdd != null && GUILayout.Button("Add to All Build Scenes"))
        {
            AddPrefabToBuildScenesOnly(prefabToAdd);
        }
    }

    private static void AddPrefabToBuildScenesOnly(GameObject prefab)
    {
        string originalScene = SceneManager.GetActiveScene().path;

        var buildScenes = EditorBuildSettings.scenes;
        foreach (var buildScene in buildScenes)
        {
            if (!buildScene.enabled)
                continue;

            var scene = EditorSceneManager.OpenScene(buildScene.path);
            bool alreadyExists = GameObject.Find(prefab.name) != null;

            if (!alreadyExists)
            {
                GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                instance.name = prefab.name; // Avoid duplicates by name
                EditorSceneManager.MarkSceneDirty(scene);
            }

            EditorSceneManager.SaveScene(scene);
        }

        // Reopen the original scene
        if (!string.IsNullOrEmpty(originalScene))
        {
            EditorSceneManager.OpenScene(originalScene);
        }

        Debug.Log($"Prefab '{prefab.name}' added to all build scenes.");
    }
}
