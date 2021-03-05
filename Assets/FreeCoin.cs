using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FreeCoin : MonoBehaviour
{

    public GameObject TimeLine;
    public Text CoinsText;
    int AllCoins;
    int ValueWin;
    public Button _watchButton;
    private void OnEnable()
    {
        EventController.chnageButtonRewardRequest += ChangeRewardStatut;
       
        EventController.videoRewarded += VideoBonuseRewarded;
        _watchButton.interactable = IronSource.Agent.isRewardedVideoAvailable();
    }
    private void OnDisable()
    {
        EventController.chnageButtonRewardRequest -= ChangeRewardStatut;

        EventController.videoRewarded -= VideoBonuseRewarded;
    }

    // Update is called once per frame

    public void OnClick()
    {
        AllCoins = Singleton._instance.coins;
        ShowVideo();




    }
    IEnumerator SetupCoin()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        int value = ValueWin / 20;
        for (int i = 0; i < 1000; i++)
        {
            yield return new WaitForSecondsRealtime(0.04f);

            if (ValueWin <= 0)
            {
                ValueWin = 0;
                break;
            }
            ValueWin -= value;
            AllCoins += value;
            CoinsText.text = AllCoins.ToString();
            Singleton._instance.coins = AllCoins;
            Singleton._instance.save();
            TimeLine.SetActive(false);
        }
    }
    public void ChangeRewardStatut(bool b)
    {
        _watchButton.interactable = b;
    }

    bool IsWatched;
    void ShowVideo()
    {
        IsWatched = true;
        AdsManager._instance.ShowRewardVideo("Mainscreen_freecoin_reward");
    }

    void VideoBonuseRewarded(bool b)
    {
        if (b == false)
        {
            IsWatched = false;
            return;
        }
        if (IsWatched)
        {
            ValueWin = Random.Range(50, 251);
            TimeLine.SetActive(true);
            StartCoroutine(SetupCoin());
         

        }
        IsWatched = false;

    }

}
