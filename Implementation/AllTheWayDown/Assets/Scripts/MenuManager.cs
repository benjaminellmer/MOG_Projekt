
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public static MenuManager inst;

    private MenuCamera menuCamera;
    public GameData gameData;
    private int newStage = 0;
    [SerializeField] private Text coinText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text scoreLabel;
    [SerializeField] private int tilesPerStage = 5;
    [SerializeField] private int maxStage = 1;

    private void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        Camera mainCamera = Camera.main;
        menuCamera = mainCamera.GetComponent<MenuCamera>();
        setScores();
    }

    public void startGame(int stage)
    {
        gameData.setStartStage(stage);
        SceneManager.LoadScene("Game");
    }

    public void GoToSettings()
    {
        SceneManager.LoadScene("ControlMenu");
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
        
        //Check if a new stage was unlocked
        var stage = PlayerPrefs.GetInt("stage", 0);
        var tiles = gameData.getTiles() - 2;
        var reachedStage = (int) tiles / tilesPerStage;
        if (reachedStage > stage)
        {
            stage = reachedStage;
            if (stage > maxStage)
            {
                stage = maxStage;
            }
            PlayerPrefs.SetInt("stage", stage);
            //Transition camera
            newStage = stage;
            Invoke("TransitionCamera", .7f);
        }
        //Save the prefs
        PlayerPrefs.Save();
    }

    private void TransitionCamera()
    {
        int moveBy = newStage * 30;
        menuCamera.moveBy(moveBy);
    }
}