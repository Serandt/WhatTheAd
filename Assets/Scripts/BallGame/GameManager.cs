using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text scoreText;
    public TMP_Text timeText;
    public float gameTime = 300.0f; 

    private int score;
    private float timeRemaining = -100;
    public bool playGame = false;

    public enum MaterialTag
    {
        Red,
        Blue,
        Yellow
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
        ManageUiVisibility(false);
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
            EndGame();
        }

    }

    public void AddScore(int points)
    {
        score += points;
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        timeText.text = "Time: " + Mathf.CeilToInt(timeRemaining);
    }

    public void StartGame()
    {
        timeRemaining = gameTime;
        playGame = true;
        ManageUiVisibility(true);
    }

    public void EndGame()
    {
        playGame = false;
        ManageUiVisibility(false);
    }

    private void ManageUiVisibility(bool b)
    {
        scoreText.gameObject.SetActive(b);
        timeText.gameObject.SetActive(b);
    }
}