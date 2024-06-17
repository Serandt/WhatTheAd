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

        // Instantiate and get a reference to the new ball
        GameObject newBall = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);

        // Correctly set the 'obj' reference on the newly instantiated ball
        FalseBall falseBallComponent = newBall.GetComponent<FalseBall>();
        if (falseBallComponent != null)
        {
            falseBallComponent.obj = gameObject; // Set to this GameObject, not the prefab
        }
        else
        {
            Debug.LogError("FalseBall component not found on the newly instantiated ball.");
        }
    }

    public void SpawnBin()
    {
        Instantiate(bin, transform.position, Quaternion.identity);
    }
}
