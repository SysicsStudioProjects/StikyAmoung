using System.Collections;

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

    public CardImages[] cardImages;
    public GameObject TextRequired;

    //Win Button +Bonuse
    int CoinsBonuse;
    public Text CoinsBonusText;
    public Button AddBonuse;
    public GameObject BonusePanel;
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
        EventController.hasACard += GetCard;
        EventController.cardRequired += CardRequired;
        EventController.chnageButtonRewardRequest += ChangeRewardStatut;
        if (LevelBonuse)
        {
            EventController.levelBonuseFinished += LevelBonuseFinished;
            coinsValue *= 2;
            if (EventController.isBonuceLevel != null)
            {
                EventController.isBonuceLevel(true);
            }
        }
        EventController.videoRewarded += VideoBonuseRewarded;
        EventController.onchangeItems += InitCoin;

    }
    private void Start()
    {
        
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
        EventController.hasACard -= GetCard;
        EventController.cardRequired -= CardRequired;
        EventController.chnageButtonRewardRequest -= ChangeRewardStatut;
        EventController.videoRewarded -= VideoBonuseRewarded;
        EventController.onchangeItems -= InitCoin;



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
            StartCoroutine(YielTowin());
            AdsManager._instance.DestroyBanner();
            TmeRcanvas.SetActive(false);
        }
    }

    IEnumerator YielTowin()
    {
        yield return new WaitForSeconds(0.5f);
        GameWin();
    }
    void GameWin()
    {
        AdsManager._instance.DestroyBanner();


        AllCoinsText.text = AllCoins.ToString();
        CoinsBonuse = coinsValue * alleennemie;
        CoinsBonusText.text = "+" + CoinsBonuse;
        if (EventController.gameWin != null)
        {
            EventController.gameWin();
        }
        StartCoroutine(LevelCompleted());
       
            AddBonuse.interactable = IronSource.Agent.isRewardedVideoAvailable();
       
    }
    IEnumerator LevelCompleted()
    {
        AdsManager._instance.DestroyBanner();

        if (EventController.gameWin != null)
        {
            EventController.gameWin();
        }
        Singleton._instance.level = LevelIndex;
        Singleton._instance.save();
        yield return new WaitForSecondsRealtime(1);
        WinPanel.SetActive(true);
        CoinsWinText.text = CoinsWin.ToString();
        AllCoinsText.text = AllCoins.ToString();
        yield return new WaitForSecondsRealtime(3f);
        StartCoroutine(SetupCoin());
        Singleton._instance.level = LevelIndex;
        Singleton._instance.save();

    }
    void LevelBonuseFinished()
    {
        AdsManager._instance.DestroyBanner();

        if (EventController.gameWin != null)
        {
            EventController.gameWin();
        }
        Singleton._instance.level = LevelIndex;
        Singleton._instance.save();
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
        AdsManager._instance.DestroyBanner();

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
     bool IsGameStarted;
    void GameStart(bool b)
    {
        IsGameStarted=b;
        print("Game started");
        AllCanvas.SetActive(true);

        AdsManager._instance.ShowBanner();
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
        //KillButon.SetActive(false);
        JoysticsObject.SetActive(false);
    }

    void LeftTeleport(Transform t,Transform pos)
    {
        Player.transform.position = pos.position;
        Player.transform.rotation = pos.rotation;
        Player.SetActive(true);
       
      //  KillButon.SetActive(true);
        JoysticsObject.SetActive(true);
    }

    void GetCard(CardType type)
    {
        for (int i = 0; i < cardImages.Length; i++)
        {
            if (type==cardImages[i].cardType)
            {
                cardImages[i].Card.gameObject.SetActive(true);
                CardRequired(false);
            }
        }
    }

    void CardRequired(bool b)
    {

        print(b);
        TextRequired.SetActive(b);
    }

    bool IsBonuseReward;
    public void GetBonuse()
    {
        AdsManager._instance.ShowRewardVideo();
        IsBonuseReward = true;
    }

    void ChangeRewardStatut(bool b)
    {
        AddBonuse.interactable = b;
    }


    void VideoBonuseRewarded(bool b)
    {
        if (b==false)
        {
            return;
        }
        if (IsBonuseReward)
        {
            BonusePanel.SetActive(true);
            BonusePanel.GetComponent<BonuseController>().InitCoins(CoinsBonuse);
        }
       
    }

    public void ContiniueWithoutBonuseReward()
    {
        
        if (IsBonuseReward==false)
        {
            int random = Random.Range(0, 2);
            print(random);
            if (random==0)
            {
                AdsManager._instance.ShowIntertiate();
                print("we show video");
                
            }
        }
    }
}


[System.Serializable]
public class CardImages
{
    public Image Card;
    public CardType cardType;
}