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
    
    
}
