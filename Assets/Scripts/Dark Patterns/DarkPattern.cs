using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DarkPattern : MonoBehaviour
{
    public int ID;
    public float SpawnTime { get; private set; }
    public float CloseTime { get; private set; }
    public bool IsClosed { get; private set; }

    public GameObject GameObject;

    public void Initialize(int id, GameObject gameObject)
    {
        ID = id;
        GameObject = gameObject;
        SpawnTime = Time.time - GameManager.Instance.startTime;
        IsClosed = false;
    }

    public void Close()
    {
        CloseTime = Time.time - GameManager.Instance.startTime;
        IsClosed = true;
        if(!GameManager.Instance.playTutorial)
            GameData.Instance.AddAdData(ID, SpawnTime, CloseTime, IsClosed);
        OnClose();
        Destroy(this.gameObject);
    }

    protected virtual void OnClose()
    {
        
    }

    void OnDestroy()
    {
        if (!IsClosed) Close(); 
    }
}
