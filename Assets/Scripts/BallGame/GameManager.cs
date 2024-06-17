using System;
using System.Collections.Generic;
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

    public static float gameTime = 60 *1f;

    public int playerLives = 3;
    public int livesRemaining;

    private float score;
    public static float timeRemaining = -100;
    public bool playGame = false;
    public bool outOfBoundary = false;

    private int conditionCounter;
    public Condition condition;

    public float startTime;

    public bool playTutorial = false;

    public float globalHighscore = 40.12f;



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
        LivesDisplay.GetComponent<TextMeshPro>().text = $"Lives: {playerLives}";
        highscoresDisplay.GetComponent<TextMeshPro>().text = $"Highscore: {globalHighscore}";
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
        score += ((float)(1 * Math.Pow(.5f, DarkPatternManager.Instance.activePatterns.Count))); 
        if (!playTutorial)
            GameData.Instance.AddEvent(Time.time - startTime, true);
    }

    public void AddError()
    {
        score--;
        if (!playTutorial)
            GameData.Instance.AddEvent(Time.time - startTime, false);
    }

    void UpdateUI()
    {
        scoreDisplay.GetComponent<TextMeshPro>().text = "Points: " + score.ToString();
        timeDisplay.GetComponent<TextMeshPro>().text = FormatTime(timeRemaining);
        popupsDisplay.GetComponent<TextMeshPro>().text = $"Current ads open: {DarkPatternManager.Instance.activePatterns.Count} {Environment.NewLine} Points for ball: {Math.Pow(.5f, DarkPatternManager.Instance.activePatterns.Count)}";
    }

    public void StartGame(Condition cond)
    {
        GameData.Instance.ClearDataWrapper();
        startTime = Time.time;
        buttons.SetActive(false);
        timeRemaining = gameTime;
        playGame = true;
        condition = cond;
       

        switch (cond)
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
            for (int i = 1; i < buttons.transform.childCount; i++)
            {
                buttons.transform.GetChild(i).gameObject.SetActive(true);
            }
            EndGame();
            playTutorial = false;
        
        }
        else
        {
            playTutorial = true;
            playGame = true;
            DarkPatternManager.Instance.activeDarkPattern = DarkPatternManager.Instance.tutorialAd;
            timeRemaining = gameTime * 5;
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
            if (!playTutorial){
                GameData.Instance.AddAdData(pattern.ID, pattern.SpawnTime, float.NaN, pattern.IsClosed);

            }
               
        }
        if (!playTutorial)
        {
            GameData.Instance.SaveData();
            if(score > globalHighscore)
            {
                globalHighscore = score;
            }
            //conditionCounter++;
        }
        playGame = false;
        condition = Condition.None;
        
        ResetLives();
        buttons.SetActive(true);
        timeRemaining = 0;
        DarkPatternManager.Instance.spawnCount = 0;
        BoundaryManager.Instance.collideWithBoundaryCounter = 0;

        DeleteObjects();

      /*  Debug.Log("ConditionCounter: " + conditionCounter);
        if (conditionCounter == 3)
        {
            GameData.Instance.SetPlayerID();
            conditionCounter = 0;
        }*/


        score = 0;
        LivesDisplay.GetComponent<TextMeshPro>().text = $"Lives: {playerLives}";
        scoreDisplay.GetComponent<TextMeshPro>().text = "Points: " + score.ToString();
        timeDisplay.GetComponent<TextMeshPro>().text = FormatTime(timeRemaining);
        popupsDisplay.GetComponent<TextMeshPro>().text = $"Current ads open: {0} {Environment.NewLine} Points for ball: {Math.Pow(.5f, 0)}";
    }

    private void DeleteObjects()
    {
        List<GameObject> objects = new List<GameObject>(); // Erstellen einer Liste anstelle eines Arrays

        // Hinzufügen der GameObjects zu der Liste
        objects.AddRange(GameObject.FindGameObjectsWithTag("Ball"));
        objects.AddRange(GameObject.FindGameObjectsWithTag("FalseBall"));
        objects.AddRange(GameObject.FindGameObjectsWithTag("Bin"));
        objects.AddRange(GameObject.FindGameObjectsWithTag("FalseFriend"));
        objects.AddRange(GameObject.FindGameObjectsWithTag("Popup"));
        objects.AddRange(GameObject.FindGameObjectsWithTag("ProximityAd"));
        objects.AddRange(GameObject.FindGameObjectsWithTag("TutorialAd"));

        foreach (GameObject b in objects)
        {
            Destroy(b);
        }

        DarkPatternManager.Instance.activePatterns.Clear(); 
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