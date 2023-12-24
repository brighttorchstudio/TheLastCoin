using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guide1 : MonoBehaviour
{
    public GameObject guideZero;
    public GameObject guide0;
    public GameObject guide1;
    public GameObject guide2;
    public GameObject guide3;
    public GameObject guide4;
    public GameObject guide6;
    public GameObject guide7;
    public GameObject guide8;
    public GameObject guide10;
    public GameObject guide11;

    public GameObject guide12;
    public GameObject guide13;
    public GameObject guide14;
    public GameObject guide15;
    public GameObject guide16;
    public GameObject guide17;
    public GameObject guide18;

    public GameObject guide20;
    public GameObject guide21;
    public GameObject guide22;


    private void Start()
    {
        StartCoroutine(waitGuide0());
    }

    IEnumerator waitGuide0()
    {
        yield return new WaitForSeconds(1f);
        guide0.SetActive(true);
        guideZero.SetActive(false);
    }

    public void ShowGuide1()
    {
        guide0.SetActive(false);
        guide1.SetActive(true);
    }
    public void ShowGuide2()
    {
        guide1.SetActive(false);
        guide2.SetActive(true);
    }
    public void ShowGuide3()
    {
        guide2.SetActive(false);
        guide3.SetActive(true);
    }
    public void ShowGuide4()
    {
        guide3.SetActive(false);
        guide4.SetActive(true);
    }

    public void ShowGuide6()
    {
        guide4.SetActive(false);
        guide6.SetActive(true);
    }
    public void ShowGuide7()
    {
        guide6.SetActive(false);
        guide7.SetActive(true);
    }
    public void ShowGuide8()
    {
        guide7.SetActive(false);
        guide8.SetActive(true);
    }
    public void ShowGuide10()
    {
        guide8.SetActive(false);
        guide10.SetActive(true);
    }
    public void ShowGuide11()
    {
        guide10.SetActive(false);
        guide11.SetActive(true);
    }

    public void ShowGuide12()
    {
        guide11.SetActive(false);
        guide12.SetActive(true);
    }

    public void ShowGuide13()
    {
        guide12.SetActive(false);
        guide13.SetActive(true);
    }

    public void ShowGuide14()
    {
        guide13.SetActive(false);
        guide14.SetActive(true);
    }

    public void ShowGuide15()
    {
        guide14.SetActive(false);
        guide15.SetActive(true);
    }

    public void ShowGuide16()
    {
        guide15.SetActive(false);
        guide16.SetActive(true);
    }

    public void ShowGuide17()
    {
        guide16.SetActive(false);
        guide17.SetActive(true);
    }

    public void ShowGuide18()
    {
        guide17.SetActive(false);
        guide18.SetActive(true);
    }

    
    public void ShowGuide20()
    {
        guide18.SetActive(false);
        guide20.SetActive(true);
    }
    public void ShowGuide21()
    {
        guide20.SetActive(false);
        guide21.SetActive(true);
    }

    public void ShowGuide22()
    {
        guide21.SetActive(false);
        guide22.SetActive(true);
    }

    public void HasBeenGuided()
    {
        PlayerPrefs.SetString("Has_been_guided", "true");
    }

}
