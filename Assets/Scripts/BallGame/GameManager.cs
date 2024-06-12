using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject scoreDisplay;
    public GameObject timeDisplay;
    public GameObject LivesDisplay;
    public GameObject highscoresDisplay;
    public GameObject popupsDisplay;
    public GameObject buttons;

    public static float gameTime = 60*2.0f;

    public int playerLives = 3;
    public int livesRemaining;

    private double score;
    public static float timeRemaining = -100;
    public bool playGame = false;
    public bool outOfBoundary = false;

    private int conditionCounter;
    public Condition condition;

    public bool playTutorial = false;



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
        Proximity,
        FalseFriend
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
        livesRemaining = playerLives;
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
        score += 1 * Math.Pow(.5f, DarkPatternManager.Instance.activePatterns.Count); 
        UpdateUI();
        if (!playTutorial)
            GameData.Instance.AddEvent(Time.time, true);
    }

    public void AddError()
    {
        score--;
        UpdateUI();
        if (!playTutorial)
            GameData.Instance.AddEvent(Time.time, false);
    }

    void UpdateUI()
    {
        scoreDisplay.GetComponent<TextMeshPro>().text = "Points: " + score.ToString();
        timeDisplay.GetComponent<TextMeshPro>().text = FormatTime(timeRemaining);
        popupsDisplay.GetComponent<TextMeshPro>().text = $"Current ads open: {DarkPatternManager.Instance.activePatterns.Count} {Environment.NewLine} Points for ball: {Math.Pow(.5f, DarkPatternManager.Instance.activePatterns.Count)}";
    }

    public void StartGame(Condition cond)
    {
        buttons.SetActive(false);
        timeRemaining = gameTime;
        playGame = true;
        condition = cond;
        LivesDisplay.GetComponent<TextMeshPro>().text = $"Lives: {playerLives}";

        switch (condition)
        {
            case Condition.Baseline:
                DarkPatternManager.Instance.activeDarkPattern = null;
                break;
            case Condition.FalseFriend:
                DarkPatternManager.Instance.activeDarkPattern = DarkPatternManager.Instance.falseFriendAd;
                break;
            case Condition.Message:
                DarkPatternManager.Instance.activeDarkPattern = DarkPatternManager.Instance.messagePopUpAd;
                break;
            case Condition.Proximity:
                DarkPatternManager.Instance.activeDarkPattern = DarkPatternManager.Instance.proximityAd;
                break;
        }
    }

    public void PlayTutorial()
    {
        if(playTutorial == true)
        {
            playGame = false;
            playTutorial = true;
            condition = Condition.None;
            ResetLives();
            LivesDisplay.GetComponent<TextMeshPro>().text = $"Lives: {playerLives}";
            for (int i = 1; i < buttons.transform.childCount; i++)
            {
                buttons.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else
        {
            playTutorial = true;
            DarkPatternManager.Instance.activeDarkPattern = DarkPatternManager.Instance.tutorialAd;
            timeRemaining = 60 * 20;
            for (int i = 1; i < buttons.transform.childCount; i++)
            {
                buttons.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void EndGame()
    { 
        foreach (var pattern in DarkPatternManager.Instance.activePatterns)
        {
            if(!playTutorial)
                GameData.Instance.AddAdData(pattern.ID, pattern.SpawnTime, float.NaN, pattern.IsClosed); 
        }
        conditionCounter++;
        playGame = false;
        condition = Condition.None;
        GameData.Instance.SaveData();
        HighscoreManager.Instance.SaveHighscore(score);
        ResetLives();
        LivesDisplay.GetComponent<TextMeshPro>().text = $"Lives: {playerLives}";
        buttons.SetActive(true);

        if(conditionCounter == 3)
        {
            GameData.Instance.SetPlayerID();
            conditionCounter = 0;
        }
            
            
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
            if (score > 0)
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