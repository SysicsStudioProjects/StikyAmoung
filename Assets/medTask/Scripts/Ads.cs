using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Ads : MonoBehaviour
{
    public delegate void tourAds();
    public static event tourAds tourads;
    public Text t;
    public static Ads ins_ads;
    int r;
    public bool tour;
    public int nbvideo;

    // Start is called before the first frame update
    void Start()
    {
        tour = false;
        DontDestroyOnLoad(this);
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
        if (ins_ads == null)
            ins_ads = this;
        else
            Destroy(this);
        

        //initialize the ad units
        IronSource.Agent.init("e87f5639", IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.BANNER);

        //call the methode after 1s
        Invoke("initBanner", 1);

    }
    private void Update()
    {
        t.text = r + "";
    }


    //initialize the banner at the bottom position
    public void initBanner()
    {
        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
    }

    //play the ad Video
    public void showReward()
    {
        IronSource.Agent.showRewardedVideo();
    }


    public void showRewardP(string place)
    {
        IronSource.Agent.showRewardedVideo(place);
    }
    //Invoked when the user completed the video and should be rewarded
    void RewardedVideoAdRewardedEvent(IronSourcePlacement placement)
    {
        
        if (placement.getPlacementName()== "Turn_Complete")
        {
            if (nbvideo == 1)
            {
                tourads();
                nbvideo = 2;
            }
            else
            {
                nbvideo--;
            }
        }
        else
        {
            r++;
        }
    }

    public void load(int a)
    {
        SceneManager.LoadScene(a);
    }

}
