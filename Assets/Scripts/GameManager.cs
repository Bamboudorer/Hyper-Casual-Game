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
    [SerializeField] private TextMeshProUGUI startingText;

    // Start is called before the first frame update
    void Start()
    {
        if (timer == null) {
            timer = GetComponent<CountDownTimer>();
        }

        if (startingText == null) {
            Debug.Log("null");
        }
        startingText.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        StartGame();
    }

    private void StartGame() {
        if (!gameIsOn) {
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
        else if (gameIsOn && !gameHasStarted) {
            startingText.gameObject.SetActive(false);
        }
    }
}
