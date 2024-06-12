using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
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
    public int livesRemaining;

    private double score;
    public static float timeRemaining = -100;
    public bool playGame = false;
    public bool outOfBoundary = false;

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
        livesRemaining = playerLives;

        //TODO: highscoresDisplay;
        StartGame(Condition.None);
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
        LivesDisplay.GetComponent<TextMeshPro>().text = $"Lives: {playerLives}";
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
        ResetLives();
        LivesDisplay.GetComponent<TextMeshPro>().text = $"Lives: {playerLives}";
    }

    private string FormatTime(float timeInSeconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(Mathf.CeilToInt(timeInSeconds));
        return string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }

    public void RemoveLive()
    {
        livesRemaining--;
        LivesDisplay.GetComponent<TextMeshPro>().text = $"Lives: {livesRemaining}";
        Debug.Log("Lives: " + livesRemaining);

        if(livesRemaining == 0)
        {
            Debug.Log("Game Over");
            LivesDisplay.GetComponent<TextMeshPro>().text = $"Lives: {livesRemaining}{Environment.NewLine}You lost... {Environment.NewLine}Reseting Score";
            ResetLives();
            score = 0;
            Invoke("ResetGameOverMessage", 5f);
        }
    }

    private void ResetLives()
    {
        livesRemaining = playerLives;
    }

    private void ResetGameOverMessage()
    {
        LivesDisplay.GetComponent<TextMeshPro>().text = $"Lives: {livesRemaining}";
    }
}