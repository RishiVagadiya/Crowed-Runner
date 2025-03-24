using UnityEngine;
using GoogleMobileAds.Api;

public class BannerAdManager : MonoBehaviour
{
    private BannerView bannerView;

    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        RequestBanner();
    }

    private void RequestBanner()
    {
        string bannerAdUnitId = "ca-app-pub-6801434645272696/5784811156"; // तुम्हारी Ad Unit ID

        bannerView = new BannerView(bannerAdUnitId, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest(); // ✅ सही तरीका
        bannerView.LoadAd(request);
    }
}
