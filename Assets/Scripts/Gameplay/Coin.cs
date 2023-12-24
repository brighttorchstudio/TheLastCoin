using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Coin : MonoBehaviour
{
    public CoinBoard coinBoard;
    public int i, j;
    public List<GameObject> validPositions;
    public List<GameObject> passedOverCoins;
    
    public void SetIndex(int i, int j)
    {
        this.i = i;
        this.j = j;
    }

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }

    public bool IsEnabled()
    {
        return GetComponent<SpriteRenderer>().enabled;
    }

    public void Disable()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
    public void Enable()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void GetValidPositions()
    {
        validPositions = new List<GameObject>();
        passedOverCoins = new List<GameObject>();
        if (i - 2 >= 0)
        {
            if (coinBoard.Positions[i - 1, j].GetComponent<Coin>().IsEnabled() == true &&
                coinBoard.Positions[i - 2, j].GetComponent<Coin>().IsEnabled() == false)
            {
                validPositions.Add(coinBoard.Positions[i - 2, j]);
                passedOverCoins.Add(coinBoard.Positions[i - 1, j]);
            }
        }
        if (j - 2 >= 0)
        {
            if (coinBoard.Positions[i, j - 1].GetComponent<Coin>().IsEnabled() == true &&
                coinBoard.Positions[i, j - 2].GetComponent<Coin>().IsEnabled() == false)
            {
                validPositions.Add(coinBoard.Positions[i, j - 2]);
                passedOverCoins.Add(coinBoard.Positions[i, j - 1]);
            }
        }
        if (j + 2 < coinBoard.columns)
        {
            if (coinBoard.Positions[i, j + 1].GetComponent<Coin>().IsEnabled() == true &&
                coinBoard.Positions[i, j + 2].GetComponent<Coin>().IsEnabled() == false)
            {
                validPositions.Add(coinBoard.Positions[i, j + 2]);
                passedOverCoins.Add(coinBoard.Positions[i, j + 1]);
            }
        }
        if (i + 2 < coinBoard.rows)
        {
            if (coinBoard.Positions[i + 1, j].GetComponent<Coin>().IsEnabled() == true &&
                coinBoard.Positions[i + 2, j].GetComponent<Coin>().IsEnabled() == false)
            {
                validPositions.Add(coinBoard.Positions[i + 2, j]);
                passedOverCoins.Add(coinBoard.Positions[i + 1, j]);
            }
        }
    }

    public void ShowValidPositions()
    {
        for (int i = 0; i < validPositions.Count; i++)
        {
            coinBoard.yellowSquares[i].SetActive(true);
            coinBoard.yellowSquares[i].transform.position = validPositions[i].transform.position;
        }
    }

}
