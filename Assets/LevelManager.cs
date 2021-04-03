using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class LevelManager : MonoBehaviour
{
    public GameObject Level;
    public GameObject SkinMenu;
    public GameObject Player;
    public CinemachineBrain cinemachineBrain;
    public CanvasGroup panel;
    public GameObject gameControl;
    public List<Level> Levels;
    [HideInInspector]
    public Level thislevel;

    public static LevelManager _instance;
    void Awake(){
        _instance = this;
    }
    private void Start()
    {
        Singleton._instance.state = GameState.none;
        int LevelIndex = Singleton._instance.level;
        int l = Levels.FindIndex(d => d.LevelIndex == LevelIndex);
        Level = Levels[l].LevelObj;
        thislevel = Levels[l];
        Player.transform.position = Levels[l].PlayerPos.position;
        /*IronSource.Agent.loadInterstitial();
         
        if (!IronSource.Agent.isInterstitialReady())
        {
            StartCoroutine(LoadInter());
        }*/

        if (!AdsManager._instance.verifInter())
        {
            StartCoroutine(LoadInter());
        }
      
    }
    private void OnEnable()
    {
        Time.timeScale = 1f;
        StartCoroutine(StartPanel());
        SkinMenu.SetActive(false);
        StartCoroutine(EnablePlayer());
       // IronSource.Agent.loadInterstitial();
    }

    IEnumerator EnablePlayer(){
        yield return new WaitForSeconds(0.2f);
        Level.SetActive(true);
        gameControl.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Player.SetActive(true);
    }

    IEnumerator LoadInter()
    {
        AdsManager._instance.LoadInterstitial();
      //  IronSource.Agent.loadInterstitial();
        yield return new WaitForSeconds(3);
        if (!AdsManager._instance.verifInter())
        {
            StartCoroutine(LoadInter());
        }
        /* if (!IronSource.Agent.isInterstitialReady())
         {
             StartCoroutine(LoadInter());
         }*/

    }
    public void OpenSkinMenu(){
         Level.SetActive(false);
        gameControl.SetActive(false);
        SkinMenu.SetActive(true);
         StartCoroutine(ControlCamera());
    }

    public void CloseSkinMenu(){
         Level.SetActive(true);
        gameControl.SetActive(true);
        SkinMenu.SetActive(false);
        StartCoroutine(ControlCamera());
    }

    IEnumerator ControlCamera(){
        cinemachineBrain.m_DefaultBlend.m_Time = 0.4f;
        yield return new WaitForSeconds(0.2f);
        cinemachineBrain.m_DefaultBlend.m_Time = 2.0f;

    }

    IEnumerator StartPanel()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForFixedUpdate();
            panel.alpha -= 0.01f;
        }

        panel.gameObject.SetActive(false);
    }
    
    public string  ReturnLevelindex()
    {
        return "0";
    }
}

[System.Serializable]
public class Level
{
    public Transform PlayerPos;
    public int LevelIndex;
    public GameObject LevelObj;
    public int NbEnnemy;
    public bool IsBonuceLevel;
}
