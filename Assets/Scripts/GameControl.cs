﻿using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameControl : MonoBehaviour
{

    public bool LevelBonuse;
    public GameObject TmeRcanvas;

    public int NBkill;
    public Text EnnemieDieText;
    public Text AllennemieText;
    public int alleennemie;

    public GameObject WinPanel;
    public GameObject LoosePanel;

    public int CoinsWin;
    public int coinsValue;
    public Text CoinsWinText;
    public Text AllCoinsText;
    public int AllCoins;
    public int LevelIndex;
    public Text LevelTextIndex;
    public GameObject AllCanvas;
    public GameObject JoysticsObject;
    public GameObject KillButon;
    

    public GameObject Player;
    private void OnEnable()
    {
        
        Time.timeScale = 1;
        
        EventController.ennemieDown += EnnemieDown;
        EventController.gameLoose += GameLoose;
        AllennemieText.text = "/"+alleennemie.ToString();
        EnnemieDieText.text = NBkill.ToString();
        InitCoin();
        EventController.gameStart += GameStart;
        EventController.enterTeleport += EnterTeleport;
        EventController.leftTeleport += LeftTeleport;
    }
    private void Start()
    {
        if (LevelBonuse)
        {
            EventController.levelBonuseFinished += LevelBonuseFinished;
            coinsValue *= 2;
            if (EventController.isBonuceLevel != null)
            {
                EventController.isBonuceLevel(true);
            }
        }
    }
    private void OnDisable()
    {
        if (LevelBonuse)
        {
            EventController.levelBonuseFinished -= LevelBonuseFinished;
        }
        EventController.ennemieDown -= EnnemieDown;
        EventController.gameLoose -= GameLoose;
        EventController.gameStart -= GameStart;
        EventController.enterTeleport -= EnterTeleport;
        EventController.leftTeleport -= LeftTeleport;


    }
    // Start is called before the first frame update
    void InitCoin()
    {
        LevelTextIndex.text = "Level " + LevelIndex;
        AllCoins = Singleton._instance.coins;
       
    }

    public void EnnemieDown(EnnemiePatrol ennemie)
    {
        NBkill++;
        EnnemieDieText.text = NBkill.ToString();
        CoinsWin += coinsValue;
        if (NBkill==alleennemie)
        {
            GameWin();
            TmeRcanvas.SetActive(false);
        }
    }

    void GameWin()
    {
        AllCoinsText.text = AllCoins.ToString();
        if (EventController.gameWin != null)
        {
            EventController.gameWin();
        }
        StartCoroutine(LevelCompleted());
    }
    IEnumerator LevelCompleted()
    {
        if (EventController.gameWin != null)
        {
            EventController.gameWin();
        }
        yield return new WaitForSecondsRealtime(1);
        WinPanel.SetActive(true);
        CoinsWinText.text = CoinsWin.ToString();
        AllCoinsText.text = AllCoins.ToString();
        yield return new WaitForSecondsRealtime(3f);
        StartCoroutine(SetupCoin());
    }
    void LevelBonuseFinished()
    {
        if (EventController.gameWin != null)
        {
            EventController.gameWin();
        }
        GameWin();
       
    }

    IEnumerator SetupCoin()
    {
        if (EventController.gameWin != null)
        {
            EventController.gameWin();
        }
        int value = CoinsWin / 20;
        for (int i = 0; i < 1000; i++)
        {
            yield return new WaitForSecondsRealtime(0.04f);
            
            if (CoinsWin<=0)
            {
                Singleton._instance.coins = AllCoins;
                Singleton._instance.save();
                CoinsWinText.text = "0";
                break;
            }
            CoinsWin -= value;
            CoinsWinText.text = CoinsWin.ToString();
            AllCoins += value;
            AllCoinsText.text = AllCoins.ToString();

        }
    }

    void GameLoose()
    {
        Time.timeScale = 0;
        LoosePanel.SetActive(true);
        //StartCoroutine(LevelLoose());
    }

    IEnumerator LevelLoose()
    {
       
        yield return new WaitForSecondsRealtime(1);
       
    }
    public void LoadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        int buildIndex = scene.buildIndex;
        
        if (buildIndex==SceneManager.sceneCountInBuildSettings-1)
        {
            buildIndex = 0;
            SceneManager.LoadScene(buildIndex);
        }
        else
        {
            SceneManager.LoadScene(buildIndex + 1);
        }
        
    }

    void GameStart()
    {
        print("Game started");
        AllCanvas.SetActive(true);
    }

    public void RestartScene()
    {

        Scene scene = SceneManager.GetActiveScene();
        int buildIndex = scene.buildIndex;
        SceneManager.LoadScene(buildIndex);
    }

    //player will disepear
    //Joystic Canvas will close
     void EnterTeleport(Transform t)
    {
        Player.SetActive(false);
        KillButon.SetActive(false);
        JoysticsObject.SetActive(false);
    }

    void LeftTeleport(Transform t,Transform pos)
    {
        Player.transform.position = pos.position;
        Player.transform.rotation = pos.rotation;
        Player.SetActive(true);
       
        KillButon.SetActive(true);
        JoysticsObject.SetActive(true);
    }
}
