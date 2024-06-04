using UnityEngine;
using System.Collections.Generic;
using Oculus.Platform;
using Oculus.Platform.Models;

public class GuardianController : MonoBehaviour
{

    [System.Obsolete]
    void Start()
    {
        OVRManager.boundary.GetConfigured();
        OVRManager.boundary.GetVisible();
    }

    [System.Obsolete]
    void Update()
    {
        CheckBoundaryCollision();
    }

    [System.Obsolete]
    void CheckBoundaryCollision()
    {
        // Get the boundary test result
        OVRBoundary.BoundaryTestResult boundaryTestResult = OVRManager.boundary.TestNode(OVRBoundary.Node.Head, OVRBoundary.BoundaryType.PlayArea);

        // Check if the player is colliding with the boundary
        if (boundaryTestResult.IsTriggering)
        {
            Debug.Log("Player is colliding with the Guardian boundary!");
            // Add your custom logic here, e.g., notify the player, change UI, etc.
        }
    }
}
