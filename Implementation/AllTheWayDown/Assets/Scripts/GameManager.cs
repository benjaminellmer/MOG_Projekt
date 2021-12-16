using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool freezeTiles = false;
    private int coins = 0;
    private int tiles = 0;
    public static GameManager inst;

    [SerializeField] private Text tilesText;
    [SerializeField] private Text coinsText;

    private void Awake()
    {
        inst = this;
    }

    public void IncScore()
    {
        ++coins;
        coinsText.text = "Coins: " + coins;
    }

    public void IncTiles()
    {
        if (!freezeTiles)
        {
            ++tiles;
            tilesText.text = "" + tiles;
        }
    }

    public void FreezeTiles()
    {
        freezeTiles = true;
    }
}
