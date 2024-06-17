using System;
using Oculus.Interaction;
using UnityEngine;

public class BoundaryManager : MonoBehaviour
{
    public Transform hmd;
    public Transform rightController;
    public Transform leftController;
    private Material defaultMaterial;
    public Material danger1;
    public Material danger2;
    public Material danger3;

    private float[] thresholdWidths = new float[3];
    private float[] thresholdDepths = new float[3];

    private Vector3 surfaceCenter;
    private MeshRenderer meshRenderer;

    private int collideWithBoundaryCounter = 0;

    public static BoundaryManager Instance;

    private void Start()
    {
        Instance = this;

        // Cache the MeshRenderer component at start
        meshRenderer = GetComponent<MeshRenderer>();
        defaultMaterial = meshRenderer.material;

        // Initialize boundaries based on object scale
        Vector3 scale = transform.localScale;
        surfaceCenter = new Vector3(transform.position.x, transform.position.y + scale.y / 2, transform.position.z);

        InitializeThresholds(scale.x, thresholdWidths);
        InitializeThresholds(scale.z, thresholdDepths);
    }

    private void InitializeThresholds(float dimension, float[] thresholds)
    {
        float increment = dimension / 4;
        for (int i = 0; i < 3; i++)
        {
            thresholds[i] = increment * (i + 1);
        }
    }

    private void Update()
    {
        Vector3 distanceToCenter = hmd.transform.position - surfaceCenter;
        float absDistanceX = Math.Abs(distanceToCenter.x);
        float absDistanceZ = Math.Abs(distanceToCenter.z);

        // Determine the material based on the distance thresholds
        Material newMaterial = DetermineMaterial(absDistanceX, absDistanceZ);
        if (meshRenderer.material != newMaterial)
        {
            meshRenderer.material = newMaterial;
        }

        CheckIfOutsideZone();
    }

    private Material DetermineMaterial(float absDistanceX, float absDistanceZ)
    {
        if (absDistanceX > thresholdWidths[2] || absDistanceZ > thresholdDepths[2])
        {
            return danger3;
        }
        else if (absDistanceX > thresholdWidths[1] || absDistanceZ > thresholdDepths[1])
        {
            return danger2;
        }
        else if (absDistanceX > thresholdWidths[0] || absDistanceZ > thresholdDepths[0])
        {
            return danger1;
        }
        return defaultMaterial;
    }

    void CheckIfOutsideZone()
    {
        if (OutsideBoundary(hmd) || OutsideBoundary(leftController) || OutsideBoundary(rightController))
        {


            if (!GameManager.Instance.outOfBoundary)
            {
                OnLeaveZone();
            }
        }
        else
        {
            GameManager.Instance.outOfBoundary = false;
        }
    }

    bool OutsideBoundary(Transform transform)
    {
        return !this.GetComponent<Collider>().bounds.Contains(transform.position);
    }

    void OnLeaveZone()
    {
        GameManager.Instance.outOfBoundary = true;
        GameManager.Instance.RemoveLive();
        SaveBoundaryCollision();
    }

    void SaveBoundaryCollision()
    {
        if (!GameManager.Instance.playTutorial)
        {
            collideWithBoundaryCounter += 1;
            if (OutsideBoundary(hmd) && OutsideBoundary(leftController) && OutsideBoundary(rightController))
            {
                GameData.Instance.AddBoundaryCollision(Time.time - GameManager.Instance.startTime, collideWithBoundaryCounter, true, true);
            }
            else if (OutsideBoundary(leftController) || OutsideBoundary(rightController))
            {
                GameData.Instance.AddBoundaryCollision(Time.time - GameManager.Instance.startTime, collideWithBoundaryCounter, false, true);
            }
            else
            {
                GameData.Instance.AddBoundaryCollision(Time.time - GameManager.Instance.startTime, collideWithBoundaryCounter, true, false);
            }
        }
       
    }
}



