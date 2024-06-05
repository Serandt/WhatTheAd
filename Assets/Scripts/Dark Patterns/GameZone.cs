using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameZone : MonoBehaviour
{
    public Vector2 gameAreaSize; // Size of the entire game area (width, height)
    public int numberOfZones; // Number of zones
    public Vector2 spawnPoint; // Point where the player spawns (relative to the center of the game area)

    public Rect[] zones; // Array to store the coordinates of each zone


    public static GameZone Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Calculate the size of each zone
        float zoneWidthIncrement = gameAreaSize.x / (2 * numberOfZones);
        float zoneHeightIncrement = gameAreaSize.y / (2 * numberOfZones);

        // Initialize the zones array
        zones = new Rect[numberOfZones];

        // Calculate the boundaries of each zone
        for (int i = 0; i < numberOfZones; i++)
        {
            float zoneWidth = gameAreaSize.x - (2 * i * zoneWidthIncrement);
            float zoneHeight = gameAreaSize.y - (2 * i * zoneHeightIncrement);
            float zoneX = -zoneWidth / 2;
            float zoneY = -zoneHeight / 2;

            zones[i] = new Rect(zoneX, zoneY, zoneWidth, zoneHeight);

            Debug.Log($"Zone {i + 1}: {zones[i]}");
        }

        // Determine the zone of the spawn point
        int spawnZone = CalculateZone(spawnPoint, zoneWidthIncrement, zoneHeightIncrement, numberOfZones);
        Debug.Log($"The spawn point is in zone {spawnZone}");
    }

    public int CalculateZone(Vector3 point, float zoneWidthIncrement, float zoneHeightIncrement, int numberOfZones)
    {
        // Adjust the point so that (0,0) is in the center of the game area
        Vector2 adjustedPoint = new Vector2(point.x + gameAreaSize.x / 2, point.z + gameAreaSize.y / 2);

        // Calculate the zone based on the minimum distance to the edge
        int zoneX = Mathf.FloorToInt(adjustedPoint.x / zoneWidthIncrement) + 1;
        int zoneY = Mathf.FloorToInt(adjustedPoint.y / zoneHeightIncrement) + 1;

        // The zone is determined by the maximum of zoneX and zoneY
        int zone = Mathf.Max(zoneX, zoneY);

        // Ensure the zone is within bounds
        if (zone > numberOfZones) zone = numberOfZones;

        return zone;
    }

    public void SpawnObjectInZone(GameObject obj, int zoneIndex, float minHeight, float maxHeight)
    {
        if (zoneIndex < 1 || zoneIndex > numberOfZones)
        {
            Debug.LogError("Zone index out of range.");
            return;
        }

        Rect outerZone = zones[zoneIndex - 1];
        Rect innerZone = zoneIndex > 1 ? zones[zoneIndex - 2] : new Rect(Vector2.zero, Vector2.zero);

        float xMin = outerZone.xMin;
        float xMax = outerZone.xMax;
        float yMin = outerZone.yMin;
        float yMax = outerZone.yMax;

        if (zoneIndex > 1)
        {
            xMin = innerZone.xMax;
            xMax = innerZone.xMin;
            yMin = innerZone.yMax;
            yMax = innerZone.yMin;
        }

        Vector3 spawnPosition = new Vector3(
            Random.Range(xMin, xMax),
            Random.Range(minHeight, maxHeight),
            Random.Range(yMin, yMax)
        );

        Instantiate(obj, spawnPosition, Quaternion.identity);
        Debug.Log($"Spawned object in zone {zoneIndex} at position {spawnPosition}");
    }
}