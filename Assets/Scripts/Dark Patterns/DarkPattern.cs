using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DarkPattern : MonoBehaviour
{
    public int ID { get; private set; }
    public float SpawnTime { get; private set; }
    public float CloseTime { get; private set; }
    public bool IsClosed { get; private set; }

    public GameObject GameObject;

    public void Initialize(int id, GameObject gameObject)
    {
        ID = id;
        GameObject = gameObject;
        SpawnTime = Time.time;
        IsClosed = false;
    }

    public void Close()
    {
        CloseTime = Time.time;
        IsClosed = true;
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
