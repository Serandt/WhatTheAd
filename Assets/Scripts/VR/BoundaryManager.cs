using System;
using UnityEngine;

public class BoundaryManager : MonoBehaviour
{
    public GameObject hmd;
    private Material defaultMaterial;
    public Material danger1;
    public Material danger2;
    public Material danger3;


    private float thresholdWidth1;
    private float thresholdWidth2;
    private float thresholdWidth3;

    private float thresholdDepth1;
    private float thresholdDepth2;
    private float thresholdDepth3;

    private float width;
    private float depth;
    private Vector3 surfaceCenter;


    private Vector3 currentPosition;

    private void Start()
    {
        defaultMaterial = GetComponent<MeshRenderer>().material;
        Vector3 center = transform.position;
        width = transform.localScale.x;
        depth = transform.localScale.z;
        surfaceCenter = new Vector3(center.x, center.y + transform.localScale.y / 2, center.z);

        float widthIncrease = width / 4;
        float depthIncrease = depth / 4;

        thresholdWidth1 = widthIncrease;
        thresholdWidth2 = widthIncrease * 2;
        thresholdWidth3 = widthIncrease * 3;

        thresholdDepth1 = depthIncrease;
        thresholdDepth2 = depthIncrease * 2;
        thresholdDepth3 = depthIncrease * 3;

        Debug.Log(thresholdWidth1);
        Debug.Log(thresholdWidth2);
        Debug.Log(thresholdWidth3);

    }

    private void Update()
    {
        currentPosition = hmd.transform.position;
        Vector3 distanceToCenter = currentPosition - surfaceCenter;
        float absDistanceX = Math.Abs(distanceToCenter.x);
        float absDistanceZ = Math.Abs(distanceToCenter.z);



        // Check the furthest thresholds first
        if (absDistanceX > thresholdWidth3 || absDistanceZ > thresholdDepth3)
        {
            GetComponent<MeshRenderer>().material = danger3;
            Debug.Log("Zone4");
        }
        else if (absDistanceX > thresholdWidth2 || absDistanceZ > thresholdDepth2)
        {
            GetComponent<MeshRenderer>().material = danger2;
            Debug.Log("Zone3");
        }
        else if (absDistanceX > thresholdWidth1 || absDistanceZ > thresholdDepth1)
        {
            GetComponent<MeshRenderer>().material = danger1;
            Debug.Log("Zone2");
        }
        else
        {
            // Only apply the default material if none of the danger zones are triggered
            GetComponent<MeshRenderer>().material = defaultMaterial;
            Debug.Log("Zone1");
        }
    }
}



