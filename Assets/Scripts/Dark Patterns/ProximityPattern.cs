using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProximityPattern : DarkPattern
{
    public float fleeDistance = 1.0f; 
    
    public NavMeshAgent agent; 
  
    public float fleeMultiplier = 1.5f; 

    void Update()
    {
        float distance = Vector3.Distance(transform.position, DarkPatternManager.Instance.GetCameraPos());

        if (distance < fleeDistance)
        {
            Vector3 fleeDirection = (transform.position - DarkPatternManager.Instance.GetCameraPos()).normalized;
            Vector3 fleePoint = transform.position + fleeDirection * fleeDistance * fleeMultiplier;

            // Stelle sicher, dass der Fluchtpunkt auf dem NavMesh liegt
            NavMeshHit hit;
            if (NavMesh.SamplePosition(fleePoint, out hit, fleeDistance * fleeMultiplier, NavMesh.AllAreas))
            {
                Debug.Log("inIF");
                agent.SetDestination(hit.position);
            }
        }
    }
}

/*void Start()
    {
        rb = GetComponent<Rigidbody>();

        MeshRenderer renderer = GetComponentInParent<MeshRenderer>();
        boundaryMin = renderer.bounds.min;
        boundaryMax = renderer.bounds.max;

        NavMeshAgent navAgent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        if (DarkPatternManager.Instance.overCameraRig != null)
        {



            float distance = Vector3.Distance(transform.position, DarkPatternManager.Instance.GetCameraPos());
            if (distance < fleeDistance)
            {
                //Vector3 direction = (transform.position - DarkPatternManager.Instance.GetCameraPos()).normalized;
                Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
                Vector3 direction = ((transform.position - DarkPatternManager.Instance.GetCameraPos()).normalized + randomDirection).normalized;
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

