using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] ParticleSystem[] checkpointEffect;
    [SerializeField] float timeIncreaseAmount = 5f;
    [SerializeField] float spawnTimeDecreaseAmount = 0.2f;

    GameManager gameManager;
    ObstacleSpawner obstacleSpawner;

    const String playerTag = "Player";

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager == null)
            Debug.LogError("No Game Manager found in scene, please add one");

        obstacleSpawner = FindFirstObjectByType<ObstacleSpawner>();
        if (obstacleSpawner == null)
            Debug.LogError("No Obstacle Spawner found in scene, please add one");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            gameManager.IncreaseTimer(timeIncreaseAmount);
            foreach (var effect in checkpointEffect)
            {
                effect.Play();
            }
            obstacleSpawner.DecreaseSpawnTime(spawnTimeDecreaseAmount);
        }
    }
}
