using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkPatternManager : MonoBehaviour
{
    [SerializeField] public OVRCameraRig overCameraRig;
    [SerializeField] private GameObject proximityAd;

    public static DarkPatternManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Vector3 cameraPos = GetCameraPos();
        Debug.Log("Camera Position: " + cameraPos);
        SpawnyAd(proximityAd, 2);
    }

    public Vector3 GetCameraPos()
    {
        overCameraRig = GameObject.Find("OVRCameraRig").GetComponent<OVRCameraRig>();
        return overCameraRig.centerEyeAnchor.position;
    }

    public void SpawnyAd(GameObject ad,  int zone)
    {
        GameZone.Instance.SpawnObjectInZone(ad, zone, 1, 1);
    }
}