using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_Text timeText;
    [SerializeField] GameObject gameOverText;
    [SerializeField] float startTime = 5f;


    float timeLeft;
    bool gameOver = false;

    //Different ways to make a public getter for a private variable (properties)
    // public bool GameOver { get { return gameOver; } }
    // public bool GameOver { get; private set; }
    public bool GameOver => gameOver;

    void Start()
    {
        timeLeft = startTime;
    }

    void Update()
    {
        DecreaseTime();
    }


    public void IncreaseTimer(float timeIncreaseAmount)
    {
        timeLeft += timeIncreaseAmount;
    }


    void DecreaseTime()
    {
        if (gameOver) return;

        timeLeft -= Time.deltaTime;
        timeText.text = timeLeft.ToString("F2");

        if (timeLeft <= 0)
        {
            PlayerGameOver();
        }
    }

    void PlayerGameOver()
    {
        gameOver = true;
        playerController.enabled = false;
        gameOverText.SetActive(true);
        Time.timeScale = 0.1f;
    }
}
