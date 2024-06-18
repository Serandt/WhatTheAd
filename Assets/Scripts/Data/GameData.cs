using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


[System.Serializable]
public class GameEvent
{
    public float timestamp;
    public bool isPoint; // true if it's a point, false if it's an error
}

[System.Serializable]
public class AddData
{
    public int id;
    public float spawnTime;
    public float closeTime;
    public bool isClosed;
}

[System.Serializable]
public class BoundaryCollision
{
    public float timestamp;
    public int count;
    public bool isHead;
    public bool isHand;
}

[System.Serializable]
public class GameDataWrapper
{
    public List<GameEvent> gameEvents;
    public List<AddData> addDatas;
    public List<BoundaryCollision> boundaryCollisions;
}

public class GameData : MonoBehaviour
{

    public string playerID;

    public static GameData Instance;

    private GameDataWrapper dataWrapper = new GameDataWrapper
    {
        gameEvents = new List<GameEvent>(),
        addDatas = new List<AddData>(),
        boundaryCollisions = new List<BoundaryCollision>()
    };

    private string playerName;

    private void Awake()
    {
        Instance = this;
    }

    public void SetPlayerID(string newId)
    {
        playerID = newId;
    }

   

    public void AddEvent(float timestamp, bool isPoint)
    {
        dataWrapper.gameEvents.Add(new GameEvent
        {
            timestamp = timestamp,
            isPoint = isPoint
        });
    }

    public void AddAdData(int id, float spawnTime, float closeTime, bool isClosed)
    {
        dataWrapper.addDatas.Add(new AddData
        {
            id = id,
            spawnTime = spawnTime,
            closeTime = closeTime,
            isClosed = isClosed
        });
    }

    public void AddBoundaryCollision(float timestamp, int count, bool isHead, bool isHand)
    {
        dataWrapper.boundaryCollisions.Add(new BoundaryCollision
        {
            timestamp = timestamp,
            count = count,
            isHead = isHead,
            isHand = isHand
        });
    }

    public void SaveData()
    {
        if(GameManager.Instance.condition != GameManager.Condition.Tutorial)
        {
            string json = JsonUtility.ToJson(dataWrapper, true);
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            string filePath = Path.Combine(Application.persistentDataPath, $"{playerID}_{timestamp}_{GameManager.Instance.condition.ToString()}_GameData.json");
            File.WriteAllText(filePath, json);

            SaveDataToCSV();
        }

        ClearDataWrapper();
    }

    public void ClearDataWrapper()
    {
        dataWrapper.gameEvents.Clear();
        dataWrapper.addDatas.Clear();
        dataWrapper.boundaryCollisions.Clear();
    }

    private string GameEventsToCSV()
    {
        StringBuilder csv = new StringBuilder("Timestamp;IsPoint\n");  // Adjust the header to reflect each column for properties
        foreach (var gameEvent in dataWrapper.gameEvents)
        {
            csv.AppendLine($"{gameEvent.timestamp};{gameEvent.isPoint}");
        }
        return csv.ToString();
    }

    private string AddDataToCSV()
    {
        StringBuilder csv = new StringBuilder("ID;SpawnTime;CloseTime;IsClosed\n");  // Header with columns for each data property
        foreach (var add in dataWrapper.addDatas)
        {
            csv.AppendLine($"{add.id};{add.spawnTime};{add.closeTime};{add.isClosed}");
        }
        return csv.ToString();
    }


    private string BoundaryCollisionsToCSV()
    {
        StringBuilder csv = new StringBuilder();
        // Header für die CSV-Datei
        csv.AppendLine("Count;Timestamp;IsHand;IsHead");

        // Daten für jede Kollision
        foreach (var collision in dataWrapper.boundaryCollisions)
        {
            csv.AppendLine($"{collision.count};{collision.timestamp};{collision.isHand};{collision.isHead}");
        }

        return csv.ToString();
    }

    public void SaveDataToCSV()
    {
        string basePath = Application.persistentDataPath;
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");

        // Saving each data part to a separate file
        File.WriteAllText(Path.Combine(basePath, $"{playerID}_{timestamp}_{GameManager.Instance.condition.ToString()}_GameEvents.csv"), GameEventsToCSV());
        File.WriteAllText(Path.Combine(basePath, $"{ playerID}_{ timestamp}_{ GameManager.Instance.condition.ToString()}_AdData.csv"), AddDataToCSV());
        File.WriteAllText(Path.Combine(basePath, $"{ playerID}_{ timestamp}_{ GameManager.Instance.condition.ToString()}_BoundaryCollisions.csv"), BoundaryCollisionsToCSV());
    }



}