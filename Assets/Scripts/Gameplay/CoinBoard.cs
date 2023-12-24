using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CoinBoard : MonoBehaviour
{
    public GameObject rewardedAdsUI;

    public AudioManager audioManager;
    public SavingManager savingManager;

    public GameObject winUI;
    public GameObject loseUI;

    public GameObject[,] Positions;
    public int rows, columns;
    public GameObject blueSquare;
    public List<GameObject> yellowSquares;

    private List<GameObject> _enabledCoins;
    private int _touchCount;
    private bool _canTouch;
    private bool _startMoving;
    private bool _canContinue;
    private Coin _firstSelectedCoin, _secondSelectedCoin;
    private Vector2 coinPos1, coinPos2;



    private void Start()
    {
        ResetBoard();
        if (savingManager.HasCurrentLevelBeenSaved() == false)
        {
            NewBoard();
        }
        else
        {
            LoadBoard();
        }
    }

    public void DisableTouch()
    {
        _canTouch = false;
    }

    private void Update()
    {
        if (Input.touchCount > 0 && _canTouch)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition3D = Camera.main.ScreenToWorldPoint(touch.position);
                Vector2 touchPosition2D = new Vector2(touchPosition3D.x, touchPosition3D.y);
                RaycastHit2D hit = Physics2D.Raycast(touchPosition2D, Camera.main.transform.forward);
                if (hit.collider)
                {
                    _touchCount++;
                    GameObject hitObject = hit.collider.gameObject;
                    Coin coin = hitObject.GetComponent<Coin>();
                    if (_touchCount == 1)
                    {
                        if (coin.IsEnabled())
                        {
                            audioManager.PlayCoinTouchingSound();
                            GetCoin(ref _firstSelectedCoin, ref coinPos1, coin);
                            _firstSelectedCoin.ShowValidPositions();
                        }
                        else
                        {
                            ResetBoard();
                        }
                    }
                    if (_touchCount == 2)
                    {
                        if (!coin.IsEnabled())
                        {
                            if (_firstSelectedCoin.validPositions.Contains(coin.gameObject))
                            {
                                audioManager.PlayCoinTouchingSound();
                                GetCoin(ref _secondSelectedCoin, ref coinPos2, coin);
                                DisablePassedOverCoin();

                                _startMoving = true;
                                _canTouch = false;
                                DisableYellowSquares();
                            }
                            else
                            {
                                ResetBoard();
                            }
                        }
                        else
                        {
                            audioManager.PlayCoinTouchingSound();
                            DisableYellowSquares();
                            _touchCount = 1;
                            GetCoin(ref _firstSelectedCoin, ref coinPos1, coin);
                            _firstSelectedCoin.ShowValidPositions();
                        }
                    }
                }
                else
                {
                    ResetBoard();
                }
            }
        }

        if (_startMoving)
        {
            _firstSelectedCoin.transform.position = Vector2.MoveTowards(_firstSelectedCoin.transform.position,coinPos2, 0.1f);
            _secondSelectedCoin.transform.position = Vector2.MoveTowards(_secondSelectedCoin.transform.position,coinPos1, 0.1f);
            if (Vector2.Distance(_firstSelectedCoin.transform.position, coinPos2) == 0 && Vector2.Distance(_secondSelectedCoin.transform.position, coinPos1) == 0)
            {
                Swap2Coins();
                if (_enabledCoins.Count == 1)
                {
                    savingManager.IncreaseTotalCoins();
                    
                    audioManager.PlayWinSound();
                    ResetBoard();
                    savingManager.PassTheCurrentLevel();
                    winUI.SetActive(true);
                    _canTouch = false;
                }
                else
                {
                    UpdateEnabledCoinsValidPositions();
                    if (!_canContinue)
                    {
                        int totalCoins = int.Parse(savingManager.totalCoinsText.text) - int.Parse(savingManager.bonusCoinsText.text);
                        if (SceneManager.GetActiveScene().buildIndex != 1 && totalCoins < 0)
                        {
                            rewardedAdsUI.SetActive(true);
                        }
                        savingManager.DecreaseTotalCoins();
                      
                        audioManager.PlayLoseSound();
                        ResetBoard();
                        loseUI.SetActive(true);
                        
                        _canTouch = false;
                    }
                    else
                    {
                        ResetBoard();
                    }
                }
            }
        }
    }


    void ResetBoard()
    {
        _startMoving = false;
        _canTouch = true;
        _touchCount = 0;
        blueSquare.SetActive(false);
        DisableYellowSquares();
    }

    void DisableYellowSquares()
    {
        foreach (GameObject sprite in yellowSquares)
        {
            sprite.SetActive(false);
        }
    }

    void NewBoard()
    {
        Positions = new GameObject[rows,columns];
        _enabledCoins = new List<GameObject>();
        int k = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Coin coin = transform.GetChild(k).GetComponent<Coin>();
                coin.SetIndex(i,j);
                savingManager.SaveCoin(coin);
                if (coin.IsEnabled())
                {
                    _enabledCoins.Add(coin.gameObject);
                }
                Positions[i, j] = coin.gameObject;

                k++;
            }
        }
        savingManager.SaveCurrentLevel();
        UpdateEnabledCoinsValidPositions();
    }

    void LoadBoard()
    {
        Positions = new GameObject[rows,columns];
        _enabledCoins = new List<GameObject>();

        int k = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Coin coin = transform.GetChild(k).GetComponent<Coin>();
                savingManager.LoadCoin(coin);
                if (coin.IsEnabled())
                {
                    _enabledCoins.Add(coin.gameObject);
                }

                Positions[coin.i, coin.j] = coin.gameObject;

                k++;
            }
        }
        UpdateEnabledCoinsValidPositions();
    }




    void UpdateEnabledCoinsValidPositions()
    {
        int count = 0;
        foreach (GameObject coin in _enabledCoins)
        {
            coin.GetComponent<Coin>().GetValidPositions();
            if (coin.GetComponent<Coin>().validPositions.Count != 0)
            {
                count++;
            }
        }
        if (count == 0)
        {
            _canContinue = false;
        }
        else
        {
            _canContinue = true;
        }
    }


    void GetCoin(ref Coin coin, ref Vector2 position, Coin coinToGet)
    {
        coin = coinToGet;
        position = coinToGet.transform.position;
        blueSquare.SetActive(true);
        blueSquare.transform.position = position;
    }


    void DisablePassedOverCoin()
    {
        int index = _firstSelectedCoin.validPositions.IndexOf(_secondSelectedCoin.gameObject);
        Coin coinToBeDisabled = _firstSelectedCoin.passedOverCoins[index].GetComponent<Coin>();

        coinToBeDisabled.Disable();
        _enabledCoins.Remove(coinToBeDisabled.gameObject);
        savingManager.SaveCoin(coinToBeDisabled);
        savingManager.GetOneBonusCoin();
    }

    void Swap2Coins()
    {
        GameObject temp = Positions[_firstSelectedCoin.i, _firstSelectedCoin.j];
        Positions[_firstSelectedCoin.i, _firstSelectedCoin.j] = Positions[_secondSelectedCoin.i, _secondSelectedCoin.j];
        Positions[_secondSelectedCoin.i, _secondSelectedCoin.j] = temp;

        int tempI = _firstSelectedCoin.i;
        int tempJ = _firstSelectedCoin.j;
        _firstSelectedCoin.SetIndex(_secondSelectedCoin.i, _secondSelectedCoin.j);
        _secondSelectedCoin.SetIndex(tempI,tempJ);

        savingManager.SaveCoin(_firstSelectedCoin);
        savingManager.SaveCoin(_secondSelectedCoin);

    }


}
