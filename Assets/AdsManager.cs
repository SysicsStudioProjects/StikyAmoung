using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Services.LevelPlay;
using com.unity3d.mediation;
using System;

public class AdsManager : MonoBehaviour
{

    public static AdsManager _instance;
    public string AdsState;
    public string key;
    public bool canShowInter;
    // Create Rewarded Ad object 
LevelPlayRewardedAd rewardedAd;
LevelPlayInterstitialAd interstitialAd;



    // Start is called before the first frame update
    void Start()
    {


        // IronSource.Agent.setUserId(AppsFlyer.getAppsFlyerId());
        // AppsFlyer.validateReceipt();
        IronSource.Agent.shouldTrackNetworkState(true);
#if UNITY_ANDROID
        string appKey = "24cd78fa5";
#elif UNITY_IPHONE
        string appKey = "8545d445";
#else
        string appKey = "unexpected_platform";
#endif


        LevelPlay.OnInitSuccess += SdkInitializationCompletedEvent;
        LevelPlay.OnInitFailed += SdkInitializationFailedEvent;
        // SDK init
        LevelPlay.Init(appKey);

    }

    private void SdkInitializationFailedEvent(LevelPlayInitError error)
    {
        Debug.Log("LevelPlay SDK initialization failed with error: " + error.ToString());
    }

    private void SdkInitializationCompletedEvent(LevelPlayConfiguration configuration)
    {
        Debug.Log("LevelPlay SDK initialized successfully.");
    }

    private void Awake()
    {
        if (_instance == null)
        {

            _instance = this;
            DontDestroyOnLoad(this.gameObject);

            //Rest of your Awake code

        }
        else
        {

            Destroy(gameObject);
        }
        //StartCoroutine(LoadInter());



    }

    private void OnEnable()
    {
        rewardedAd = new LevelPlayRewardedAd( "ytbey091kdijhq58" );
        interstitialAd = new LevelPlayInterstitialAd( "ytbey091kdijhq58");
        InitRewardVideo();
        InitIntertiate();
        InitBanner();

    }

    private void OnDisable()
    {
        DiInitRewardVideo();
        DeIntertiate();
        DeInitBanner();
    }


    #region rewardVideo 
    void InitRewardVideo()
    {
        rewardedAd.OnAdLoaded += RewardedVideoAdOpenedEvent;
        rewardedAd.OnAdClosed  += RewardedVideoAdClosedEvent;
        rewardedAd.OnAdInfoChanged  += RewardedVideoAvailabilityChangedEvent;
        rewardedAd.OnAdDisplayed  += RewardedVideoAdStartedEvent;
        //IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
        rewardedAd.OnAdRewarded  += RewardedVideoAdRewardedEvent;
        rewardedAd.OnAdDisplayFailed  += RewardedVideoAdShowFailedEvent;
        rewardedAd.OnAdClicked += RewardedVideoAdClickedEvent;
    }
    void DiInitRewardVideo()
    {
        if (rewardedAd != null)
        {
            rewardedAd.OnAdLoaded -= RewardedVideoAdOpenedEvent;
        rewardedAd.OnAdClosed  -= RewardedVideoAdClosedEvent;
        rewardedAd.OnAdInfoChanged  -= RewardedVideoAvailabilityChangedEvent;
        rewardedAd.OnAdDisplayed  -= RewardedVideoAdStartedEvent;
        //IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
        rewardedAd.OnAdRewarded  -= RewardedVideoAdRewardedEvent;
        rewardedAd.OnAdDisplayFailed  -= RewardedVideoAdShowFailedEvent;
        rewardedAd.OnAdClicked -= RewardedVideoAdClickedEvent;
        }
        

    }

    void RewardedVideoAvailabilityChangedEvent(LevelPlayAdInfo adInfo)
    {
        //when canShowAd false we will disable all button was request a Reward Ads

        AdsState = "unity-script: I got RewardedVideoAvailabilityChangedEvent, value = " + rewardedAd.IsAdReady();
        if (EventController.chnageButtonRewardRequest != null)
        {
            EventController.chnageButtonRewardRequest(rewardedAd.IsAdReady());
        }

        Debug.Log("unity-script: I got RewardedVideoAvailabilityChangedEvent, value = " + rewardedAd.IsAdReady());
    }

