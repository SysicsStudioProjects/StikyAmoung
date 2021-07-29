using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Analytics;
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

    public LevelManager levelManager;
    public GameObject BGspecialProgress;
    public static bool IsRewardedAfterWin;
    
    private void OnEnable()
    {
        //AdsManager._instance.ShowBanner("Banner_bottom");
        IsRewardedAfterWin = false;
        LevelBonuse = levelManager.thislevel.IsBonuceLevel;
        alleennemie = levelManager.thislevel.NbEnnemy;
        LevelIndex = levelManager.thislevel.LevelIndex;
        Time.timeScale = 1f;
        
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
      //  EventController.chnageButtonRewardRequest += ChangeRewardStatut;
        if (LevelBonuse)
        {
            TmeRcanvas.SetActive(true);
            EventController.levelBonuseFinished += LevelBonuseFinished;
            //coinsValue *= 2;
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
       // EventController.chnageButtonRewardRequest -= ChangeRewardStatut;
        EventController.videoRewarded -= VideoBonuseRewarded;
        EventController.onchangeItems -= InitCoin;



    }
    private void Update()
    {
        if (AddBonuse!=null)
        {
            AddBonuse.interactable = AdsManager._instance.VerifRewarded();
        }
        
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
        //CoinsWin += coinsValue;
        if (NBkill==alleennemie)
        {
            StartCoroutine(YielTowin());
           // AdsManager._instance.DestroyBanner();
            TmeRcanvas.SetActive(false);
        }
    }

    IEnumerator YielTowin()
    {
        Singleton._instance.state = GameState.win;
        FirebaseAnalytics.LogEvent("Win_Level_", new Parameter("Win_Level_", levelManager.Level.ToString()));
        yield return new WaitForSeconds(0.4f);
        if (EventController.gameWin != null)
        {
            EventController.gameWin();
        }
        yield return new WaitForSeconds(0.1f);
        GameWin();
    }
    void GameWin()
    {
       // AdsManager._instance.DestroyBanner();


        AllCoinsText.text = AllCoins.ToString();
        if (LevelBonuse)
        {
            coinsValue = 7;
            CoinsBonuse = coinsValue * alleennemie;
        }
        else
        {
            CoinsBonuse = 50;
        }
        
        CoinsBonusText.text = "+" + (CoinsBonuse*3);
       
        StartCoroutine(LevelCompleted());
       
        // AddBonuse.interactable = IronSource.Agent.isRewardedVideoAvailable();
       
    }
    IEnumerator LevelCompleted()
    {
       // AdsManager._instance.DestroyBanner();

        if (EventController.gameWin != null)
        {
            EventController.gameWin();
        }
        Singleton._instance.level = LevelIndex+1;
        Singleton._instance.save();
        yield return new WaitForSecondsRealtime(1);
        WinPanel.SetActive(true);
        CoinsWinText.text = CoinsWin.ToString();
        AllCoinsText.text = AllCoins.ToString();
        yield return new WaitForSecondsRealtime(3f);
        BGspecialProgress.SetActive(true);
        StartCoroutine(SetupCoin());
        Singleton._instance.level = LevelIndex+1;
        Singleton._instance.save();

    }
    void LevelBonuseFinished()
    {
      //  AdsManager._instance.DestroyBanner();

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
        CoinsWin = CoinsBonuse;
        CoinsWinText.text = CoinsWin.ToString();
        int value = CoinsWin / 10;
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
        // AdsManager._instance.DestroyBanner();
        FirebaseAnalytics.LogEvent("Lose_Level_", new Parameter("Lose_Level_", levelManager.Level.ToString()));
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
        /*int a = 0;
        a = LevelIndex / 5;
        int modulo = LevelIndex % 5;
        if (modulo==0)
        {
            SceneManager.LoadScene(a + 1);
        }
        else
        {
            Scene scene = SceneManager.GetActiveScene();
            int buildIndex = scene.buildIndex;
            SceneManager.LoadScene(buildIndex);
        }*/


        Scene scene = SceneManager.GetActiveScene();
        int buildIndex = scene.buildIndex;
        SceneManager.LoadScene(buildIndex+1);
        if (SkinSelected.nbtest == 1)
        {
            BGSpecialController.skinSetter = null;
            SkinSelected.nbtest = 0;
        }
        /* if (buildIndex==SceneManager.sceneCountInBuildSettings-1)
         {
             buildIndex = 0;
             SceneManager.LoadScene(buildIndex);
         }
         else
         {
             SceneManager.LoadScene(buildIndex + 1);
         }*/

    }
     bool IsGameStarted;
    void GameStart(bool b)
    {
        IsGameStarted=b;
        print("Game started");
        AllCanvas.SetActive(true);

        //AdsManager._instance.ShowBanner();
        if (!Application.isEditor)
        {
            AdsManager._instance.ShowBanner();
        }
           
    }

    public void RestartScene()
    {

        Scene scene = SceneManager.GetActiveScene();
        int buildIndex = scene.buildIndex;
        SceneManager.LoadSceneAsync(buildIndex);
        
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

        //  AdsManager._instance.ShowRewardVideo("Endlevel_win_x3coin_reward");
        AdsManager._instance.ShowRewardVideo("Endlevel_win_x3coin_reward");
        IsBonuseReward = true;
    }

    void ChangeRewardStatut(bool b)
    {
        AddBonuse.interactable = b;
    }

    public GameObject GreatJobText;
    void VideoBonuseRewarded(bool b)
    {
        if (b==false)
        {
            return;
        }
        if (IsBonuseReward)
        {
            StopAllCoroutines();
            //BonusePanel.SetActive(true);
            IsRewardedAfterWin = true;
            if (AddBonuse!=null)
            {
                Destroy(AddBonuse.gameObject);
                CoinsBonuse = CoinsBonuse * 4;
                CoinsWinText.text = CoinsBonuse.ToString();
                AllCoins += 150;
                Singleton._instance.coins = AllCoins;
                AllCoinsText.text = AllCoins.ToString();

                Singleton._instance.save();
                StopAllCoroutines();
                GreatJobText.gameObject.SetActive(true);
            }
            
           
            //BonusePanel.GetComponent<BonuseController>().InitCoins(CoinsBonuse);
        }
       
    }

    public void ContiniueWithoutBonuseReward(string s)
    {
        if (SpecialItemProgress.Topen==true)
        {
            //BGspecialProgress.SetActive(true);
            return;
        }
        if (IsBonuseReward)
        {
            return;
        }
        // AdsManager._instance.ShowIntertiate(s);

        AdsManager._instance.ShowIntertiate("DefaultInterstitial");
    }
}


[System.Serializable]
public class CardImages
{
    public Image Card;
    public CardType cardType;
}

public enum GameState { none,win,loose}