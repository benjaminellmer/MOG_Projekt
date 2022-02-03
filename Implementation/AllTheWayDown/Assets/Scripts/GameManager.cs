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
    private int stage;
    
    private GameCamera gameCamera;
    public GameData gameData;
    [SerializeField] private Text coinText;
    [SerializeField] private Text meterText;
    [SerializeField] private int maxStages = 2;
    [SerializeField] private int tilesPerStage = 20;
    [SerializeField] private int startingTiles = 3;
    

    private void Awake()
    {
        stage = gameData.GetStartStage();
        inst = this;
    }

    private void Start()
    {
        Camera mainCamera = Camera.main;
        gameCamera = mainCamera.GetComponent<GameCamera>();
        gameCamera.HardTransition(stage);
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

        if ((stage == 0 && tiles >= startingTiles) ||
            (tiles >= tilesPerStage && stage < maxStages))
        {
            stage++;
            tiles = 0;
            gameCamera.InitiateTransition(stage);
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
        int reachedStage = stage;
        if (tiles < 2)
        {
            reachedStage = stage - 1;
        }
        gameData.SetReachedStage(reachedStage);
        gameData.SetCoins(coins);
        gameData.SetMeters(meters);
        SceneManager.LoadScene("Menu");
    }

    public int GetStage()
    {
        return stage;
    }
}
