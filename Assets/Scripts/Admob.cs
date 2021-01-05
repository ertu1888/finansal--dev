using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using GoogleMobileAds;

public class Admob : MonoBehaviour
{
    // banner
    public string BannerAndroidID = "";
    private BannerView banner;
    
    // Interstitial
    public string IntersAndroidID = "";
    private InterstitialAd inter;

    public int count = 1;

    public void Start()
    {
        this.RequestBanner();
        this.Request();
    }

    private void RequestBanner()
    {

#if UNITY_ANDROID
        //string id = androidID;
#elif UNITY_IPHONE
        //string id = "ca-app-pub-3940256099942544/2934735716";
#else
        string id = "unexpected_platform";
#endif

        banner = new BannerView(BannerAndroidID, AdSize.SmartBanner, AdPosition.Bottom);


        //AdRequest request = new AdRequest.Builder().AddTestDevice("33BE2250B43518CCDA7DE426D04EE231").Build();
        AdRequest request = new AdRequest.Builder().Build();

        banner.LoadAd(request);
        banner.Show();
    }

    public void ShowBanner()
    {
        if (count % 2 == 0)
        {
            banner.Show();
        }
        else
        {
            banner.Hide();
        }

        count++;
    }

    private void Request()
    {
        this.inter = new InterstitialAd(IntersAndroidID);

        this.inter.OnAdClosed += InterstitialClosed;

        AdRequest request = new AdRequest.Builder().Build();

        this.inter.LoadAd(request);
    }

    private void InterstitialClosed(object sender, EventArgs e)
    {
        this.Request();
    }

    public void Show()
    {
        this.inter.Show();
    }
}
