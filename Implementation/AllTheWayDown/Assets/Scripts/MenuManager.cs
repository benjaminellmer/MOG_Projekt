
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

    private void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        Camera mainCamera = Camera.main;
        menuCamera = mainCamera.GetComponent<MenuCamera>();
        SetScores();
    }

    public void StartGame(int stage)
    {
        gameData.SetStartStage(stage);
        SceneManager.LoadScene("Game");
    }

    public void GoToSettings()
    {
        SceneManager.LoadScene("ControlMenu");
    }

    private void SetScores()
    {
        //Get data from last run and total data
        var highScore = PlayerPrefs.GetInt("highScore", 0);
        var totalCoins = PlayerPrefs.GetInt("coins", 0);
        var lastCoins = gameData.GetCoins();
        var lastScore = gameData.GetMeters();
        
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
        
        //Check if its a highScore -> save if it is
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
        gameData.SetHighScore(highScore);

        //Add coins to total save and display
        totalCoins += lastCoins;
        coinText.text = totalCoins + "";
        PlayerPrefs.SetInt("coins", totalCoins);
        
        //Check if a new stage was unlocked
        var stage = PlayerPrefs.GetInt("stage", 1);
        var reachedStage = gameData.GetReachedStage();
        if (reachedStage > stage)
        {
            stage = reachedStage;
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
        int moveBy = (newStage-1) * 30;
        menuCamera.MoveBy(moveBy);
    }
}