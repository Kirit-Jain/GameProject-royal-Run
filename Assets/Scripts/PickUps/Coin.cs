using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Coin : PickUp
{
    [SerializeField] int scoreAmount = 100;
    ScoreboardManager scoreboardManager;


    public void Init(ScoreboardManager scoreboardManager)
    {
        this.scoreboardManager = scoreboardManager;
        if (scoreboardManager == null)
            Debug.LogError("No Scoreboard Manager found in scene, please add one");
    }
    protected override void OnPickUp()
    {
        scoreboardManager.IncreaceScore(scoreAmount);
    }
}
