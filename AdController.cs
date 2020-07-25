using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;
using GoogleMobileAds.Api;

public class scrAdController : MonoBehaviour
{
    public Platform currentPlatform;
    private RewardType currentRewardType;
    public bool testMode = false;
    public string rewardedVideo = "rewardedVideo";
    public bool rewardAdLoaded;

    //private const string Google_App_ID = "ca-app-pub-7289528884340209~1196943395";
    private const string Google_Test_Id = "ca-app-pub-3940256099942544/5224354917";
    private const string Google_Reward_Id = "ca-app-pub-7289528884340209/3085326869";

    private RewardBasedVideoAd rewardBasedVideo;

    private static scrAdController _instance = null;

    public static scrAdController Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            InitalizeMonetization();
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
       
    private void InitalizeMonetization()
    {
        switch (currentPlatform)
        {
            case Platform.Android:
                print("Android Ad Initalize Started");
                //MobileAds.Initialize(Google_App_ID);
                print("Android Ad Initalize Started");
                Advertisement.Initialize(scrGameId.gameIdAndroid, testMode);
                // Initialize the Google Mobile Ads SDK.
                MobileAds.Initialize(initStatus => { });

                // Get singleton reward based video ad reference.
                this.rewardBasedVideo = RewardBasedVideoAd.Instance;
                rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
                rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
                rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
                rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
                rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
                rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
                rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
                
                this.RequestRewardBasedVideo();
                break;
            case Platform.Ios:
                Debug.LogError("Currently there is no ios!");
                break;
        }

    }
    
    public bool isGoogleAdReady()
    {
        //return Advertisement.IsReady("rewardedVideo");
        if (rewardBasedVideo.IsLoaded() || Advertisement.IsReady("rewardedVideo"))
            return true;
        return false;
    }

    private void RequestRewardBasedVideo()
    {
        string adUnitId = Google_Reward_Id;

        #if UNITY_ANDROID
        //adUnitId = Google_Reward_Id;
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adUnitId);
    }
    
    public void ShowRewardAd(RewardType _type)
    {
        currentRewardType = _type;
        if (rewardBasedVideo.IsLoaded()) //if true gonna display Google Ads else gonna display Unity Ads
        {
            rewardBasedVideo.Show();
        }else{
            if (Advertisement.IsReady("rewardedVideo"))
            {
                var options = new ShowOptions { resultCallback = HandleShowResult };
                Advertisement.Show("rewardedVideo", options);
            }
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (currentRewardType)
        {
            case RewardType.boost:
                scrGameManager.Instance.Boost();
                break;
            case RewardType.idleIncomeReward:
                scrGameManager.Instance.IdleIncomeReward();
                break;
            case RewardType.rebornFree:
                print("rebornfree");
                scrGameManager.Instance.RebornFromAd(false);
                break;
            case RewardType.rebornPremium:
                print("rebornPremium");
                scrGameManager.Instance.RebornFromAd(true);
                break;
            case RewardType.chestReward:
                print("how the hell you come here?");
                break;
            case RewardType.supriseMoney:
                scrGameManager.Instance.GetSupriseReward(false);
                break;
            case RewardType.supriseTc:
                scrGameManager.Instance.GetSupriseReward(true);
                break;
            default:
                break;
        }
    }
    private void HandleAdResult(){
        switch (currentRewardType)
        {
            case RewardType.boost:
                scrGameManager.Instance.Boost();
                break;
            case RewardType.idleIncomeReward:
                scrGameManager.Instance.IdleIncomeReward();
                break;
            case RewardType.rebornFree:
                print("rebornfree");
                scrGameManager.Instance.RebornFromAd(false);
                break;
            case RewardType.rebornPremium:
                print("rebornPremium");
                scrGameManager.Instance.RebornFromAd(true);
                break;
            case RewardType.chestReward:
                print("how the hell you come here?");
                break;
            case RewardType.supriseMoney:
                scrGameManager.Instance.GetSupriseReward(false);
                break;
            case RewardType.supriseTc:
                scrGameManager.Instance.GetSupriseReward(true);
                break;
            default:
                break;
        }
    }
    
    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        rewardAdLoaded = true;
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        rewardAdLoaded = false;
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        HandleAdResult();
        MonoBehaviour.print(
            "HandleRewardBasedVideoRewarded event received for "
                        + amount.ToString() + " " + type);
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }

}
[System.Serializable]
public enum Platform
{
    Android,
    Ios
}
public enum RewardType
{
    boost = 0,
    idleIncomeReward = 1,
    rebornFree=2,
    rebornPremium=3,
    chestReward = 4,
    supriseTc = 5,
    supriseMoney = 6
}
