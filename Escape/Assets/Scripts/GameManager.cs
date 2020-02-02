using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float timeLeft = 300.0f;
    public Text countdownText; 


    void Update()
    {
        timeLeft -= Time.deltaTime;
        countdownText.text = "Time left: "+(timeLeft).ToString("0");
        if (timeLeft < 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        throw new NotImplementedException();
    }
}
