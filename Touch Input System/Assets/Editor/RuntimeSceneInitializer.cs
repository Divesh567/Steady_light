using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class RuntimeSceneInitializer
{
    private static bool isInitialLoad = true;
    public static Scene activeScene;

    static RuntimeSceneInitializer()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {

        if (state == PlayModeStateChange.ExitingEditMode)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        }

        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            activeScene = EditorSceneManager.GetActiveScene();

            if (EditorSceneManager.GetActiveScene().buildIndex != 0)
            {
                EditorApplication.update += LoadPreviousScene;

                EditorSceneManager.LoadScene(0);
            }
        }

    }

    private static void LoadPreviousScene()
    {
        EditorApplication.update -= LoadPreviousScene;

        if (!string.IsNullOrEmpty(activeScene.name))
        {
            EditorSceneManager.LoadSceneAsyncInPlayMode(activeScene.path, new LoadSceneParameters(LoadSceneMode.Single));


        }
        else
        {
            Debug.LogError("Failed to load the previous scene because the path is invalid.");
        }
    }
}
