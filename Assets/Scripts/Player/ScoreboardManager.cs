using UnityEngine;
using TMPro;
using System.Data;

public class ScoreboardManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

    int score = 0;

    public void IncreaceScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }

}
