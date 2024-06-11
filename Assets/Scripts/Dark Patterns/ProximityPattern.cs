using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityPattern : DarkPattern
{
    public float fleeDistance = 5.0f; 
    public float moveSpeed = 5.0f; 
    private Rigidbody rb; 
    private Vector3 boundaryMin; 
    private Vector3 boundaryMax;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        MeshRenderer renderer = GetComponentInParent<MeshRenderer>();
        boundaryMin = renderer.bounds.min;
        boundaryMax = renderer.bounds.max;
    }

    void FixedUpdate()
    {
        if (DarkPatternManager.Instance.overCameraRig != null)
        {
            float distance = Vector3.Distance(transform.position, DarkPatternManager.Instance.GetCameraPos());
            if (distance < fleeDistance)
            {
                Vector3 direction = (transform.position - DarkPatternManager.Instance.GetCameraPos()).normalized;
                Vector3 newPosition = rb.position + direction * moveSpeed * Time.fixedDeltaTime;

                newPosition.x = Mathf.Clamp(newPosition.x, boundaryMin.x, boundaryMax.x);
                newPosition.y = transform.position.y;
                newPosition.z = Mathf.Clamp(newPosition.z, boundaryMin.z, boundaryMax.z);

                rb.MovePosition(newPosition);
            }

         
        }
    
    }
    protected override void OnClose()
    {
        base.OnClose();
        Debug.Log("Proximity Ad closed.");
    }

    /* void Update()
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
     }*/

}
