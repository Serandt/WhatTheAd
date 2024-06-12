using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseFirend : DarkPattern
{
    private Vector3 areaSize;
    public GameObject ballPrefab;
    public GameObject bin;
    private bool spawn = false;
    private void Awake()
    {
        areaSize = BallSpawner.Instance.areaSize;
        if(spawn == false)
        {
            SpawnBall();
            SpawnBin();
            spawn = true;
        }
  
    }

    public void SpawnBall()
    {
        
        Vector3 spawnPosition = new Vector3(
            Random.Range(-areaSize.x / 2, areaSize.x / 2),
            Random.Range(-areaSize.y / 2, areaSize.y / 2),
            Random.Range(-areaSize.z / 2, areaSize.z / 2))
             + BallSpawner.Instance.gameObject.transform.position;

        Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
        ballPrefab.GetComponent<FalseBall>().id = ID;
    }

    public void SpawnBin()
    {
        Instantiate(bin, transform.position, Quaternion.identity);
    }
}
