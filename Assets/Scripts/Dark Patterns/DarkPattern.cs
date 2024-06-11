using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DarkPattern : MonoBehaviour
{
    public int ID { get; private set; }
    public DateTime SpawnTime { get; private set; }
    public DateTime CloseTime { get; private set; }
    public bool IsClosed { get; private set; }

    public GameObject GameObject;

    public void Initialize(int id, GameObject gameObject)
    {
        ID = id;
        GameObject = gameObject;
        SpawnTime = DateTime.Now;
        IsClosed = false;
    }

    public void Close()
    {
        CloseTime = DateTime.Now;
        IsClosed = true;
        OnClose();
    }

    protected virtual void OnClose()
    {
        
    }

    void OnDestroy()
    {
        if (!IsClosed) Close(); 
    }
}
