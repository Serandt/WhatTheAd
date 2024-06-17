using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProximityPattern : DarkPattern
{
    /* public float fleeDistance = 0.5f; 

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


    public float fleeDistance = 1f;
    public NavMeshAgent agent;
    public float checkDistance = 2f;
    
    private void Start()
    {
    agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Vector3 playerPosition = DarkPatternManager.Instance.GetCameraPos();
        float distance = Vector3.Distance(transform.position, playerPosition);

        if (distance < fleeDistance)
        {
            Vector3 directionToPlayer = transform.position - playerPosition;
            directionToPlayer.Normalize();
            Vector3 potentialDestination = transform.position + directionToPlayer * fleeDistance;

            // �berpr�fen, ob der Fluchtpunkt g�ltig ist
            if (NavMesh.SamplePosition(potentialDestination, out NavMeshHit hit, checkDistance, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
            else
            {
                // Finden Sie eine alternative Route entlang des Randes
                FindPathAlongEdge(directionToPlayer);
            }
        }
    }

    void FindPathAlongEdge(Vector3 directionToPlayer)
    {
    // Probieren Sie, nach links oder rechts auszuweichen
    Vector3 sideStep = Quaternion.Euler(0, 90, 0) * directionToPlayer * fleeDistance;
    if (!NavMesh.SamplePosition(transform.position + sideStep, out NavMeshHit hitLeft, checkDistance, NavMesh.AllAreas))
    {
        sideStep = Quaternion.Euler(0, -90, 0) * directionToPlayer * fleeDistance;
        if (NavMesh.SamplePosition(transform.position + sideStep, out NavMeshHit hitRight, checkDistance, NavMesh.AllAreas))
        {
            agent.SetDestination(hitRight.position);
        }
        // Keine g�ltigen Punkte gefunden: Letzter Versuch k�nnte ein �ber den Spieler springen sein
        // (ben�tigt Erweiterungen des NavMesh und des Agenten)
    }
    else
    {
        agent.SetDestination(hitLeft.position);
    }
    }
}

  



