using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : MonoBehaviour
{
    public AudioManager audioManager;
    public CanvasGroup canvasGroup;
    public Animator transitionAnimator;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (!PlayerPrefs.HasKey("level_to_load"))
        {
            PlayerPrefs.SetInt("level_to_load", 1);
        }
    }

    public void CanvasGroupUninterractable()
    {
        canvasGroup.interactable = false;
    }
    public void CanvasGroupInterractable()
    {
        canvasGroup.interactable = true;
    }

    IEnumerator LoadScene(int buildIndex)
    {
        audioManager.PlayButtonTouchingSound();
        canvasGroup.interactable = false;
        transitionAnimator.SetTrigger("LoadLevel");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(buildIndex);
    }

    IEnumerator LoadScene(string sceneName)
    {
        audioManager.PlayButtonTouchingSound();
        canvasGroup.interactable = false;
        transitionAnimator.SetTrigger("LoadLevel");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator QuitGame()
    {
        audioManager.PlayButtonTouchingSound();
        canvasGroup.interactable = false;
        transitionAnimator.SetTrigger("LoadLevel");
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }

    public void Replay()
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
        PlayerPrefs.SetString("Has_been_guided", "true");
        StartCoroutine(LoadScene(level));
    }

    public void LoadSceneWithBuildIndex(int buildIndex)
    {
        StartCoroutine(LoadScene(buildIndex));
    }
    public void LoadSceneWithSceneName(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    public void LoadTheNextLevel()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadSavedLevel()
    {
        if (PlayerPrefs.HasKey("Has_been_guided"))
        {
            StartCoroutine(LoadScene(PlayerPrefs.GetInt("level_to_load")));
        }
        else
        {
            LoadSceneWithSceneName("GuidingScene");
        }
    }

    public void LoadNewGame()
    {
        int volume = PlayerPrefs.GetInt("Volume");
        int language = PlayerPrefs.GetInt("Language");
        int volumeToggleValue = PlayerPrefs.GetInt("Volume_value");
        int languageToggleValue = PlayerPrefs.GetInt("Language_value");
        

        int finalCoins = 0;
        if(PlayerPrefs.HasKey("Final coins"))
        {
            finalCoins = PlayerPrefs.GetInt("Final coins");
        }

        string guided = null;
        if (PlayerPrefs.HasKey("Has_been_guided"))
        {
            guided = "true";
        }

        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt("Volume",volume);
        PlayerPrefs.SetInt("Language",language);
        PlayerPrefs.SetInt("Volume_value",volumeToggleValue);
        PlayerPrefs.SetInt("Language_value",languageToggleValue);
        PlayerPrefs.SetInt("Final coins", finalCoins);

        if(guided == null)
        {
            LoadSceneWithSceneName("GuidingScene");
        }
        else
        {
            StartCoroutine(LoadScene(1));
        }

        
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadScene(0));
    }

    public void Quit()
    {
        StartCoroutine(QuitGame());
    }



}
