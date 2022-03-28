using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CountDownTimer timer;
    [SerializeField] private float timerBeforeStart = 3f;
    [SerializeField] private bool gameIsOn = false;
    [SerializeField] private bool gameHasStarted = false;
    [SerializeField] private bool gameHasFinished = false;
    [SerializeField] private TextMeshProUGUI startingText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private PlayerMovements player;
    [SerializeField] private int maxPoints;
    [SerializeField] private int numberOfPoints = 0;
    [SerializeField] private GameObject winSection;
    [SerializeField] private GameObject gameOverSection;

    // Start is called before the first frame update
    void Start()
    {
        maxPoints = 2;

        if (timer == null) {
            timer = GetComponent<CountDownTimer>();
        }

        startingText.gameObject.SetActive(true);

        if (player == null) {
            Debug.Log("null player");
        }

        if (scoreText != null) {
            scoreText.text = "0 %";
        }

        winSection.gameObject.SetActive(false);
        gameOverSection.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        StartGame();
        player.rightToPlay = gameIsOn;
    }

    private void StartGame() {
        if (!gameIsOn && !gameHasFinished) {
            if (timerBeforeStart > 0) {
                timerBeforeStart -= Time.deltaTime;
                float newTime = (int)timerBeforeStart;
                startingText.text = newTime.ToString();
            }
            else {
                timerBeforeStart = 0;
                gameIsOn = true;
                float newTime = (int)timerBeforeStart;
                startingText.text = newTime.ToString();
            }
        } 
        else if (gameIsOn && !gameHasStarted && !gameHasFinished) {
            startingText.gameObject.SetActive(false);
            gameHasStarted = true;
            timer.timerIsRunning = true;
        }
    }

    public void AddScore() {
        numberOfPoints += 1;
        float displayNumber = (int)((numberOfPoints / (float)maxPoints) * 100);
        scoreText.text = displayNumber.ToString() + " %";

        if (numberOfPoints == maxPoints) {
            WinGame();
        }
    }

    public void GameOver() {
        timer.timerIsRunning = false;
        gameIsOn = false;
        gameHasFinished = true;
        gameOverSection.gameObject.SetActive(true);
    }

    public void WinGame() {
        timer.timerIsRunning = false;
        gameIsOn = false;
        gameHasFinished = true;
        winSection.gameObject.SetActive(true);
    }
}
