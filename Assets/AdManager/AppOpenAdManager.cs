using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AppOpenAdManager : MonoBehaviour
{
    private static AppOpenAd appOpenAd;
    private static bool isShowingAd = false;
    private static string adUnitId = "ca-app-pub-6801434645272696/6003151625"; 

    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        LoadAd();
    }

    public static void LoadAd()
    {
        if (appOpenAd != null)
        {
            appOpenAd.Destroy();
            appOpenAd = null;
        }

        AdRequest request = new AdRequest();

        AppOpenAd.Load(adUnitId, request, (AppOpenAd ad, LoadAdError error) =>
        {
            if (error != null)
            {
                Debug.Log("App Open Ad Failed to Load: " + error.GetMessage());
                return;
            }

            if (ad == null)
            {
                Debug.Log("App Open Ad is null after loading.");
                return;
            }

            appOpenAd = ad;

            appOpenAd.OnAdFullScreenContentClosed += () =>
            {
                isShowingAd = false;
                LoadAd();
            };
        });
    }

    public static void ShowAdIfAvailable()
    {
        if (appOpenAd != null && !isShowingAd)
        {
            appOpenAd.Show();
            isShowingAd = true;
        }
        else
        {
            Debug.Log("No App Open Ad Available");
        }
    }
}