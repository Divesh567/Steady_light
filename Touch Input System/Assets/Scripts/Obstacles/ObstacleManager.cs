using System.Collections.Generic;
using UnityEngine;

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

        for (int i = 0; i < obstacles.Count; i++)
        {
            string newName = $"{obstacles[i].name}_{i}";
            obstacles[i].InitObstacle(newName);
        }
    }

    
}
