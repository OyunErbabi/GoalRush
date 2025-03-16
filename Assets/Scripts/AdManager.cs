using System.Collections;
using UnityEngine;
using GoogleMobileAds.Ump.Api;
using GoogleMobileAds.Api;
using Unity.Services.RemoteConfig;
using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Advertisement.IosSupport;


public class AdManager : MonoBehaviour
{
    public static AdManager Instance;

    public struct userAttributes { }
    public struct appAttributes { }

    private BannerView LowerBannerView;
    private InterstitialAd _interstitialAd;
    string InterstitialAdUnitId;
    string BannerAdUnitId;

    bool TestAds;

    public GameObject DebugConsole;
    async Task Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            return;
        }

        if (Utilities.CheckForInternetConnection())
        {
            await InitializeRemoteConfigAsync();
        }

        RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;
        await RemoteConfigService.Instance.FetchConfigsAsync(new userAttributes(), new appAttributes());
    }

    async Task InitializeRemoteConfigAsync()
    {
        await UnityServices.InitializeAsync();

        //if (!AuthenticationService.Instance.IsSignedIn)
        //{
        //    await AuthenticationService.Instance.SignInAnonymouslyAsync();
        //}
    }

    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        switch (configResponse.requestOrigin)
        {
            case ConfigOrigin.Default:
                Debug.Log("No Settings Loaded!");
                break;
            case ConfigOrigin.Cached:
                Debug.Log("No Settings Loaded! Cached!");
                break;
            case ConfigOrigin.Remote:

                if (RemoteConfigService.Instance.appConfig.GetBool("DebugConsole"))
                {
                    DebugConsole.SetActive(true);
                }

                //StartAds();
                StartCoroutine(RequestATT());
                break;
        }
    }

    void StartAds()
    {

        //StartCoroutine(RequestATT());

        TestAds = RemoteConfigService.Instance.appConfig.GetBool("TestAds");

        //Debug.Log("TestAds: " + TestAds);


        if (TestAds)
        {
            InterstitialAdUnitId = "ca-app-pub-3940256099942544/1033173712";
            BannerAdUnitId = "ca-app-pub-3940256099942544/6300978111";
        }
        else
        {
#if UNITY_EDITOR
            InterstitialAdUnitId = "ca-app-pub-3940256099942544/1033173712";
            BannerAdUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_ANDROID
                                    InterstitialAdUnitId = "ca-app-pub-6598748819351230/6876060221";
                                    BannerAdUnitId = "ca-app-pub-6598748819351230/2940684643";
#elif UNITY_IPHONE
                                    InterstitialAdUnitId = "ca-app-pub-6598748819351230/9956684980";
                                    BannerAdUnitId = "ca-app-pub-6598748819351230/9266124695";
#endif
        }


        //var DebugSettings = new ConsentDebugSettings
        //{
        //    DebugGeography = DebugGeography.EEA,
        //    TestDeviceHashedIds = new List<string>
        //    {
        //        "B7341B9D32A55780DFD5D984859DF50E"
        //    }
        //};

        ConsentRequestParameters request = new ConsentRequestParameters
        {
            TagForUnderAgeOfConsent = false,
            //ConsentDebugSettings = DebugSettings,
        };

        ConsentInformation.Update(request, OnConsentInfoUpdated);
    }

    private void RequestInterstitial()
    {
        LoadInterstitialAd();
    }

    public void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(InterstitialAdUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                _interstitialAd = ad;
                RegisterReloadHandler(ad);
            });
    }

    private void RegisterReloadHandler(InterstitialAd interstitialAd)
    {
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            //SoundManager.Instance.Mute(true);
        };
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial Ad full screen content closed.");

            // Reload the ad so that we can show another as soon as possible.
            LoadInterstitialAd();
            //SoundManager.Instance.Mute(false);
        };
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            LoadInterstitialAd();
        };
    }

    public void ShowInterstitialAd()
    {
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            _interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }

    private void RequestBanner()
    {
        Debug.Log("RequestBanner");


        // Clean up banner ad before creating a new one.
        if (LowerBannerView != null)
        {
            LowerBannerView.Destroy();
        }

        AdSize adaptiveSize =
                AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        LowerBannerView = new BannerView(BannerAdUnitId, adaptiveSize, AdPosition.Bottom);

        // Register for ad events.
        LowerBannerView.OnBannerAdLoaded += OnBannerAdLoaded;
        LowerBannerView.OnBannerAdLoadFailed += OnBannerAdLoadFailed;

        AdRequest adRequest = new AdRequest();

        // Load a banner ad.
        LowerBannerView.LoadAd(adRequest);
    }

    private void OnBannerAdLoaded()
    {
        Debug.Log("Banner view loaded");
        LowerBannerView.Show();
    }

    private void OnBannerAdLoadFailed(LoadAdError error)
    {
        Debug.LogError("Banner view failed to load an ad with error : "
                + error);
    }

    private void OnConsentInfoUpdated(FormError error)
    {
        if (error != null)
        {
            Debug.Log("ConsentInfoUpdated error: " + error.Message);
            return;
        }

        if (ConsentInformation.IsConsentFormAvailable())
        {
            LoadConsentForum();
        }
        else
        {
            MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                RequestBanner();
                RequestInterstitial();
            });
        }

    }

    private void LoadConsentForum()
    {
        ConsentForm.Load(OnConsentFormLoaded);
    }

    private void OnConsentFormLoaded(ConsentForm form, FormError formError)
    {
        if (formError != null)
        {
            return;
        }

        ConsentForm m_consentForm = form;

        if (ConsentInformation.ConsentStatus == ConsentStatus.Required)
        {
            m_consentForm.Show(OnShowForm);
        }
        else
        {
            MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                RequestBanner();
                RequestInterstitial();
            });
        }
    }

    private void OnShowForm(FormError error)
    {
        if (error != null)
        {
            Debug.Log("ShowForm error: " + error.Message);
            return;
        }
        LoadConsentForum();
    }

    IEnumerator RequestATT()
    {
        if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() ==
         ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            yield return new WaitForSeconds(1.0f);
            ATTrackingStatusBinding.RequestAuthorizationTracking();
            
            yield return new WaitUntil(() => ATTrackingStatusBinding.GetAuthorizationTrackingStatus() !=
                ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED);

            
            if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() !=
                ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                StartAds();
            }
        }
        else
        {
           
            StartAds();
        }
    }

}