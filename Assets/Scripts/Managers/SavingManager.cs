using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavingManager : MonoBehaviour
{
    public TMP_Text totalCoinsText;
    public TMP_Text bonusCoinsText;
    private string _currentLevelName;
    private int _currentLevelIndex;


    private void Start()
    {
        _currentLevelName = SceneManager.GetActiveScene().name;
        _currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

        if (!PlayerPrefs.HasKey("Total coins") && !PlayerPrefs.HasKey("Bonus coins"))
        {
            totalCoinsText.SetText("0");
            bonusCoinsText.SetText("+0");
            SaveTotalCoinsAndBonusCoins();
        }
        else
        {
            string totalCoins = PlayerPrefs.GetString("Total coins");
            string bonusCoins = PlayerPrefs.GetString("Bonus coins");
            totalCoinsText.SetText(totalCoins);
            bonusCoinsText.SetText(bonusCoins);
        }

    }

    public void SaveCurrentLevel()
    {
        PlayerPrefs.SetInt("level_to_load", _currentLevelIndex);
        PlayerPrefs.SetString(_currentLevelName + "_" + "hasBeenSaved", "true");
    }

    public void PassTheCurrentLevel()
    {
        string totalCoins = PlayerPrefs.GetString("Total coins");
        string bonusCoins = PlayerPrefs.GetString("Bonus coins");

        int volume = PlayerPrefs.GetInt("Volume");
        int language = PlayerPrefs.GetInt("Language");

        int volumeToggleValue = PlayerPrefs.GetInt("Volume_value");
        int languageToggleValue = PlayerPrefs.GetInt("Language_value");

        int finalCoins = 0;
        if (PlayerPrefs.HasKey("Final coins"))
        {
            finalCoins = PlayerPrefs.GetInt("Final coins");
        }

        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt("level_to_load", _currentLevelIndex + 1);
        PlayerPrefs.SetString("Total coins",totalCoins);
        PlayerPrefs.SetString("Bonus coins",bonusCoins);

        PlayerPrefs.SetInt("Volume",volume);
        PlayerPrefs.SetInt("Language",language);
        PlayerPrefs.SetInt("Volume_value",volumeToggleValue);
        PlayerPrefs.SetInt("Language_value",languageToggleValue);

        if(PlayerPrefs.GetInt("level_to_load") == 21)
        {
            PlayerPrefs.SetInt("Final coins", int.Parse(PlayerPrefs.GetString("Total coins")));
        }
        else
        {
            PlayerPrefs.SetInt("Final coins", finalCoins);
        }

        if(PlayerPrefs.GetInt("Final coins") <= finalCoins)
        {
            PlayerPrefs.SetInt("Final coins", finalCoins);
        }
        PlayerPrefs.SetString("Has_been_guided", "true");
    }



    public bool HasCurrentLevelBeenSaved()
    {
        if (PlayerPrefs.HasKey(_currentLevelName + "_" + "hasBeenSaved"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SaveCoin(Coin coin)
    {
        Vector2 position = coin.transform.position;

        //save coin position
        PlayerPrefs.SetFloat(_currentLevelName + "_" + coin.name + "_" + "positionX", position.x);
        PlayerPrefs.SetFloat(_currentLevelName + "_" + coin.name + "_" + "positionY", position.y);
        //save index
        PlayerPrefs.SetInt(_currentLevelName + "_" + coin.name + "_" + "i", coin.i);
        PlayerPrefs.SetInt(_currentLevelName + "_" + coin.name + "_" + "j", coin.j);
        //save state
        if (coin.IsEnabled())
        {
            PlayerPrefs.SetString(_currentLevelName + "_" + coin.name + "_" + "enabled", "true");
        }
        else
        {
            PlayerPrefs.SetString(_currentLevelName + "_" + coin.name + "_" + "enabled", "false");
        }

    }

    public void LoadCoin(Coin coin)
    {
        Vector2 position = new Vector2(PlayerPrefs.GetFloat(_currentLevelName + "_" + coin.name + "_" + "positionX"),
            PlayerPrefs.GetFloat(_currentLevelName + "_" + coin.name + "_" + "positionY"));
        coin.SetPosition(position);

        int i, j;
        i = PlayerPrefs.GetInt(_currentLevelName + "_" + coin.name + "_" + "i");
        j = PlayerPrefs.GetInt(_currentLevelName + "_" + coin.name + "_" + "j");
        coin.SetIndex(i,j);

        if (PlayerPrefs.HasKey(_currentLevelName + "_" + coin.name + "_" + "enabled"))
        {
            if (PlayerPrefs.GetString(_currentLevelName + "_" + coin.name + "_" + "enabled").Equals("true"))
            {
                coin.Enable();
            }
            else
            {
                coin.Disable();
            }

        }
    }

    public void GetOneBonusCoin()
    {
        string bonusCoins = (int.Parse(bonusCoinsText.text) + 1).ToString();
        bonusCoinsText.SetText("+" + bonusCoins);
        SaveTotalCoinsAndBonusCoins();
    }

    public void IncreaseTotalCoins()
    {
        string totalCoins = (int.Parse(totalCoinsText.text) + int.Parse(bonusCoinsText.text)).ToString();
        totalCoinsText.SetText(totalCoins);
        bonusCoinsText.SetText("+0");
        SaveTotalCoinsAndBonusCoins();
    }

    public void DecreaseTotalCoins()
    {
        int totalCoins = int.Parse(totalCoinsText.text) - int.Parse(bonusCoinsText.text);
        if (totalCoins < 0)
        {
            totalCoinsText.SetText("0");
            bonusCoinsText.SetText("+0");
            PlayerPrefs.SetInt("level_to_load", 1);
            FailTheCurrentLevel();
        }
        else
        {
            totalCoinsText.SetText(totalCoins.ToString());
            bonusCoinsText.SetText("+0");
            FailTheCurrentLevel();
        }
        SaveTotalCoinsAndBonusCoins();
    }

    void FailTheCurrentLevel()
    {
        string totalCoins = PlayerPrefs.GetString("Total coins");
        string bonusCoins = PlayerPrefs.GetString("Bonus coins");
        int level = PlayerPrefs.GetInt("level_to_load");
        int volume = PlayerPrefs.GetInt("Volume");
        int language = PlayerPrefs.GetInt("Language");
        int volumeToggleValue = PlayerPrefs.GetInt("Volume_value");
        int languageToggleValue = PlayerPrefs.GetInt("Language_value");

        int finalCoins = 0;
        if (PlayerPrefs.HasKey("Final coins"))
        {
            finalCoins = PlayerPrefs.GetInt("Final coins");
        }

        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("Total coins",totalCoins);
        PlayerPrefs.SetString("Bonus coins",bonusCoins);

        PlayerPrefs.SetInt("Volume",volume);
        PlayerPrefs.SetInt("Language",language);
        PlayerPrefs.SetInt("Volume_value",volumeToggleValue);
        PlayerPrefs.SetInt("Language_value",languageToggleValue);
        PlayerPrefs.SetInt("Final coins", finalCoins);
        PlayerPrefs.SetInt("level_to_load", level);
        PlayerPrefs.SetString("Has_been_guided", "true");
    }
    public void SaveTotalCoinsAndBonusCoins()
    {
        PlayerPrefs.SetString("Total coins", totalCoinsText.text);
        PlayerPrefs.SetString("Bonus coins", bonusCoinsText.text);
    }




}
