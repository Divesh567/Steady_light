using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteGameObjectFromAllScenes : EditorWindow
{
    [SerializeField]
    private List<GameObject> prefabsToRemove = new List<GameObject>();

    private SerializedObject serializedObject;
    private SerializedProperty prefabListProp;

    [MenuItem("AutoDo/Delete Prefabs From All Scenes")]
    public static void ShowWindow()
    {
        GetWindow<DeleteGameObjectFromAllScenes>("Delete Prefabs From Build Scenes");
    }

    private void OnEnable()
    {
        serializedObject = new SerializedObject(this);
        prefabListProp = serializedObject.FindProperty("prefabsToRemove");
    }

    private void OnGUI()
    {
        GUILayout.Label("Select prefabs to remove instances of from build scenes", EditorStyles.boldLabel);

        serializedObject.Update();
        EditorGUILayout.PropertyField(prefabListProp, new GUIContent("Prefabs To Remove"), true);
        serializedObject.ApplyModifiedProperties();

        if (prefabsToRemove.Count > 0 && GUILayout.Button("Remove From Build Scenes"))
        {
            RemoveGameObjectsFromScenes(prefabsToRemove);
        }
    }

    private static void RemoveGameObjectsFromScenes(List<GameObject> prefabs)
    {
        string originalScene = SceneManager.GetActiveScene().path;

        var buildScenes = EditorBuildSettings.scenes;
        foreach (var buildScene in buildScenes)
        {
            if (!buildScene.enabled)
                continue;

            var scene = EditorSceneManager.OpenScene(buildScene.path);

            DeletePrefabInstances(prefabs);

            EditorSceneManager.SaveScene(scene);
        }

        // Reopen the original scene
        if (!string.IsNullOrEmpty(originalScene))
        {
            EditorSceneManager.OpenScene(originalScene);
        }

        Debug.Log($"Removed all instances of {prefabs.Count} prefab(s) from all build scenes.");
    }

    private static void DeletePrefabInstances(List<GameObject> prefabs)
    {
        var allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (var obj in allObjects)
        {
            GameObject prefabRoot = PrefabUtility.GetCorrespondingObjectFromSource(obj);

            if (prefabs.Contains(prefabRoot))
            {
                Undo.DestroyObjectImmediate(obj);
            }
        }
    }
}
