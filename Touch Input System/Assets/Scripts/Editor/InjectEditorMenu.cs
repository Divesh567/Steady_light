#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class InjectorEditorMenu
{
    [MenuItem("IMR/Inject/Run For All Build Scenes")]
    public static void InjectAllBuildScenes()
    {
        // Save current scene so we can restore it later
        var currentScene = SceneManager.GetActiveScene().path;
        bool userWantsSave = EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

        if (!userWantsSave) return;

        var buildScenes = EditorBuildSettings.scenes;
        int processed = 0;

        foreach (var scene in buildScenes)
        {
            if (!scene.enabled) continue; // skip disabled scenes

            var openedScene = EditorSceneManager.OpenScene(scene.path, OpenSceneMode.Single);
            Debug.Log($"[SimpleInjector] Opened scene {openedScene.name}");

            // Inject all behaviours in this scene
            foreach (var root in openedScene.GetRootGameObjects())
            {
                var behaviours = root.GetComponentsInChildren<MonoBehaviour>(true);
                foreach (var mb in behaviours)
                {
                    if (mb == null) continue; // missing script
                    SimpleInjector.Inject(mb, mb.gameObject);
                    EditorUtility.SetDirty(mb);
                }
            }

            // Save scene after injection
            EditorSceneManager.SaveScene(openedScene);
            processed++;
            Debug.Log($"[SimpleInjector] Injected + saved scene: {openedScene.name}");
        }

        // Restore previously open scene
        if (!string.IsNullOrEmpty(currentScene))
        {
            EditorSceneManager.OpenScene(currentScene, OpenSceneMode.Single);
            Debug.Log($"[SimpleInjector] Restored original scene: {currentScene}");
        }

        Debug.Log($"[SimpleInjector] Completed injection for {processed} build scenes.");
    }
}
#endif
