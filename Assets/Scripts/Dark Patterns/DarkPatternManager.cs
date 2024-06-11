using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkPatternManager : MonoBehaviour
{
    public OVRCameraRig overCameraRig;


    public float spawnTimer = 60f;
    public int maxSpawns = 10;

    private int spawnCount = 0;
    private float currentTimeSpawner;

    [SerializeField] private GameObject proximityAd;

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
            currentTimeSpawner = spawnTimer;
            
            spawnCount++;
        }
    }

    void Start()
    {
        currentTimeSpawner = spawnTimer;
        Vector3 cameraPos = GetCameraPos();
        BoundaryZones.Instance.SpawnAd(proximityAd, maxSpawns, spawnCount);
    }

    public Vector3 GetCameraPos()
    {
        overCameraRig = GameObject.Find("OVRCameraRig").GetComponent<OVRCameraRig>();
        return overCameraRig.centerEyeAnchor.position;
    }

}