    void RewardedVideoAdOpenedEvent(LevelPlayAdInfo adInfo)
    {

        AdsState = "unity-script: I got RewardedVideoAdOpenedEvent";
        Debug.Log("unity-script: I got RewardedVideoAdOpenedEvent");

    }

    void RewardedVideoAdRewardedEvent(LevelPlayAdInfo adInfo, LevelPlayReward adReward)
    {
        if (EventController.videoRewarded != null)
        {
            EventController.videoRewarded(true);
        }
        

    }

    void RewardedVideoAdClosedEvent(LevelPlayAdInfo adInfo)
    {
        if (EventController.videoRewarded != null)
        {
            EventController.videoRewarded(false);
        }
        AdsState = "unity-script: I got RewardedVideoAdClosedEvent";
        Debug.Log("unity-script: I got RewardedVideoAdClosedEvent");
    }

    void RewardedVideoAdStartedEvent(LevelPlayAdInfo adInfo)
    {
        AdsState = "unity-script: I got RewardedVideoAdStartedEvent";
        Debug.Log("unity-script: I got RewardedVideoAdStartedEvent");
    }

    void RewardedVideoAdEndedEvent(LevelPlayAdInfo adInfo)
    {
        AdsState = "unity-script: I got RewardedVideoAdEndedEvent";
        Debug.Log("unity-script: I got RewardedVideoAdEndedEvent");
    }

    void RewardedVideoAdShowFailedEvent( LevelPlayAdDisplayInfoError error)
    {
        if (EventController.videoRewarded != null)
        {
            EventController.videoRewarded(false);
        }
        if (EventController.chnageButtonRewardRequest != null)
        {
            EventController.chnageButtonRewardRequest(false);
        }
        
    }

    void RewardedVideoAdClickedEvent(LevelPlayAdInfo adInfo)
    {
        
    }

    public void ShowRewardVideo(string s)
    {

        if (rewardedAd.IsAdReady())
        {
            rewardedAd.ShowAd(s);
            canShowInter = false;
        }

    }

    public bool VerifRewarded()
    {
        if (rewardedAd.IsAdReady())
        {
            return true;
        }
        else
        {
            rewardedAd.LoadAd();
            return false;
        }
    }

    public bool verifInter()
    {
        if (interstitialAd.IsAdReady())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GoToNextScene(string name)
    {
        StartCoroutine(LoadYourAsyncScene(name));
    }

    IEnumerator LoadYourAsyncScene(string name)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    #endregion
    #region Intertiate
    void InitIntertiate()
    {
        interstitialAd.OnAdLoaded += InterstitialAdReadyEvent;
        interstitialAd.OnAdLoadFailed += InterstitialAdLoadFailedEvent;
        interstitialAd.OnAdDisplayed += InterstitialAdShowSucceededEvent;
        interstitialAd.OnAdDisplayFailed += InterstitialAdShowFailedEvent;
        interstitialAd.OnAdClicked += InterstitialAdClickedEvent;
        interstitialAd.OnAdClicked += InterstitialAdOpenedEvent;
        interstitialAd.OnAdClosed  += InterstitialAdClosedEvent;
    }

    void DeIntertiate()
    {
        if (interstitialAd != null)
        {
            interstitialAd.OnAdLoaded -= InterstitialAdReadyEvent;
        interstitialAd.OnAdLoadFailed -= InterstitialAdLoadFailedEvent;
        interstitialAd.OnAdDisplayed -= InterstitialAdShowSucceededEvent;
        interstitialAd.OnAdDisplayFailed -= InterstitialAdShowFailedEvent;
        interstitialAd.OnAdClicked -= InterstitialAdClickedEvent;
        interstitialAd.OnAdClicked -= InterstitialAdOpenedEvent;
        interstitialAd.OnAdClosed  -= InterstitialAdClosedEvent;
        }
        
    }
    void InterstitialAdReadyEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialAdReadyEvent");
    }

    void InterstitialAdLoadFailedEvent(LevelPlayAdError error)
    {
        //Debug.Log("unity-script: I got InterstitialAdLoadFailedEvent, code: " + error.getCode() + ", description : " + error.getDescription());
    }

    void InterstitialAdShowSucceededEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialAdShowSucceededEvent");

    }

    void InterstitialAdShowFailedEvent(LevelPlayAdDisplayInfoError error)
    {
        //Debug.Log("unity-script: I got InterstitialAdShowFailedEvent, code :  " + error.getCode() + ", description : " + error.getDescription());
    }

    void InterstitialAdClickedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialAdClickedEvent");
    }

    void InterstitialAdOpenedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialAdOpenedEvent");
    }

    void InterstitialAdClosedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialAdClosedEvent");
    }

    public void ShowIntertiate(string s)
    {
        if (interstitialAd.IsAdReady() && canShowInter)
        {
            interstitialAd.ShowAd(s);
            StartCoroutine(loadInter());
        }
        else
        {
            Debug.Log("unity-script: IronSource.Agent.isInterstitialReady - False");
            interstitialAd.LoadAd();

        }
        canShowInter = !canShowInter;
    }
    IEnumerator loadInter()
    {
        yield return new WaitForSeconds(5);
        interstitialAd.LoadAd();

    }
    #endregion

    #region Banner
    void InitBanner()
    {
        // Add Banner Events
        /*IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;
        IronSourceEvents.onBannerAdLoadFailedEvent += BannerAdLoadFailedEvent;
        IronSourceEvents.onBannerAdClickedEvent += BannerAdClickedEvent;
        IronSourceEvents.onBannerAdScreenPresentedEvent += BannerAdScreenPresentedEvent;
        IronSourceEvents.onBannerAdScreenDismissedEvent += BannerAdScreenDismissedEvent;
        IronSourceEvents.onBannerAdLeftApplicationEvent += BannerAdLeftApplicationEvent;*/

        //Add ImpressionSuccess Event
        //IronSourceEvents.onImpressionSuccessEvent += ImpressionSuccessEvent;
    }
    void DeInitBanner()
    {
        // Add Banner Events
        /*IronSourceEvents.onBannerAdLoadedEvent -= BannerAdLoadedEvent;
        IronSourceEvents.onBannerAdLoadFailedEvent -= BannerAdLoadFailedEvent;
        IronSourceEvents.onBannerAdClickedEvent -= BannerAdClickedEvent;
        IronSourceEvents.onBannerAdScreenPresentedEvent -= BannerAdScreenPresentedEvent;
        IronSourceEvents.onBannerAdScreenDismissedEvent -= BannerAdScreenDismissedEvent;
        IronSourceEvents.onBannerAdLeftApplicationEvent -= BannerAdLeftApplicationEvent;

        //Add ImpressionSuccess Event
        IronSourceEvents.onImpressionSuccessEvent -= ImpressionSuccessEvent;*/
    }

    void BannerAdLoadedEvent()
    {
        Debug.Log("unity-script: I got BannerAdLoadedEvent");
    }

    void BannerAdLoadFailedEvent(IronSourceError error)
    {
        Debug.Log("unity-script: I got BannerAdLoadFailedEvent, code: " + error.getCode() + ", description : " + error.getDescription());
    }

    void BannerAdClickedEvent()
    {
        Debug.Log("unity-script: I got BannerAdClickedEvent");

    }

    void BannerAdScreenPresentedEvent()
    {
        Debug.Log("unity-script: I got BannerAdScreenPresentedEvent");
    }

    void BannerAdScreenDismissedEvent()
    {
        Debug.Log("unity-script: I got BannerAdScreenDismissedEvent");
    }

    void BannerAdLeftApplicationEvent()
    {
        Debug.Log("unity-script: I got BannerAdLeftApplicationEvent");
    }
    void ImpressionSuccessEvent(IronSourceImpressionData impressionData)
    {
        Debug.Log("unity - script: I got ImpressionSuccessEvent ToString(): " + impressionData.ToString());
        Debug.Log("unity - script: I got ImpressionSuccessEvent allData: " + impressionData.allData);
    }

    public void ShowBanner()
    {
        //IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM, "Banner_bottom");
        //IronSource.Agent.displayBanner();
    }

    public void DestroyBanner()
    {
        //IronSource.Agent.destroyBanner();
    }
    #endregion


    IEnumerator LoadInter()
    {

        yield return new WaitForSeconds(1);
        print("that's so good");
        interstitialAd.LoadAd();
    }

}
