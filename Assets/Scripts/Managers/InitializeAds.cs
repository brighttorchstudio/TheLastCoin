using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Networking;

public class InitializeAds : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener
{
    private string androidGameID = "5502707";
    private string adUnitID = "Rewarded_Android";
    public static InitializeAds Instance {  get; private set; }

    public bool isInitialized;
    public bool isInitializing;

    //dont destroy onload
    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Initialize();
    }

   public void Initialize()
    {
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            isInitializing = true;
            Advertisement.Initialize(androidGameID, false, this);
        }
        else
        {
            isInitializing = false;
        }
    }

    public bool checkInitialized()
    {
        if(isInitializing)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void LoadAds()
    {
        Advertisement.Load(adUnitID, this);
    }

    public void OnInitializationComplete()
    {
        LoadAds();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        isInitialized = false;
        isInitializing = false;
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        isInitialized = true;
        isInitializing = false;
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        isInitialized = false;
        isInitializing = false;
    }
}
