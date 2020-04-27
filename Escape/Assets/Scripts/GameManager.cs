using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float timeLeft = 10.0f;
    public Text countdownText;
    public int level;
    public GameObject explosion;

    

   

    void Start()
    {
        String name = "Level" + level;
        GameObject g = GameObject.Find(name);
        GameObject k = GameObject.Find("key");
        k.transform.position = g.transform.position;


    }


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
        
        explosion.SetActive(true);
    }
}
