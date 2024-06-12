using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseFirend : DarkPattern
{
    private Vector3 areaSize;
    public GameObject ballPrefab;
    public GameObject bin;
    private void Awake()
    { 
        areaSize = BallSpawner.Instance.areaSize;
    }

    private void Start()
    {
        SpawnBall();
        SpawnBin();
    }

    public void SpawnBall()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(-areaSize.x / 2, areaSize.x / 2),
            Random.Range(-areaSize.y / 2, areaSize.y / 2),
            Random.Range(-areaSize.z / 2, areaSize.z / 2))
             + BallSpawner.Instance.gameObject.transform.position;

        Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
        ballPrefab.GetComponent<FalseBall>().id = DarkPatternManager.Instance.spawnCount;
    }

    public void SpawnBin()
    {
        Instantiate(bin, transform.position, Quaternion.identity);
    }
}
