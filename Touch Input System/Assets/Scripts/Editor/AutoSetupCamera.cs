using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using Cinemachine;
using System.Linq;

public class AutoSetupCamera : EditorWindow
{
    private static readonly string mainCameraPrefabPath = "Assets/Prefabs/Camera/Main Camera.prefab";
    private static readonly string cameraRigPrefabPath = "Assets/Prefabs/Camera/GameCVCAM.prefab"; // VCam + TargetGroup

    [MenuItem("AutoDo/Setup Camera & Assign Target Group")]
    public static void SetupCameraAndAssignTargetGroup()
    {
        // -------------------------
        // 1. Delete Existing Cameras & TargetGroup
        // -------------------------
        var allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (var obj in allObjects)
        {
            if (obj.CompareTag("MainCamera") || obj.GetComponent<CinemachineVirtualCamera>() || obj.GetComponent<CinemachineTargetGroup>())
            {
                Undo.DestroyObjectImmediate(obj);
            }
        }

        // -------------------------
        // 2. Add New Prefabs
        // -------------------------
        GameObject mainCamPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(mainCameraPrefabPath);
        GameObject rigPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(cameraRigPrefabPath);

        if (mainCamPrefab == null || rigPrefab == null)
        {
            Debug.LogError("Could not find camera prefabs. Please check the prefab paths.");
            return;
        }

        GameObject mainCamInstance = (GameObject)PrefabUtility.InstantiatePrefab(mainCamPrefab);
        GameObject rigInstance = (GameObject)PrefabUtility.InstantiatePrefab(rigPrefab);

        // -------------------------
        // 3. Find TargetGroup in Instantiated Prefab
        // -------------------------
        CinemachineTargetGroup targetGroup = rigInstance.GetComponentInChildren<CinemachineTargetGroup>();
        if (targetGroup == null)
        {
            Debug.LogError("Target Group not found in camera rig prefab.");
            return;
        }

        // -------------------------
        // 4. Find Components to Add
        // -------------------------
        BallCollisions[] ballCollisions = GameObject.FindObjectsOfType<BallCollisions>();
        TouchController[] players = GameObject.FindObjectsOfType<TouchController>();

        int count = ballCollisions.Length + players.Length;
        if (count == 0)
        {
            Debug.LogWarning("No BallCollision or PlayerController objects found.");
            return;
        }

        Undo.RecordObject(targetGroup, "Assign Cinemachine Targets");
        targetGroup.m_Targets = new CinemachineTargetGroup.Target[count];

        int index = 0;
        foreach (var bc in ballCollisions)
        {
            targetGroup.m_Targets[index++] = new CinemachineTargetGroup.Target
            {
                target = bc.transform,
                weight = 1,
                radius = 2
            };
        }

        foreach (var pc in players)
        {
            targetGroup.m_Targets[index++] = new CinemachineTargetGroup.Target
            {
                target = pc.transform,
                weight = 1,
                radius = 2
            };
        }

        // -------------------------
        // 5. Finalize
        // -------------------------
        EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        Debug.Log($"Setup complete. Assigned {count} targets to new CinemachineTargetGroup.");
    }
}
