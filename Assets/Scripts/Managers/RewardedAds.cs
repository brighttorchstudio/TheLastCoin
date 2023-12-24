using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class RewardedAds : MonoBehaviour, IUnityAdsShowListener
{
    public GameObject loseUI;
    public GameObject rewardedAdsUI;
    public SceneLoadingManager sceneLoadingManager;
    public GameObject errorWindow;
    public GameObject loadingWindow;

    private string adUnitID = "Rewarded_Android";

    

    IEnumerator InitializeRewardedAds()
    {
        sceneLoadingManager.CanvasGroupUninterractable();
        loadingWindow.SetActive(true);
        UnityWebRequest request = new UnityWebRequest("https://google.com");
        yield return request.SendWebRequest();

        if (request.error != null)
        {
            sceneLoadingManager.CanvasGroupInterractable();
            errorWindow.SetActive(true);
            loadingWindow.SetActive(false);
        }
        else
        {
            InitializeAds.Instance.Initialize();
            yield return new WaitUntil(InitializeAds.Instance.checkInitialized);

            if (InitializeAds.Instance.isInitialized)
            {
                ShowAds();
            }
            else
            {
                sceneLoadingManager.CanvasGroupInterractable();
                errorWindow.SetActive(true);
                loadingWindow.SetActive(false);
            }
        }

    }

    
    public void HideErrorWindow()
    {
        errorWindow.SetActive(false);
    }

    public void ShowRewardedAds() 
    {
        StartCoroutine(InitializeRewardedAds());
    }


    //show ads

    void ShowAds()
    {
        Advertisement.Show(adUnitID, this);
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        sceneLoadingManager.CanvasGroupInterractable();
        errorWindow.SetActive(true);
        loadingWindow.SetActive(false);
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        loadingWindow.SetActive(false);
        rewardedAdsUI.SetActive(false);
        loseUI.SetActive(false);
        InitializeAds.Instance.LoadAds();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if(adUnitID.Equals(placementId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            int level_to_load = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("level_to_load", level_to_load);
            sceneLoadingManager.LoadSceneWithBuildIndex(level_to_load);
        }
    }
}
