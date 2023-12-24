using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetFinalCoins : MonoBehaviour
{
    private TMP_Text coinText;
    private void Start()
    {
        coinText = GetComponent<TMP_Text>();
        coinText.SetText(PlayerPrefs.GetInt("Final coins").ToString());
    }
}
