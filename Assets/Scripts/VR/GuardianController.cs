using UnityEngine;
using System.Collections.Generic;
using Oculus.Platform;
using Oculus.Platform.Models;
using TMPro;

public class GuardianController : MonoBehaviour
{
    private OVRBoundary boundary;

    void Start()
    {
        boundary = new OVRBoundary();
    }

    void Update()
    {
        CheckBoundaryCollision();
    }

    void CheckBoundaryCollision()
    {
        // Check if the boundary system is configured
        if (OVRManager.boundary.GetConfigured())
        {
            // Test the head and hand positions
            List<OVRBoundary.BoundaryTestResult> testResults = new List<OVRBoundary.BoundaryTestResult>();
            testResults.Add(boundary.TestNode(OVRBoundary.Node.Head, OVRBoundary.BoundaryType.OuterBoundary));
            testResults.Add(boundary.TestNode(OVRBoundary.Node.HandLeft, OVRBoundary.BoundaryType.OuterBoundary));
            testResults.Add(boundary.TestNode(OVRBoundary.Node.HandRight, OVRBoundary.BoundaryType.OuterBoundary));

            foreach (var result in testResults)
            {
                if (result.IsTriggering)
                {
                    Debug.Log("Player is colliding with the Guardian boundary at " + result.ClosestPoint);
     
                    GameManager.Instance.distanceDisplay.GetComponent<TextMeshPro>().text = "Collision";
                }
                else
                {
                    float distance = Vector3.Distance(result.ClosestPoint, OVRManager.instance.transform.position);
                    Debug.Log("Player's head is within the Guardian boundary. Distance to boundary: " + distance + " meters.");
                    GameManager.Instance.distanceDisplay.GetComponent<TextMeshPro>().text = distance.ToString();
                }
            }
        }
        else
        {
            GameManager.Instance.distanceDisplay.GetComponent<TextMeshPro>().text = "Error";
            Debug.LogWarning("Boundary system is not configured.");
        }
    }
}
