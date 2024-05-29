using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject scoreClock;
    public GameObject timeClock;
    public static float gameTime = 300.0f; 

    private int score;
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
        Demo,
        Sort,
        Repeate,
        Move
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
        score++;
        UpdateUI();

        float currentTime = Time.time;
        GameData.Instance.AddEvent(currentTime, true);
    }

    public void AddError()
    {
        score--;
        float currentTime = Time.time;
        GameData.Instance.AddEvent(currentTime, false);
    }

    void UpdateUI()
    {
        scoreClock.GetComponent<TextMeshPro>().text = "Points: " + score.ToString();
        timeClock.GetComponent<TextMeshPro>().text = FormatTime(timeRemaining);
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
        playGame = false;
        condition = Condition.None;
        GameData.Instance.SaveData();
        HighscoreManager.Instance.SaveHighscore(score);
    }

    private string FormatTime(float timeInSeconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(Mathf.CeilToInt(timeInSeconds));
        return string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }

}