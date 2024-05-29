using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class GameEvent
{
    public float timestamp;
    public bool isPoint; // true if it's a point, false if it's an error
}

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    private List<GameEvent> gameEvents = new List<GameEvent>();
    private string filePath;
    private string playerName;

    private void Awake()
    {
        Instance = this;
        playerName = "Player";
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
    }

    public void AddEvent(float timestamp, bool isPoint)
    {
        GameEvent newEvent = new GameEvent
        {
            timestamp = timestamp,
            isPoint = isPoint
        };
        gameEvents.Add(newEvent);
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(new GameEventsWrapper { events = gameEvents }, true);
        string timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
        filePath = Path.Combine(Application.persistentDataPath, $"{playerName}_{timestamp}_GameData.json");
        File.WriteAllText(filePath, json);
        gameEvents.Clear();
    }

    public List<GameEvent> GetGameEvents()
    {
        return gameEvents;
    }
}

[System.Serializable]
public class GameEventsWrapper
{
    public List<GameEvent> events;
}