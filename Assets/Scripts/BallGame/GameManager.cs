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
    public static bool isPopupActive = false;
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
    }

}