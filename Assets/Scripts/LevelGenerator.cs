using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    //Serialized Variables
    [Header("Chunk Generation Settings")]
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] int startingChunkAmount = 12;
    [SerializeField] Transform chunkParent;
    [SerializeField] float chunkLength = 10f;
    [SerializeField] float moveSpeed = 8f;


    //Game Variables
    List<GameObject> chunks = new List<GameObject>();

    void Start()
    {
        SpawnStartingChunks();
    }

    void Update()
    {
        MoveChunks();
    }

    void SpawnStartingChunks()
    {
        for (int i = 0; i < startingChunkAmount; i++)
        {
            SpwanSingularChunk();
        }
    }

    private void SpwanSingularChunk()
    {
        float spawnPositionZ = CalculateSpawnPositionZ();

        Vector3 chunkSpawnPos = new Vector3(transform.position.x, transform.position.y, spawnPositionZ);
        GameObject newChunk = Instantiate(chunkPrefab, chunkSpawnPos, Quaternion.identity, chunkParent);

        chunks.Add(newChunk);
    }

    float CalculateSpawnPositionZ()
    {
        float spawnPositionZ;

        if (chunks.Count == 0)
            spawnPositionZ = transform.position.z;
        else
        {
            spawnPositionZ = chunks[chunks.Count - 1].transform.position.z + chunkLength;
        }

        return spawnPositionZ;
    }

    void MoveChunks()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(-transform.forward * (moveSpeed * Time.deltaTime));

            if (chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength)
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                SpwanSingularChunk();
            }
        }
    }
}

