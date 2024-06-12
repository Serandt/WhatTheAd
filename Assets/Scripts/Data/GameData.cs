using System;
using System.Collections.Generic;
using System.IO;
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
    public bool isHead;  //true if head, false if hand
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
        playerName = "Player";
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
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

    public void AddBoundaryCollision(float timestamp, int count, bool isHead)
    {
        dataWrapper.boundaryCollisions.Add(new BoundaryCollision
        {
            timestamp = timestamp,
            count = count,
            isHead = isHead
        });
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(dataWrapper, true);
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string filePath = Path.Combine(Application.persistentDataPath, $"{playerName}_{timestamp}_GameData.json");
        File.WriteAllText(filePath, json);
        dataWrapper.gameEvents.Clear();
        dataWrapper.addDatas.Clear();
    }

    public List<GameEvent> GetGameEvents()
    {
        return dataWrapper.gameEvents;
    }
}