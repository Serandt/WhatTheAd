using System.Collections.Generic;
using UnityEngine;
using System;

public class BoundaryZones : MonoBehaviour
{

    public static BoundaryZones Instance;
    public float noSpawnZoneWidth = 2.0f;
    public float noSpawnZoneDepth = 2.0f;
    public float spawnRadius = 0.4f;


    private Vector3 surfaceCenter; 
  
    private float width;
    private float depth; 
 

    private float spawnAreaIncreaseWidth;
    private float spawnAreaIncreaseDepth;

    System.Random rand = new System.Random();

    public GameObject boundary;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Vector3 center = boundary.transform.position;
        width = boundary.transform.localScale.x;
        depth = boundary.transform.localScale.z;
        surfaceCenter = new Vector3(center.x, center.y + boundary.transform.localScale.y / 2, center.z);
    }


    

    public void SpawnAd(GameObject objectToSpawn, int maxSpawns, int spawnCount)
    {
        spawnAreaIncreaseWidth = width / maxSpawns;
        spawnAreaIncreaseDepth = depth / maxSpawns;

        Vector3 spawnPosition;
        bool positionFound = false;

        float currentWidth = (spawnAreaIncreaseWidth * spawnCount) + spawnAreaIncreaseWidth;
        float currentDepth = (spawnAreaIncreaseDepth * spawnCount) + spawnAreaIncreaseDepth;

        float offsetWidth = currentWidth;
        float offsetDepth = currentDepth;

        while (!positionFound)
        {
            if (rand.Next(0, 2) == 0)
            {
                if (rand.Next(0, 2) == 0)
                {
                    offsetWidth = -currentWidth;
                }
                offsetDepth = UnityEngine.Random.Range(-currentDepth, currentDepth);

            }
            else
            {
                if (rand.Next(0, 2) == 0)
                {
                    offsetDepth = -currentDepth;
                }
                offsetWidth = UnityEngine.Random.Range(-currentWidth, currentWidth);
            }

            if (Mathf.Abs(offsetWidth) < noSpawnZoneWidth)
                offsetWidth = (offsetWidth >= 0) ? noSpawnZoneWidth : -noSpawnZoneWidth;
            if (Mathf.Abs(offsetDepth) < noSpawnZoneDepth)
                offsetDepth = (offsetDepth >= 0) ? noSpawnZoneDepth : -noSpawnZoneDepth;

            spawnPosition = surfaceCenter + new Vector3(offsetWidth, 0, offsetDepth);

     
            if (!Physics.CheckSphere(spawnPosition, spawnRadius))
            {
                Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
                positionFound = true;
            }
        }
    }
}