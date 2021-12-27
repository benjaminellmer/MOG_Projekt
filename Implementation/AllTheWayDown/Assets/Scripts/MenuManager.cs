
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public static MenuManager inst;

    public GameData gameData;
    [SerializeField] private Text coinText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text scoreLabel;

    private void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        setScores();
    }

    private void setScores()
    {
        //Get data from last run and total data
        var highScore = PlayerPrefs.GetInt("highScore", 0);
        var totalCoins = PlayerPrefs.GetInt("coins", 0);
        var lastCoins = gameData.getCoins();
        var lastScore = gameData.getMeters();
        
        //Set text for last score
        if (lastScore == 0)
        {
            scoreLabel.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(false);
        }
        else
        {
            scoreLabel.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(true);
            scoreText.text = lastScore + "m";
        }
        
        //Check if its a highscore -> save if it is
        if (lastScore > highScore)
        {
            highScore = lastScore;
            PlayerPrefs.SetInt("highScore", highScore);
            scoreLabel.text = "New Highscore";
        }
        else
        {
            scoreLabel.text = "Score";
        }
        highScoreText.text = highScore + "m";
        gameData.setHighscore(highScore);

        //Add coins to total save and display
        totalCoins += lastCoins;
        coinText.text = totalCoins + "";
        PlayerPrefs.SetInt("coins", totalCoins);
        
        //Save the prefs
        PlayerPrefs.Save();
    }
}