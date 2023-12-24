using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
    private TMP_Text _text;
    public string englishText;
    public string vietnameseText;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        if (PlayerPrefs.GetInt("Language") == 1)
        {
            _text.text = englishText;
        }
        else
        {
            _text.text = vietnameseText;
        }
    }

    public void ToggleLanguage()
    {
        if (gameObject.activeInHierarchy)
        {
            if (PlayerPrefs.GetInt("Language") == 1)
            {
                _text.text = englishText;
            }
            else
            {
                _text.text = vietnameseText;
            }
        }
    }
}
