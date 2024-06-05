using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityPattern : MonoBehaviour
{
    public float fleeDistance = 5.0f; // Distance at which the cube starts to flee
    public float moveSpeed = 5.0f; // Speed at which the cube moves

    private Rect cubeMovementArea; // Area where the cube can move

    void Start()
    {
        // Assume the cube is already spawned by the GameZone script
        // Calculate the movement area based on the current zone
        int zoneIndex = GameZone.Instance.CalculateZone(transform.position,
            GameZone.Instance.gameAreaSize.x / (2 * GameZone.Instance.numberOfZones),
            GameZone.Instance.gameAreaSize.y / (2 * GameZone.Instance.numberOfZones),
            GameZone.Instance.numberOfZones);

        Rect outerZone = GameZone.Instance.zones[zoneIndex - 1];
        Rect innerZone = zoneIndex > 1 ? GameZone.Instance.zones[zoneIndex - 2] : new Rect(Vector2.zero, Vector2.zero);

        float xMin = outerZone.xMin;
        float xMax = outerZone.xMax;
        float yMin = outerZone.yMin;
        float yMax = outerZone.yMax;

        if (zoneIndex > 1)
        {
            xMin = innerZone.xMax;
            xMax = innerZone.xMin;
            yMin = innerZone.yMax;
            yMax = innerZone.yMin;
        }

        cubeMovementArea = new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
    }

    void Update()
    {
        if (DarkPatternManager.Instance.overCameraRig != null)
        {
            float distance = Vector3.Distance(transform.position, DarkPatternManager.Instance.GetCameraPos());

            if (distance < fleeDistance)
            {
                Vector3 direction = (transform.position - DarkPatternManager.Instance.GetCameraPos()).normalized;
                Vector3 newPosition = transform.position + direction * Time.deltaTime * moveSpeed;

                // Ensure the cube stays within the movement area
                newPosition.x = Mathf.Clamp(newPosition.x, cubeMovementArea.xMin, cubeMovementArea.xMax);
                newPosition.z = Mathf.Clamp(newPosition.z, cubeMovementArea.yMin, cubeMovementArea.yMax);

                transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.z);
            }
        }
    }

}
