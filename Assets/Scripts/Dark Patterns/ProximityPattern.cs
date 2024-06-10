using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityPattern : MonoBehaviour
{
    public float fleeDistance = 5.0f; // Distance at which the cube starts to flee
    public float moveSpeed = 5.0f; // Speed at which the cube moves

    void Update()
    {
        if (DarkPatternManager.Instance.overCameraRig != null)
        {
            float distance = Vector3.Distance(transform.position, DarkPatternManager.Instance.GetCameraPos());

            if (distance < fleeDistance)
            {
                Vector3 direction = (transform.position - DarkPatternManager.Instance.GetCameraPos()).normalized;
                Vector3 newPosition = transform.position + direction * Time.deltaTime * moveSpeed;


                transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.z);
            }
        }
    }

}
