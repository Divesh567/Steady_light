using System.Collections.Generic;
using FirebaseUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleManager : MonoBehaviour
{
    public List<Obstacle> obstacles = new List<Obstacle>();


    private void Awake()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Obstacle newObstacle;

            if(transform.GetChild(i).TryGetComponent<Obstacle>(out newObstacle))
                obstacles.Add(newObstacle);
        }

        ApplyOverrides();

        for (int i = 0; i < obstacles.Count; i++)
        {
            string newName = $"{obstacles[i].name}_{i}";
            obstacles[i].InitObstacle(newName);
        }
    }

    private void ApplyOverrides()
    {
        var config = FirebaseRemoteConfigController.Instance?.ObstacleOverride;

        if (config == null || config.levels == null)
            return;

        string currentScene = SceneManager.GetActiveScene().name;

        // Find level override
        var levelOverride = config.levels.Find(l => l.levelName == currentScene);

        if (levelOverride == null || levelOverride.obstacles == null)
            return;

        foreach (var obstacleOverride in levelOverride.obstacles)
        {
            // Find obstacle by name (before runtime rename)
            var obstacle = obstacles.Find(o => o.name == obstacleOverride.obstacleId);

            if (obstacle == null)
                continue;

            if (!obstacleOverride.enabled)
            {
                obstacle.gameObject.SetActive(false);
            }
        }
    }
}
