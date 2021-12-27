using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool freezeTiles = false;
    private int coins = 0;
    private int tiles = 0;
    private int meters = 0;
    public static GameManager inst;

    public GameData gameData;
    [SerializeField] private Text coinText;
    [SerializeField] private Text meterText;

    private void Awake()
    {
        inst = this;
    }

    public void IncScore()
    {
        ++coins;
        coinText.text = "" + coins;
    }

    public void IncTiles()
    {
        if (!freezeTiles)
        {
            ++tiles;
        }
    }

    public void FreezeTiles()
    {
        freezeTiles = true;
    }
    
    public void IncMeters(float distance)
    {
        meters = (int) distance/3;
        meterText.text = "" + meters + "m";
    }

    public void QuitGame()
    {
        /*
        var highScore = PlayerPrefs.GetInt("highScore", 0);
        var totalCoins = PlayerPrefs.GetInt("coins", 0);
        totalCoins += coins;
        if (meters > highScore)
        {
            PlayerPrefs.SetInt("highScore", meters);
        }
        PlayerPrefs.SetInt("lastScore", meters);
        PlayerPrefs.SetInt("totalCoins", totalCoins);
        PlayerPrefs.Save();
        */
        gameData.setCoins(coins);
        gameData.setMeters(meters);
        SceneManager.LoadScene("Menu");
    }

}
