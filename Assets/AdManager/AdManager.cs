using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    private InterstitialAd interstitialAd;
    private string adUnitId = "ca-app-pub-6801434645272696/2049881592"; // Apna Ad Unit ID yaha dalo

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
    {
        Debug.Log("Running on Android: Initializing Ads...");
        MobileAds.Initialize(initStatus =>
        {
            Debug.Log("AdMob Initialized");
            LoadInterstitialAd();  // Naya ad load karo
        });
    }
    else if (Application.platform == RuntimePlatform.IPhonePlayer)
    {
        Debug.Log("Running on iOS: Setting App Pause Behavior...");
        MobileAds.SetiOSAppPauseOnBackground(true); // iOS पर बैकग्राउंड में जाने पर ऐप पॉज़ होगी

        MobileAds.Initialize(initStatus =>
        {
            Debug.Log("AdMob Initialized");
            LoadInterstitialAd();
        });
    }
    else
    {
        Debug.Log("Not running on Android or iOS. Ads will not be initialized.");
    }
    }

    void LoadInterstitialAd()
    {
        if (Application.platform != RuntimePlatform.Android) return; // Sirf Android pe chalega

    if (interstitialAd != null)
    {
        interstitialAd.Destroy();
        interstitialAd = null;
    }

    Debug.Log("Loading Interstitial Ad...");

    // 🔥 FIX: AdRequest.Builder() hata diya, ab direct `new AdRequest()` use hota hai.
    AdRequest request = new AdRequest();

    InterstitialAd.Load(adUnitId, request, (InterstitialAd ad, LoadAdError error) =>
    {
        if (error != null || ad == null)
        {
            Debug.LogError("Interstitial Ad Failed to Load: " + error);
            return;
        }

        interstitialAd = ad;
        Debug.Log("Interstitial Ad Loaded Successfully");

        // Event Listeners
        interstitialAd.OnAdFullScreenContentClosed += HandleOnAdClosed;
        interstitialAd.OnAdFullScreenContentFailed += HandleOnAdFailedToShow;
    });
    }

    public void ShowInterstitialAd()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            Debug.Log("Not running on Android. Can't show ads.");
            return;
        }

        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
        }
        else
        {
            Debug.Log("Interstitial Ad is not ready yet.");
            LoadInterstitialAd(); // Ad ko dubara load karo
        }
    }

    void HandleOnAdClosed()
    {
        Debug.Log("Interstitial Ad Closed, Loading New Ad...");
        LoadInterstitialAd();
    }

    void HandleOnAdFailedToShow(AdError error)
    {
        Debug.LogError("Interstitial Ad Failed to Show: " + error);
        LoadInterstitialAd();
    }
}
