using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AdsManager : MonoBehaviour
{
    public static AdsManager _instance;
    public string AdsState;
    //string adUnitId = "436001";
    int retryAttempt;
    private int interstitialRetryAttempt;
    private int rewardedRetryAttempt;
    private int rewardedInterstitialRetryAttempt;
    public string REWARDED_AD_UNIT_ID;
    public string INTER_AD_UNIT_ID;
    public string bannerAdUnitId;
    void Start()
    {
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) => {

            InitializeInterstitialAds();
            InitializeRewardedAds();
            InitializeBannerAds();
            // AppLovin SDK is initialized, start loading ads
        };
        MaxSdk.SetSdkKey("7PspscCcbGd6ohttmPcZTwGmZCihCW-Jwr7nSJN2a_9Mg0ERPs0tmGdKTK1gs__nr6XHQvK0vTNaTb1uR1mCIN");
        MaxSdk.InitializeSdk();
        
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
    }

   
    private void OnEnable()
    {
     
      
    }
   
    #region inter
    void InitializeInterstitialAds()
    {
        // Attach callback
        MaxSdkCallbacks.OnInterstitialLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.OnInterstitialLoadFailedEvent += OnInterstitialFailedEvent;
        MaxSdkCallbacks.OnInterstitialAdFailedToDisplayEvent += InterstitialFailedToDisplayEvent;
        MaxSdkCallbacks.OnInterstitialHiddenEvent += OnInterstitialDismissedEvent;

        // Load the first interstitial
        LoadInterstitial();
    }

    
    public void LoadInterstitial()
    {
        MaxSdk.LoadInterstitial(INTER_AD_UNIT_ID);

    }

    private void OnInterstitialLoadedEvent(string adUnitId)
    {
        // Interstitial ad is ready to be shown. MaxSdk.IsInterstitialReady(adUnitId) will now return 'true'

        // Reset retry attempt
        interstitialRetryAttempt = 0;
    }

    private void OnInterstitialFailedEvent(string adUnitId, int errorCode)
    {
        // Interstitial ad failed to load 
        // We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds)

        interstitialRetryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, interstitialRetryAttempt));

        Invoke("LoadInterstitial", (float)retryDelay);
    }

    private void InterstitialFailedToDisplayEvent(string adUnitId, int errorCode)
    {
        // Interstitial ad failed to display. We recommend loading the next ad
        LoadInterstitial();
    }

    private void OnInterstitialDismissedEvent(string adUnitId)
    {
        // Interstitial ad is hidden. Pre-load the next ad
        LoadInterstitial();
    }

    public void ShowInter(string s)
    {
        if (MaxSdk.IsInterstitialReady(INTER_AD_UNIT_ID))
        {
            MaxSdk.ShowInterstitial(INTER_AD_UNIT_ID, s);

        }
    }

    public bool verifInter()
    {
        if (MaxSdk.IsInterstitialReady(INTER_AD_UNIT_ID))
        {
            return true;

        }
        else
        {
            return false;
        }
    }
    #endregion

    #region Reward

    void InitializeRewardedAds()
    {
        // Attach callback
        MaxSdkCallbacks.OnRewardedAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.OnRewardedAdLoadFailedEvent += OnRewardedAdFailedEvent;
        MaxSdkCallbacks.OnRewardedAdFailedToDisplayEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.OnRewardedAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.OnRewardedAdClickedEvent += OnRewardedAdClickedEvent;
        MaxSdkCallbacks.OnRewardedAdHiddenEvent += OnRewardedAdDismissedEvent;
        MaxSdkCallbacks.OnRewardedAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

        // Load the first rewarded ad
        LoadRewardedAd();
    }

    void DeInitialiseRewardAds()
    {
        MaxSdkCallbacks.OnRewardedAdLoadedEvent -= OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.OnRewardedAdLoadFailedEvent -= OnRewardedAdFailedEvent;
        MaxSdkCallbacks.OnRewardedAdFailedToDisplayEvent -= OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.OnRewardedAdDisplayedEvent -= OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.OnRewardedAdClickedEvent -= OnRewardedAdClickedEvent;
        MaxSdkCallbacks.OnRewardedAdHiddenEvent -= OnRewardedAdDismissedEvent;
        MaxSdkCallbacks.OnRewardedAdReceivedRewardEvent -= OnRewardedAdReceivedRewardEvent;
    }

    private void LoadRewardedAd()
    {
        MaxSdk.LoadRewardedAd(REWARDED_AD_UNIT_ID);
    }

    private void OnRewardedAdLoadedEvent(string adUnitId)
    {
        // Rewarded ad is ready to be shown. MaxSdk.IsRewardedAdReady(adUnitId) will now return 'true'

        // Reset retry attempt
        rewardedRetryAttempt = 0;
    }

    private void OnRewardedAdFailedEvent(string adUnitId, int errorCode)
    {
        // Rewarded ad failed to load 
        // We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds)

        rewardedRetryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, rewardedRetryAttempt));

        Invoke("LoadRewardedAd", (float)retryDelay);
    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, int errorCode)
    {
         if (EventController.videoRewarded != null)
         {
             EventController.videoRewarded(false);
         }
        // Rewarded ad failed to display. We recommend loading the next ad
        LoadRewardedAd();
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId) { }

    private void OnRewardedAdClickedEvent(string adUnitId) { }

    private void OnRewardedAdDismissedEvent(string adUnitId)
    {
        // Rewarded ad is hidden. Pre-load the next ad
        LoadRewardedAd();
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward)
    {
          if (EventController.videoRewarded != null)
          {
              EventController.videoRewarded(true);
          }
        double retryDelay = Math.Pow(2, Math.Min(6, rewardedRetryAttempt));
        Invoke("LoadRewardedAd", (float)retryDelay);
        // Rewarded ad was displayed and user should receive the reward
    }

    public void ShowReward(string s)
    {
        if (MaxSdk.IsRewardedAdReady(REWARDED_AD_UNIT_ID))
        {
            MaxSdk.ShowRewardedAd(REWARDED_AD_UNIT_ID, s);

        }
    }
    #endregion

    #region banner

    void InitializeBannerAds()
    {
        // Adaptive banners are sized based on device width for positions that stretch full width (TopCenter and BottomCenter).
        // You may use the utility method `MaxSdkUtils.GetAdaptiveBannerHeight()` to help with view sizing adjustments
        MaxSdk.CreateBanner(bannerAdUnitId, MaxSdkBase.BannerPosition.BottomCenter);
        

        // Set background or background color for banners to be fully functional
        MaxSdk.SetBannerBackgroundColor(bannerAdUnitId, Color.black);
    }

    public void ShowBanner(string s)
    {
        
        MaxSdk.ShowBanner(bannerAdUnitId);
    }


    #endregion

    public bool VerifRewarded()
    {
        if (MaxSdk.IsRewardedAdReady(REWARDED_AD_UNIT_ID))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /*  private void Update()
      {

          //rewardbutton.interactable = MaxSdk.IsRewardedAdReady(adUnitId);

      }*/
}
