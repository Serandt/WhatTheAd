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

   

      /*  if (distance < fleeDistance)
        {
            Vector3 directionToPlayer = transform.position - DarkPatternManager.Instance.GetCameraPos();
            directionToPlayer.Normalize();  
            Vector3 fleeDestination = transform.position + directionToPlayer * fleeDistance;  

            
            if (NavMesh.SamplePosition(fleeDestination, out NavMeshHit hit, fleeDistance, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
        }*/
    }
}

  



