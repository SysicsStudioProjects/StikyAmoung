using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private void OnEnable()
    {
        Time.timeScale = 1;
        if (LevelBonuse)
        {
            EventController.levelBonuseFinished += LevelBonuseFinished;
            coinsValue*=  2;
            if (EventController.isBonuceLevel!=null)
            {
                EventController.isBonuceLevel(true);
            }
        }
        EventController.ennemieDown += EnnemieDown;
        EventController.gameLoose += GameLoose;
        AllennemieText.text = "/"+alleennemie.ToString();
        EnnemieDieText.text = NBkill.ToString();
    }
    private void OnDisable()
    {
        if (LevelBonuse)
        {
            EventController.levelBonuseFinished -= LevelBonuseFinished;
        }
        EventController.ennemieDown -= EnnemieDown;
    }
    // Start is called before the first frame update
    

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
        yield return new WaitForSeconds(1);
        WinPanel.SetActive(true);
        CoinsWinText.text = CoinsWin.ToString();
        AllCoinsText.text = AllCoins.ToString();
        yield return new WaitForSeconds(3f);
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
            yield return new WaitForSeconds(0.04f);
            
            if (CoinsWin<=0)
            {
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
        
        StartCoroutine(LevelLoose());
    }

    IEnumerator LevelLoose()
    {
        Time.timeScale = 0;
        LoosePanel.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
       
    }
}
