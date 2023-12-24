using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    
    public AudioSource audioSource;

    
    public AudioClip coinTouching;
    public AudioClip buttonTouching;
    public AudioClip win;
    public AudioClip lose;

    private void Start()
    {
        ToggleVolume();
    }

    public void PlayButtonTouchingSound()
    {
        audioSource.PlayOneShot(buttonTouching);
    }

    public void PlayCoinTouchingSound()
    {
        audioSource.PlayOneShot(coinTouching);
    }

    public void PlayWinSound()
    {
        audioSource.PlayOneShot(win);
    }

    public void PlayLoseSound()
    {
        audioSource.PlayOneShot(lose);
    }

    

    

    public void ToggleVolume()
    {
        if (PlayerPrefs.GetInt("Volume") == 1)
        {
            audioSource.mute = false;
        }
        else
        {
            audioSource.mute = true;
        }
    }

    





}
