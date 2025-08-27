using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl;

public class Chunk : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject fencePrefab;
    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject coinPrefab;

    [Header("Spawn Settings")]
    [SerializeField] float appleSpawnChance = 0.3f;
    [SerializeField] float coinSpwanChance = 0.5f;
    [SerializeField] float coinSeperationLength = 2f;
    [SerializeField] float[] lanes = { -2.5f, 0f, 2.5f };


    LevelGenerator levelGenerator;
    ScoreboardManager scoreboardManager;
    List<int> availableLanes = new List<int> { 0, 1, 2 };

    void Start()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoin();
    }

    public void Init(LevelGenerator levelGenerator, ScoreboardManager scoreboardManager)
    {
        this.levelGenerator = levelGenerator;
        if (levelGenerator == null)
            Debug.LogError("No Level Generator found in scene, please add one");

        this.scoreboardManager = scoreboardManager;
        if (scoreboardManager == null)
            Debug.LogError("No Scoreboard Manager found in scene, please add one");
    }

    void SpawnFences()
    {
        int fencesToSpawn = Random.Range(0, lanes.Length);

        for (int i = 0; i < fencesToSpawn; i++)
        {
            if (availableLanes.Count <= 0) break;

            int selectedLane = SelectLane();

            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
            Instantiate(fencePrefab, spawnPosition, Quaternion.identity, this.transform);
        }
    }


    void SpawnApple()
    {
        if (Random.value > appleSpawnChance || availableLanes.Count <= 0) return;

        int selectedLane = SelectLane();

        Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
        Apple newApple = Instantiate(applePrefab, spawnPosition, Quaternion.identity, this.transform).GetComponent<Apple>();
        newApple.Init(levelGenerator);
    }

    void SpawnCoin()
    {
        if (Random.value > coinSpwanChance || availableLanes.Count <= 0) return;
        int coinsToSpawn = Random.Range(1, 6);

        int selectedLane = SelectLane();

        float coinPositionZ = transform.position.z + (coinSeperationLength * 2f);

        for (int i = 0; i < coinsToSpawn; i++)
        {
            float spawnPositionZ = coinPositionZ - (i * coinSeperationLength);
            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, spawnPositionZ);
            Coin newCoin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity, this.transform).GetComponent<Coin>();
            newCoin.Init(scoreboardManager);
        }
    }
    

    int SelectLane()
    {
        int randomLaneIndex = Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomLaneIndex];
        availableLanes.RemoveAt(randomLaneIndex);
        return selectedLane;
    }
}
