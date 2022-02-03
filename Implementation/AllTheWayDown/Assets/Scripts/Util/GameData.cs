using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New GameData", menuName = "GameData")]
public class GameData : ScriptableObject
{
    [SerializeField]
    private int startStage;
    [SerializeField]
    private int tiles;
    [SerializeField]
    private int coins;
    [SerializeField]
    private int meters;
    [SerializeField]
    private int highScore;
    [SerializeField]
    private int reachedStage;

    public int GetCoins()
    {
        return coins;
    }

    public void SetCoins(int coins)
    {
        this.coins = coins;
    }
    
    public int GetMeters()
    {
        return meters;
    }

    public void SetMeters(int meters)
    {
        this.meters = meters;
    }
    
    public int GetHighScore()
    {
        return highScore;
    }

    public void SetHighScore(int highScore)
    {
        this.highScore = highScore;
    }

    public void SetStartStage(int stage)
    {
        this.startStage = stage;
    }

    public int GetStartStage()
    {
        return startStage;
    }

    public int GetTiles()
    {
        return tiles;
    }

    public void SetTiles(int tiles)
    {
        this.tiles = tiles;
    }
    
    public int GetReachedStage()
    {
        return reachedStage;
    }

    public void SetReachedStage(int reachedStage)
    {
        this.reachedStage = reachedStage;
    }

}
