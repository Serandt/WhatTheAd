using System;
using System.Collections;
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

    public float gameTime = 60 * 1f;

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

    private float globalHighscore = 134f;

    private float endGameCoolDown = 5f;

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
        conditionCounter = 0;
        livesRemaining = playerLives;
        LivesDisplay.GetComponent<TextMeshPro>().text = $"Lives: {playerLives}";
        highscoresDisplay.GetComponent<TextMeshPro>().text = $"Highscore: {globalHighscore}";
        string id = Guid.NewGuid().ToString("N").Substring(0, 8);
        GameData.Instance.SetPlayerID(id);
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
                popupsDisplay.GetComponent<TextMeshPro>().text = "Done. The experiment leader will tell you what to do next.";
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
        BallSpawner.Instance.SetUpSpawnTimes();
        livesRemaining = playerLives;
        score = 0;
        LivesDisplay.GetComponent<TextMeshPro>().text = $"Lives: {playerLives}";
        scoreDisplay.GetComponent<TextMeshPro>().text = "Points: " + score.ToString();
        popupsDisplay.GetComponent<TextMeshPro>().text = $"Current ads open: {DarkPatternManager.Instance.activePatterns.Count} {Environment.NewLine} Points for ball: {Math.Pow(.5f, DarkPatternManager.Instance.activePatterns.Count)}";

        GameData.Instance.ClearDataWrapper();
        startTime = Time.time;
        buttons.SetActive(false);
        timeRemaining = gameTime;
        playGame = true;
        condition = cond;

        DarkPatternManager.Instance.SetUpSpawnTimes();


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
        BallSpawner.Instance.SetUpSpawnTimes();
        DarkPatternManager.Instance.SetUpSpawnTimes();
        if (playTutorial == true)
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
        
        AudioManager.Instance.ConditionComplete();
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
                highscoresDisplay.GetComponent<TextMeshPro>().text = $"Highscore: {globalHighscore}";
            }
            conditionCounter++;
        }
        Debug.Log("Condition Counter: " + conditionCounter);
       
        if (conditionCounter > 3)
        {
            string id = Guid.NewGuid().ToString("N").Substring(0, 8);
            GameData.Instance.SetPlayerID(id);
            conditionCounter = 0;
            Debug.Log("In if: " + id);
        }


        ResetValues();

        DeleteObjects();
        Invoke("SetButtonsVisible", endGameCoolDown);
    }

    private void ResetValues()
    {
        playGame = false;
        condition = Condition.None;
        BoundaryManager.Instance.collideWithBoundaryCounter = 0;
        ResetLives();
        timeRemaining = 0;
        DarkPatternManager.Instance.spawnCount = 0;
        LivesDisplay.GetComponent<TextMeshPro>().text = $"Lives: {playerLives}";
        scoreDisplay.GetComponent<TextMeshPro>().text = "Points: " + score.ToString();
        timeDisplay.GetComponent<TextMeshPro>().text = FormatTime(timeRemaining);
        popupsDisplay.GetComponent<TextMeshPro>().text = $"Current ads open: {0} {Environment.NewLine} Points for ball: {Math.Pow(.5f, 0)}";
    }

    private void SetButtonsVisible()
    {
        buttons.SetActive(true);
    }

    private void DeleteObjects()
    {
        List<GameObject> objects = new List<GameObject>(); // Erstellen einer Liste anstelle eines Arrays

        // Hinzuf�gen der GameObjects zu der Liste
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
        ControllerInteraction.Instance.ControllerVibration();

        if (livesRemaining == 0)
        {
           
            LivesDisplay.GetComponent<TextMeshPro>().text = $"Lives: {livesRemaining}{Environment.NewLine}You lost... {Environment.NewLine}Reseting Score";
            ResetLives();
            if (score > 0)
                score = 0;
            Invoke("ResetGameOverMessage", 3f);
            if(playGame)
                AudioManager.Instance.GameOver();
        }
        else
        {
            AudioManager.Instance.LoseLive();
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