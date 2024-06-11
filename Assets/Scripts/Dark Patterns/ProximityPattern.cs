using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProximityPattern : DarkPattern
{
    public float fleeDistance = 0.5f; 
    
    public NavMeshAgent agent; 
  
    public float fleeMultiplier = 0.1f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, DarkPatternManager.Instance.GetCameraPos());
        
        if (distance < fleeDistance)
        {
            Vector3 directionToPlayer = transform.position - DarkPatternManager.Instance.GetCameraPos();
            agent.SetDestination(directionToPlayer);
        }

        /*if (distance < fleeDistance)
        {
            Vector3 fleeDirection = (transform.position - DarkPatternManager.Instance.GetCameraPos()).normalized;
            Vector3 fleePoint = transform.position + fleeDirection;// * fleeDistance * fleeMultiplier;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(fleePoint, out hit, fleeDistance * fleeMultiplier, NavMesh.AllAreas))
            {
                agent.SetDestination(directionToPlayer);
            }
        }*/
    }

    protected override void OnClose()
    {
        base.OnClose();
        Debug.Log("Proximity Ad closed.");
    }
}


