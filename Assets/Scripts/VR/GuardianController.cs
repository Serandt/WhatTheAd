using UnityEngine;
using System.Collections.Generic;
using Oculus.Platform;
using Oculus.Platform.Models;

public class GuardianController : MonoBehaviour
{
    private OVRBoundary boundary;

    [System.Obsolete]
    void Start()
    {
        boundary = new OVRBoundary();
        Debug.Log(boundary);
        boundary.SetVisible(false);
        
    }

    [System.Obsolete]
    void Update()
    {
        Vector3 playerPosition = transform.position;
        OVRBoundary.BoundaryTestResult result = boundary.TestPoint(playerPosition, OVRBoundary.BoundaryType.PlayArea);

        if (result.IsTriggering)
        {
            Debug.Log("You are close to or outside the Guardian boundary!");
            // Add your custom logic here, like showing a warning UI or taking corrective actions
        }
    }
}
