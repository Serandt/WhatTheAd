using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkPatternManager : MonoBehaviour
{
    public OVRCameraRig overCameraRig;


    public float startInterval = 5.0f;
    public float endInterval = 1.0f;
    public float decreaseTime = 120.0f;

    public float currentInterval;
    private float timeElapsed;
    private float lastSpawnTime;

    System.Random rand = new System.Random();
    private int currentSpawnIndex = 0;

    public int spawnCount = 0;

    public float currentTimeSpawner;

    public GameObject proximityAd;
    public GameObject falseFriendAd;
    public GameObject messagePopUpAd;
    public GameObject tutorialAd;

    public List<DarkPattern> activePatterns = new List<DarkPattern>();
    public List<GameObject> spawnPoints = new List<GameObject>();

    public GameObject activeDarkPattern;

    public static DarkPatternManager Instance;

    private void Awake()
    {
        Instance = this;
        Vector3 cameraPos = GetCameraPos();
    }

    private void Start()
    {
        SetUpSpawnTimes();
        ShuffleSpawnPoints();
    }

    private void Update()
    {
        if (GameManager.Instance.playGame)
        {
            timeElapsed += Time.deltaTime;

            if (Time.time - lastSpawnTime >= currentInterval)
            {
                SpawnAd(activeDarkPattern, spawnCount);
                lastSpawnTime = Time.time;

                currentInterval = Mathf.Lerp(startInterval, endInterval, timeElapsed / decreaseTime);
                spawnCount++;
            }
        }
    }

    public Vector3 GetCameraPos()
    {
        overCameraRig = GameObject.Find("OVRCameraRig").GetComponent<OVRCameraRig>();
        return overCameraRig.centerEyeAnchor.position;
    }

    public void removeDarkPatternFromList(GameObject obj)
    {
        DarkPattern darkPattern = obj.GetComponent<DarkPattern>();
        darkPattern.Close();

        activePatterns.Remove(activePatterns.Find(item => item.ID == darkPattern.ID));
    }

    public void SetUpSpawnTimes()
    {
        currentInterval = startInterval;
        timeElapsed = 0f;
        lastSpawnTime = Time.time;
    }

    public void SpawnAd(GameObject objectToSpawnPrefab, int spawnCount)
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogError("Spawn points list is empty.");
            return;
        }

        if (currentSpawnIndex >= spawnPoints.Count)
        {
            ShuffleSpawnPoints(); // Reshuffle the list when all points have been used
            currentSpawnIndex = 0; // Reset index
        }

        Vector3 spawnPosition = spawnPoints[currentSpawnIndex].transform.position;
        Debug.Log(spawnPoints[currentSpawnIndex].name);

        switch (objectToSpawnPrefab.tag)
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

        GameObject spawnedObject = Instantiate(objectToSpawnPrefab, spawnPosition, Quaternion.identity);
        DarkPattern darkPattern = spawnedObject.GetComponent<DarkPattern>();
        if (darkPattern != null)
        {
            darkPattern.Initialize(spawnCount, spawnedObject);
            activePatterns.Add(darkPattern);

        }
        currentSpawnIndex++;

    }

    private void ShuffleSpawnPoints()
    {
        int n = spawnPoints.Count;
        while (n > 1)
        {
            n--;
            int k = rand.Next(n+1);
            GameObject temp = spawnPoints[k];
            spawnPoints[k] = spawnPoints[n];
            spawnPoints[n] = temp;
        }
    }



}