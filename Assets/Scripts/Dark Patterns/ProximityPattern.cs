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


    public float fleeDistance;
    public NavMeshAgent agent;
    public float checkDistance;
    
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

            // Überprüfen, ob der Fluchtpunkt gültig ist
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



    /*void FindPathAlongEdge(Vector3 directionToPlayer)
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
        // Keine gültigen Punkte gefunden: Letzter Versuch könnte ein über den Spieler springen sein
        // (benötigt Erweiterungen des NavMesh und des Agenten)
    }
    else
    {
        agent.SetDestination(hitLeft.position);
    }
    }*/

    void FindPathAlongEdge(Vector3 directionToPlayer)
    {
        float maxEscapeDistance = fleeDistance;
        float angleStep = 10;
        int numberOfRays = 36;

        bool pathFound = false;
        Vector3 bestPosition = Vector3.zero;
        float bestDistance = 0;

        for (int i = 0; i < numberOfRays; i++)
        {
            float angle = i * angleStep;
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;  // Standardisiert auf vorwärts gerichtet
            Vector3 testPosition = transform.position + direction * maxEscapeDistance;

            if (NavMesh.SamplePosition(testPosition, out NavMeshHit hit, checkDistance, NavMesh.AllAreas))
            {
                float distanceToHit = Vector3.Distance(transform.position, hit.position);
                if (!pathFound || distanceToHit > bestDistance)
                {
                    bestPosition = hit.position;
                    bestDistance = distanceToHit;
                    pathFound = true;
                }
            }
        }

        if (pathFound)
        {
            agent.SetDestination(bestPosition);
        }
        else
        {
            Debug.Log("Kein Fluchtweg gefunden, Agent wartet...");
        }
    }
}

  



