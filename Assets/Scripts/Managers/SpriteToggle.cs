using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteToggle : MonoBehaviour
{
    public AudioManager audioManager;

    public string toggleName;
    public Sprite onSprite;
    public Sprite offSprite;
    public bool toggleValue = true;

    private Image image;
    private Button button;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();

        if (PlayerPrefs.HasKey(toggleName + "_value"))
        {
            if (PlayerPrefs.GetInt(toggleName + "_value") == 1)
            {
                toggleValue = true;
            }
            else
            {
                toggleValue = false;
            }
        }
        if (toggleValue)
        {
            if(toggleName == "Language")
            {
                if (!PlayerPrefs.HasKey("Language"))
                {
                    if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    {
                        image.sprite = offSprite;
                        PlayerPrefs.SetInt(toggleName, 0);
                        PlayerPrefs.SetInt(toggleName + "_value", 0);
                    }
                    else
                    {
                        image.sprite = onSprite;
                        PlayerPrefs.SetInt(toggleName, 1);
                        PlayerPrefs.SetInt(toggleName + "_value", 1);
                    }
                }
                else
                {
                    image.sprite = onSprite;
                    PlayerPrefs.SetInt(toggleName, 1);
                    PlayerPrefs.SetInt(toggleName + "_value", 1);
                }
            }
            else
            {
                image.sprite = onSprite;
                PlayerPrefs.SetInt(toggleName, 1);
                PlayerPrefs.SetInt(toggleName + "_value", 1);
            }

        }
        else
        {
            image.sprite = offSprite;
            PlayerPrefs.SetInt(toggleName,0);
            PlayerPrefs.SetInt(toggleName + "_value",0);
        }

    }


    public void Toggle()
    {
        audioManager.PlayButtonTouchingSound();
        toggleValue = !toggleValue;
        if (toggleValue)
        {
            image.sprite = onSprite;
            PlayerPrefs.SetInt(toggleName,1);
            PlayerPrefs.SetInt(toggleName + "_value",1);
        }
        else
        {
            image.sprite = offSprite;
            PlayerPrefs.SetInt(toggleName,0);
            PlayerPrefs.SetInt(toggleName + "_value",0);
        }
        button.targetGraphic = image;
    }
}
