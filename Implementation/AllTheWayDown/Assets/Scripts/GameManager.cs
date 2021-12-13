using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool freezeTiles = false;
    private int score = 0;
    private int tiles = 0;
    public static GameManager inst;

    [SerializeField] private Text tilesText;

    private void Awake()
    {
        inst = this;
    }

    public void IncScore()
    {
        ++score;
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
