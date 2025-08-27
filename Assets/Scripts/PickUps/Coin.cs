using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Coin : PickUp
{
    [SerializeField] int scoreAmount = 100;
    ScoreboardManager scoreboardManager;


    void Start()
    {
        scoreboardManager = FindFirstObjectByType<ScoreboardManager>();
        if (scoreboardManager == null)
            Debug.LogError("No Scoreboard Manager found in scene, please add one");
    }
    protected override void OnPickUp()
    {
        scoreboardManager.IncreaceScore(scoreAmount);
    }
}
