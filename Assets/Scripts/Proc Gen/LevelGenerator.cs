using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    //Serialized Variables
    [Header("References")]
    [SerializeField] CameraController cameraController;
    [SerializeField] GameObject[] chunkPrefab;
    [SerializeField] GameObject CheckpointChunkPrefab;
    [SerializeField] Transform chunkParent;
    [SerializeField] ScoreboardManager scoreboardManager;
    [SerializeField] PlayerController playerController;

    [Header("Level Generation Settings")]
    [Tooltip("We start with this many chunks")]
    [SerializeField] int startingChunkAmount = 12;
    [SerializeField] int maxCheckPointInterval = 14;
    [SerializeField] int minCheckPointInterval = 8;
    [SerializeField] float chunkLength = 10f;
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float minMoveSpeed = 2f;
    [SerializeField] float maxMoveSpeed = 20f;
    [SerializeField] float minGravityZ = -22f;
    [SerializeField] float maxGravityZ = -2f;



    //Game Variables
    List<GameObject> chunks = new List<GameObject>();
    int totalWeight = 100;
    int chunk_base_weight = 70;
    int checkpointChunkInterval = 8;
    int checkpointChunksSpawnedPos = 0; //The position at which the checkpoint is being spawned


    void Start()
    {
        SpawnStartingChunks();
    }

    void Update()
    {
        MoveChunks();
    }

    public void ChangeChunkMoveSpeed(float speedAmount)
    {
        float newMoveSpeed = moveSpeed + speedAmount;
        newMoveSpeed = Mathf.Clamp(newMoveSpeed, minMoveSpeed, maxMoveSpeed);

        if (newMoveSpeed != moveSpeed)
        {
            moveSpeed = newMoveSpeed;

            float newGravityZ = Physics.gravity.z - speedAmount;
            newGravityZ = Mathf.Clamp(newGravityZ, minGravityZ, maxGravityZ);


            Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, newGravityZ);
            cameraController.ChangeCameraFOV(speedAmount);

            if (moveSpeed > 8f)
                playerController.ChangeMoveSpeed(speedAmount);
        }
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

        GameObject chunkToSpawn = ChooseChunkToSpawn();
        GameObject newChunkGO = Instantiate(chunkToSpawn, chunkSpawnPos, Quaternion.identity, chunkParent);

        chunks.Add(newChunkGO);
        Chunk newChunk = newChunkGO.GetComponent<Chunk>();
        newChunk.Init(this, scoreboardManager);


        checkpointChunksSpawnedPos++;
    }

    GameObject ChooseChunkToSpawn()
    {
        GameObject chunkToSpawn;

        if (checkpointChunksSpawnedPos % checkpointChunkInterval == 0 && checkpointChunksSpawnedPos != 0)
        {
            chunkToSpawn = CheckpointChunkPrefab;
            checkpointChunkInterval = Random.Range(minCheckPointInterval, maxCheckPointInterval + 1);
            //Reseting the variable to make sure that the next checkpoint is spawned atleast after 8 chunks
            checkpointChunksSpawnedPos = 0;
        }
        else
            chunkToSpawn = chunkPrefab[ChunkSelection()];
        return chunkToSpawn;
    }

    int ChunkSelection()
    {
        int randomValue = Random.Range(0, totalWeight);
        if (randomValue < chunk_base_weight)
            return 0;
        else
            return Random.Range(1, chunkPrefab.Length);
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

