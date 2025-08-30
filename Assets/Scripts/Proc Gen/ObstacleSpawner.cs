using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    //Tuning Variables
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] Transform obstacleParent;
    [SerializeField] float obstacleSpawnDelay = 1f;
    [SerializeField] float minObstacleSpawnDelay = 1f;
    [SerializeField] float spawnWidth = 4f;

    void Start()
    {
        StartCoroutine(SpawnObstacleCoRoutine());
    }

    public void DecreaseSpawnTime(float amount)
    {
        obstacleSpawnDelay = Mathf.Max(minObstacleSpawnDelay, obstacleSpawnDelay - amount);
    }

    IEnumerator SpawnObstacleCoRoutine()
    {
        while (true)
        {
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnWidth, spawnWidth), transform.position.y, transform.position.z);
            yield return new WaitForSeconds(obstacleSpawnDelay);
            Instantiate(obstaclePrefab, spawnPosition, Random.rotation, obstacleParent);
        }
    }
}
