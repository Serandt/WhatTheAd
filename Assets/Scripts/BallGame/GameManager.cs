using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject scoreDisplay;
    public GameObject timeDisplay;
    public GameObject distanceDisplay;
    public GameObject LivesDisplay;
    public GameObject highscoresDisplay;
    public GameObject popupsDisplay;

    public static float gameTime = 20.0f;

    public int playerLives = 3;

    private double score;
    public static float timeRemaining = -100;
    public bool playGame = false;

    private GameObject _buttons;
    private Condition condition;
    public static int popupsCount = 0;

    public enum MaterialTag
    {
        Red,
        Blue,
        Yellow
    }

    public enum Condition
    {
        None,
        Tutorial,
        Baseline,
        Message,
        Proximity
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        score = 0;
        _buttons = GameObject.Find("Buttons");
        StartGame(Condition.None);

        //TODO: highscoresDisplay;
    }

    void Update()
    {
        if(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
            }
            UpdateUI();
        }
        else
        {
            if (playGame)
            {
                EndGame();
            }
        }
    }

    public void AddScore()
    {
        score *= Math.Pow(.5f, popupsCount); ;
        UpdateUI();

        GameData.Instance.AddEvent(Time.time, true);
    }

    public void AddError()
    {
        score--;
        GameData.Instance.AddEvent(Time.time, false);
    }

    void UpdateUI()
    {
        scoreDisplay.GetComponent<TextMeshPro>().text = "Points: " + score.ToString();
        timeDisplay.GetComponent<TextMeshPro>().text = FormatTime(timeRemaining);
        popupsDisplay.GetComponent<TextMeshPro>().text = $"Current ads open: {popupsCount} {Environment.NewLine} Points for ball: {Math.Pow(.5f, popupsCount)}";
        //TODO: distanceDisplay;
    }

    public void StartGame(Condition cond)
    {
        _buttons.SetActive(false);
        timeRemaining = gameTime;
        playGame = true;
        condition = cond;
    }

    public void EndGame()
    { 
        foreach (var pattern in DarkPatternManager.Instance.activePatterns)
        {
            GameData.Instance.AddAdData(pattern.ID, pattern.SpawnTime, float.NaN, pattern.IsClosed); 
        }

        playGame = false;
        condition = Condition.None;
        GameData.Instance.SaveData();
        HighscoreManager.Instance.SaveHighscore(score);
        playerLives = 3;
        LivesDisplay.GetComponent<TextMeshPro>().text = $"Lives: {playerLives}";
    }

    private string FormatTime(float timeInSeconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(Mathf.CeilToInt(timeInSeconds));
        return string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }

    public void RemoveLive()
    {
        playerLives--;
        LivesDisplay.GetComponent<TextMeshPro>().text = $"Lives: {playerLives}";
        Debug.Log("Lives: " + playerLives);

        if(playerLives == 0)
        {
            Debug.Log("Game Over");
            score = 0;
        }
    }

}