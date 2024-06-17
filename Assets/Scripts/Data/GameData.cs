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
    private const string PlayerIDKey = "PlayerID";
    public int PlayerID { get; private set; }

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
        //playerName = "Player " + PlayerPrefs.GetInt(PlayerIDKey, 0);
        //playerName = "Player Test";
        //Debug.Log("PlayerID: " + PlayerPrefs.GetInt(PlayerIDKey, 0));
    }

    public void SetPlayerID()
    {
        SaveID(PlayerID + 1);
    }

    public void SaveID(int newId)
    {
        if (newId > PlayerID)
        {
            PlayerID = newId;
            PlayerPrefs.SetInt(PlayerIDKey, PlayerID);
            PlayerPrefs.Save();
        }
    }

    public void ResetPlayerID()
    {
        Debug.Log("RestePlayerID");
        PlayerID = 0;
        PlayerPrefs.SetFloat(PlayerIDKey, PlayerID);
        PlayerPrefs.Save();
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
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string filePath = Path.Combine(Application.persistentDataPath, $"{timestamp}_{GameManager.Instance.condition.ToString()}_GameData.json");
            File.WriteAllText(filePath, json);
        }

        ClearDataWrapper();
    }

    public void ClearDataWrapper()
    {
        dataWrapper.gameEvents.Clear();
        dataWrapper.addDatas.Clear();
        dataWrapper.boundaryCollisions.Clear();

    }


}