using System.Collections;
using UnityEngine;

public class ObstacleInstantiator : MonoBehaviour
{
    [SerializeField]
    private float _spawnTimer;
    private int _obstacleToSpawn = 0;
    private Transform _spawnPosition;
    [SerializeField]
    private GameObject[] _obstacles;

    private void Start()
    {
        _spawnPosition = transform.GetChild(1).gameObject.transform;
        StartCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {
        yield return new WaitForSeconds(_spawnTimer);

        if (_obstacles.Length > 1)
        {
            if (_obstacles[_obstacleToSpawn] != null)
            {
                Instantiate(_obstacles[_obstacleToSpawn], _spawnPosition);
                _obstacleToSpawn++;
            }
            else
            {
                _obstacleToSpawn = 0;
            }
        }
        else
        {
            Instantiate(_obstacles[_obstacleToSpawn], _spawnPosition);
        }
        StartCoroutine(SpawnObstacles());
    }
}
