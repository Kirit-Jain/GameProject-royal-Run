using UnityEngine;

public class Apple : PickUp
{
    [SerializeField] float adjustChangeMoveSpeed = 3f;

    LevelGenerator levelGenerator;

    void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
        if (levelGenerator == null)
            Debug.LogError("No Level Generator found in scene, please add one");
    }

    protected override void OnPickUp()
    {
        levelGenerator.ChangeChunkMoveSpeed(adjustChangeMoveSpeed);
        // Debug.Log("Add 1 to speed");
    }
}
