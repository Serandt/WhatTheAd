using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkPatternManager : MonoBehaviour
{
    public OVRCameraRig overCameraRig;


    public float spawnTimer = 1.0f;
    public float maxSpawns = 10;

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
            BoundaryZones.Instance.SpawnAd(proximityAd, 10, spawnCount);
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

}