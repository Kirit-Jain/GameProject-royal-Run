using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] float timeIncreaseAmount = 5f;
    GameManager gameManager;

    const String playerTag = "Player";

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager == null)
            Debug.LogError("No Game Manager found in scene, please add one");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            gameManager.IncreaseTimer(timeIncreaseAmount);
        }
    }
}
