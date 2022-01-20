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
    private int stage = 0;
    
    private GameCamera gameCamera;
    public GameData gameData;
    [SerializeField] private Text coinText;
    [SerializeField] private Text meterText;
    [SerializeField] private int maxStages = 1;
    [SerializeField] private int tilesPerStage = 10;

    private void Awake()
    {
        stage = gameData.getStartStage();
        inst = this;
    }

    private void Start()
    {
        Camera mainCamera = Camera.main;
        gameCamera = mainCamera.GetComponent<GameCamera>();
        gameCamera.hardTransition(stage);
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

        if (tiles >= tilesPerStage && stage < maxStages)
        {
            stage++;
            gameCamera.initiateTransition(stage);
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
        gameData.setTiles(tiles);
        gameData.setCoins(coins);
        gameData.setMeters(meters);
        SceneManager.LoadScene("Menu");
    }

    public int getStage()
    {
        return stage;
    }
}
