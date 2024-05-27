using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject scoreClock;
    public GameObject timeClock;
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
        scoreClock.GetComponent<TextMeshPro>().text = score.ToString();
        timeClock.GetComponent<TextMeshPro>().text = Mathf.CeilToInt(timeRemaining).ToString();
    }

    public void StartGame()
    {
        timeRemaining = gameTime;
        playGame = true;
    }

    public void EndGame()
    {
        playGame = false;
    }

}