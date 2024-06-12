using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkPatternManager : MonoBehaviour
{
    public OVRCameraRig overCameraRig;


    public float spawnTimer = 10f;
    public int maxSpawns = 10;

    private int spawnCount = 0;
    private float currentTimeSpawner;

    [SerializeField] private GameObject proximityAd;
    [SerializeField] private GameObject falseFriendAd;

    public List<DarkPattern> activePatterns = new List<DarkPattern>();

    public static DarkPatternManager Instance;

    private void Awake()
    {
        Instance = this;
    }



    public void Update()
    {
        currentTimeSpawner -= Time.deltaTime;

        if (currentTimeSpawner <= 0 && spawnCount < maxSpawns)
        {
            BoundaryZones.Instance.SpawnAd(falseFriendAd, maxSpawns, spawnCount);
            currentTimeSpawner = spawnTimer;
            
            spawnCount++;
        }
    }

    void Start()
    {
        currentTimeSpawner = spawnTimer;
        Vector3 cameraPos = GetCameraPos();
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