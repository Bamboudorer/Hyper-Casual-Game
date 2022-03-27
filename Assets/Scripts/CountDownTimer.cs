// using System.Xml.Schema;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDownTimer : MonoBehaviour
{
    public float timeToPlay = 30f;
    public bool timerHasStarted = false;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timerText;

    // Start the running timer
    void Start() {
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerHasStarted && timerIsRunning) {
            if (timeToPlay > 0) {
                timeToPlay -= Time.deltaTime;
            }
            else {
                timeToPlay = 0;
                timerIsRunning = false;
            }
        }

        int minutes = (int)(timeToPlay / 60);
        int seconds = (int)(timeToPlay % 60); 
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
