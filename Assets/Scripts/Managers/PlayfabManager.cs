using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayfabManager : MonoBehaviour
{
    public GameObject loadingWindow;

    public GameObject nameWindow;
    public GameObject nameError;

    public GameObject errorWindow;
    public GameObject dataWindow;

    public TMP_InputField inputField;

    public Transform leaderboard;
    public GameObject topPrefab;

    public Transform playerRankingTransform;
    public GameObject playerRankingPrefab;

    public Button refreshButton;
    void Start()
    {
        
        StartCoroutine(ConnectToServer());
    }


    IEnumerator ConnectToServer()
    {
        errorWindow.SetActive(false);
        loadingWindow.SetActive(true);
        UnityWebRequest request = new UnityWebRequest("https://google.com");
        yield return request.SendWebRequest();

        if(request.error != null)
        {
            loadingWindow.SetActive(false);
            errorWindow.SetActive(true);
        }
        else
        {
            errorWindow.SetActive(false);
            Login();
            
        }

    }

    public void ReconnectToServer()
    {
        StartCoroutine(ConnectToServer());
    }
    
    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccessLogin, OnError);
    }

    void OnSuccessLogin(LoginResult result)
    {
        print("login successful");

        SendLeaderBoard();

        string name = null;

        if(result.InfoResultPayload.PlayerProfile != null)
        {
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
        }
        
        if(name == null)
        {
            loadingWindow.SetActive(false);
            nameWindow.SetActive(true);
        }
        else
        {
            loadingWindow.SetActive(true);
            GetLeaderBoard();
            
        }
        
    }

    void OnError(PlayFabError error)
    {
        print("error");
        print(error.GenerateErrorReport());
    }


    public void SendLeaderBoard()
    {
        if(PlayerPrefs.HasKey("Final coins"))
        {
            int coins = PlayerPrefs.GetInt("Final coins");
            if(coins != 0)
            {
                var request = new UpdatePlayerStatisticsRequest
                {
                    Statistics = new List<StatisticUpdate>
                    {
                        new StatisticUpdate
                        {
                            StatisticName = "TheCoinquerers",
                            Value = coins
                        }
                    }
                };
                PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
            }
        }
    }

    public void LoadingData()
    {
        refreshButton.interactable = false;
        loadingWindow.SetActive(true);
        dataWindow.SetActive(false );
    }

    void DoneLoadingData()
    {
        refreshButton.interactable = true;
        loadingWindow.SetActive(false);
        dataWindow.SetActive(true);
    }

    public void GetLeaderBoard()
    {
        LoadingData();
        GetPlayerInformation();
        var request = new GetLeaderboardRequest
        {
            StatisticName = "TheCoinquerers",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void GetPlayerInformation()
    {
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = "TheCoinquerers",
            MaxResultsCount = 1
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnLeaderboardPlayerGet, OnError);
    }

    void OnLeaderboardPlayerGet(GetLeaderboardAroundPlayerResult result)
    {
        foreach (Transform item in playerRankingTransform)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in result.Leaderboard)
        {
            GameObject newTop = Instantiate(playerRankingPrefab, playerRankingTransform);
            TMP_Text[] texts = newTop.GetComponentsInChildren<TMP_Text>();

            if(item.StatValue == 0)
            {
                texts[0].text = "_";
            }
            else
            {
                texts[0].text = (item.Position + 1).ToString();
            }
            
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();
        }
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach(Transform item in leaderboard)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in result.Leaderboard)
        {
            GameObject newTop = Instantiate(topPrefab, leaderboard);
            TMP_Text[] texts = newTop.GetComponentsInChildren<TMP_Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();  
        }
        DoneLoadingData();
        print("data get");
        
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        print("data sent");
        
    }


    public void SubmitName()
    {
        string name = inputField.text;

        if (CheckName(name))
        {
            var request = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = name,
            };
            PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
        }
        else
        {
            nameError.SetActive(true);
        }

    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        print("updated display name");
        nameWindow.SetActive(false);

       
        GetLeaderBoard();
       
    }

    bool CheckName(string name)
    {
        if(name != null && name.Trim().Length > 0)
        {
            return true;
        }
        return false;
    }

}
