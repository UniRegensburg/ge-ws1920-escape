using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI txt;
    public float timeLeft;
    public Text countdownText;
    public int level;
    public AudioSource beep;

    public GameObject explosion;
    public GameObject menuScreen;
    public GameObject player;
    public GameObject exit;

    
    public GameObject ui;

    public Door_Manager dm;
    public PlayerManager pm;
    private bool gameRunning;
    private bool gamewon;

    public GameObject playerStart;

    private String win = "Oh... you made it. I guess this round was just to easy. \nLet's see if you can manage to escape if the key is a little harder to find;)";
    private String lose = "Ouch - did that hurt? I'll give you another try. \nBut don't expect me to make it easier for you;)";

    private void Awake()
    {
        player.SetActive(false);
        menuScreen.SetActive(true);       
    }

    void Start()
    {

        gameRunning = false;
        gamewon = false;

    }

    public void Shorttime()
    {
        timeLeft = 10.0f;
    }


    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (gameRunning==true)
        {
            DisplayCountdown();
            if (timeLeft < 10.0f)
            {
                beep.pitch+=0.001f;
            }

            if (timeLeft <0 && gamewon==false)
            {
                GameLost();
            }
            
            if (gamewon)
            {
                GameWon();
            }
            
        }

        

        else
        {

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        
    }

    private void DisplayCountdown()
    {
        
    
        countdownText.text = "Time left: " + (timeLeft).ToString("0");

        
    }

    public void StartGame()
    {

        dm.Close();
        menuScreen.SetActive(false);
        gameRunning = true;
        gamewon = false;
        timeLeft = 300.0f;
        player.SetActive(true);
        player.transform.position = playerStart.transform.position;
        player.transform.rotation = playerStart.transform.rotation;
        beep.Play();
        ui.SetActive(true);
        


    }

    public void GameLost()
    {
        beep.Stop();
        ui.SetActive(false);
        txt.text = lose;
       

        explosion.SetActive(true);
        if (timeLeft<-5)
        {
            
            GameOver();
        }
    }

    public void GameWon()
    {
       
        gamewon = true;
        txt.text = win;
        
        beep.Stop();
        ui.SetActive(false);
        GameOver();
       
    }

    private void GameOver()
    {
        explosion.SetActive(false);
        
        pm.EmptyInventory();
        gameRunning = false;
        menuScreen.SetActive(true);
        player.SetActive(false);
        ui.SetActive(false);
        
        beep.pitch = 1.0f;
        gamewon = false;
        
        
    }

    
}
