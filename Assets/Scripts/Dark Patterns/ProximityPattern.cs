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


    public float fleeDistance = 2.5f;
    public NavMeshAgent agent;
    public float checkDistance = 4f;
    
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
        Vector3[] directions = new Vector3[] {
        Quaternion.Euler(0, 90, 0) * directionToPlayer,    // Rechts
        Quaternion.Euler(0, -90, 0) * directionToPlayer,   // Links
        Quaternion.Euler(0, 180, 0) * directionToPlayer,   // Rückwärts
        Quaternion.Euler(0, 45, 0) * directionToPlayer,    // Diagonal rechts vorwärts
        Quaternion.Euler(0, -45, 0) * directionToPlayer,   // Diagonal links vorwärts
        Quaternion.Euler(0, 135, 0) * directionToPlayer,   // Diagonal rechts rückwärts
        Quaternion.Euler(0, -135, 0) * directionToPlayer   // Diagonal links rückwärts
    };

        foreach (var dir in directions)
        {
            Vector3 sideStep = dir * fleeDistance;
            if (NavMesh.SamplePosition(transform.position + sideStep, out NavMeshHit hit, checkDistance, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
                return; 
            }
        }
    }
}

  



