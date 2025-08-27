using UnityEngine;
using TMPro;
using System.Data;

public class ScoreboardManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] TMP_Text scoreText;

    int score = 0;

    public void IncreaceScore(int amount)
    {
        if (gameManager.GameOver) return;

        score += amount;
        scoreText.text = score.ToString();
    }

}
