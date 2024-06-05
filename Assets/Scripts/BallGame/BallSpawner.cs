using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public Vector3 areaSize = new Vector3(2, 0, 2);
    public float startInterval = 5.0f;
    public float endInterval = 1.0f;
    public float decreaseTime = 120.0f;

    [System.Serializable]
    public struct SpawnIntervall
    {
        public int intervallLength;
        public int spawnTime;
    }
    public SpawnIntervall[] spawnIntervalls;
    private float currentInterval;
    private float timeElapsed;
    private float lastSpawnTime;

    void Start()
    {
        currentInterval = startInterval;
        lastSpawnTime = -100;
    }

    void Update()
    {
        if (GameManager.Instance.playGame)
        {
            timeElapsed += Time.deltaTime;

            if (Time.time - lastSpawnTime >= currentInterval)
            {
                SpawnBall();
                lastSpawnTime = Time.time;

                currentInterval = Mathf.Lerp(startInterval, endInterval, timeElapsed / decreaseTime);
            }
        }
    }

    void SpawnBall()
    {
        Vector3 spawnPosition = new Vector3(
           Random.Range(-areaSize.x / 2, areaSize.x / 2),
           Random.Range(-areaSize.y / 2, areaSize.y / 2),
           Random.Range(-areaSize.z / 2, areaSize.z / 2)
       ) + transform.position;

        GameObject newBall = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
        BallManager ballManager = newBall.GetComponent<BallManager>();
        ballManager.SetRandomMaterial();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 size = new Vector3(areaSize.x, 0, areaSize.z); 
        Gizmos.DrawWireCube(transform.position, size);
    }
}