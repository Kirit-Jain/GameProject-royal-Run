using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    //Tuning Variables
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] float obstacleSpawnDelay = 1f;

    //Game Variable
    int obstacleSpawned = 0;

    void Start()
    {
        StartCoroutine(SpawnObstacleCoRoutine());
    }

    IEnumerator SpawnObstacleCoRoutine()
    {
        while (obstacleSpawned < 5)
        {
            yield return new WaitForSeconds(obstacleSpawnDelay);
            Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
            obstacleSpawned++;
        }
    }
}
