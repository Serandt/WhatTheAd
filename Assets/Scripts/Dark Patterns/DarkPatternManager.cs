using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkPatternManager : MonoBehaviour
{
    public OVRCameraRig overCameraRig;


    public float spawnTimer = 10f;
    public int maxSpawns = 10;

    public int spawnCount = 0;

    private float currentTimeSpawner;

    public GameObject proximityAd;
    public GameObject falseFriendAd;
    public GameObject messagePopUpAd;
    public GameObject tutorialAd;

    public List<DarkPattern> activePatterns = new List<DarkPattern>();

    public GameObject activeDarkPattern;

    public static DarkPatternManager Instance;

    private void Awake()
    {
        Instance = this;
        currentTimeSpawner = spawnTimer;
        Vector3 cameraPos = GetCameraPos();
    }



    public void Update()
    {
        currentTimeSpawner -= Time.deltaTime;

        if (currentTimeSpawner <= 0 && spawnCount < maxSpawns && activeDarkPattern != null && GameManager.Instance.playGame)
        {
            BoundaryZones.Instance.SpawnAd(activeDarkPattern, maxSpawns, spawnCount);
            currentTimeSpawner = spawnTimer;
     
            spawnCount++;
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




}