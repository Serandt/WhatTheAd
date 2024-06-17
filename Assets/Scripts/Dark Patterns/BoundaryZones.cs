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
    private Vector3 boundaryMin;
    private Vector3 boundaryMax;
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
        MeshRenderer renderer = boundary.GetComponent<MeshRenderer>();
        boundaryMin = renderer.bounds.min;
        boundaryMax = renderer.bounds.max;
    }




    public void SpawnAd(GameObject objectToSpawnPrefab, int maxSpawns, int spawnCount)
    {
        spawnAreaIncreaseWidth = (width - 2 * spawnRadius) / maxSpawns;
        spawnAreaIncreaseDepth = (depth - 2 * spawnRadius) / maxSpawns;

        Vector3 spawnPosition;
        bool positionFound = false;
        int attemps = 1000;

        while (!positionFound && attemps >= 0)
        {
            float currentWidth = (spawnAreaIncreaseWidth * spawnCount) + spawnAreaIncreaseWidth;
            float currentDepth = (spawnAreaIncreaseDepth * spawnCount) + spawnAreaIncreaseDepth;

            float offsetWidth = currentWidth;
            float offsetDepth = currentDepth;

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

            // Zone around the table, where no Objects can spawn
            if (Mathf.Abs(offsetWidth) < noSpawnZoneWidth)
                offsetWidth = (offsetWidth >= 0) ? noSpawnZoneWidth : -noSpawnZoneWidth;
            if (Mathf.Abs(offsetDepth) < noSpawnZoneDepth)
                offsetDepth = (offsetDepth >= 0) ? noSpawnZoneDepth : -noSpawnZoneDepth;


            spawnPosition = surfaceCenter + new Vector3(offsetWidth, 0, offsetDepth);

            // Clamp spawn Position
            spawnPosition.x = Mathf.Clamp(spawnPosition.x, boundaryMin.x + spawnRadius, boundaryMax.x - spawnRadius);
            spawnPosition.z = Mathf.Clamp(spawnPosition.z, boundaryMin.z + spawnRadius, boundaryMax.z - spawnRadius);

            switch(objectToSpawnPrefab.tag)
            {
                case "FalseFriend":
                    spawnPosition.y = 0f;
                    break;
                case "TutorialAd":
                    spawnPosition.y = 0.2f;
                    break;
                case "ProximityAd":
                    spawnPosition.y = 1.5f;
                    break;
                default:
                    spawnPosition.y = BoundaryManager.Instance.hmd.position.y;
                    break;
            }

            if (IsPositionValid(spawnPosition))
            {
                GameObject spawnedObject = Instantiate(objectToSpawnPrefab, spawnPosition, Quaternion.identity);
                DarkPattern darkPattern = spawnedObject.GetComponent<DarkPattern>();
                if (darkPattern != null)
                {
                    darkPattern.Initialize(spawnCount, spawnedObject); // Initialize with a unique ID or similar identifier
                    DarkPatternManager.Instance.activePatterns.Add(darkPattern);
                    positionFound = true;
                }
            }
            attemps--;
        }
        if (!positionFound)
        {
            Debug.Log("Failed to find a valid spawn position after multiple attempts.");
        }
    }

    private bool IsPositionValid(Vector3 newPosition)
    {
        foreach (DarkPattern p in DarkPatternManager.Instance.activePatterns)
        {
            if (Vector3.Distance(p.GameObject.transform.position, newPosition) < spawnRadius)
                return false;  
        }
        return true;  
    }
}


