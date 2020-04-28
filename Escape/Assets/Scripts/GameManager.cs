using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float timeLeft;
    public Text countdownText;
    public int level;
    public GameObject explosion;
    public GameObject menuScreen;
    public GameObject player;
    public GameObject ui;
    public Door_Manager dm;
    public PlayerManager pm;
    private bool gameRunning;

    public GameObject playerStart;

    private void Awake()
    {
        player.SetActive(false);
        menuScreen.SetActive(true);

    }

    void Start()
    {

        /*
        String name = "Level" + level;
        GameObject g = GameObject.Find(name);
        GameObject k = GameObject.Find("key");
        k.transform.position = g.transform.position;
    */
        gameRunning = false;
       

    }


    void Update()
    {
        if (gameRunning==true)
        {
            DisplayCountdown();
        }

        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        
    }

    private void DisplayCountdown()
    {
        timeLeft -= Time.deltaTime;
        countdownText.text = "Time left: " + (timeLeft).ToString("0");
        if (timeLeft < 0)
        {
            GameLost();
        }
    }

    public void StartGame()
    {
        
        menuScreen.SetActive(false);
        gameRunning = true;
        timeLeft = 300.0f;
        player.SetActive(true);
        player.transform.position = playerStart.transform.position;
        player.transform.rotation = playerStart.transform.rotation;

        ui.SetActive(true);


    }

    public void GameLost()
    {
        ui.SetActive(false);
        explosion.SetActive(true);
        if (timeLeft<-5)
        {
            explosion.SetActive(false);
            GameOver();
        }
    }

    public void GameWon()
    {
        dm.Close();
        GameOver();
    }

    private void GameOver()
    {
        pm.EmptyInventory();
        gameRunning = false;
        menuScreen.SetActive(true);
        player.SetActive(false);
        ui.SetActive(false);
        
        
    }

    
}
