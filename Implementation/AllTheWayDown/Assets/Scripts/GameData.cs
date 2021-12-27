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
    private int highscore;

    public int getCoins()
    {
        return coins;
    }

    public void setCoins(int coins)
    {
        this.coins = coins;
    }
    
    public int getMeters()
    {
        return meters;
    }

    public void setMeters(int meters)
    {
        this.meters = meters;
    }
    
    public int getHighscore()
    {
        return highscore;
    }

    public void setHighscore(int highscore)
    {
        this.highscore = highscore;
    }
    
}
