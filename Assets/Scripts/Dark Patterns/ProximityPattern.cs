using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProximityPattern : DarkPattern
{

    public float fleeDistance;
    public NavMeshAgent agent;
    public float checkDistance;
    private float yPosition;
    
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        yPosition = gameObject.transform.position.y;
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

            //Brauchen wir das??
            potentialDestination.y = yPosition;

            // �berpr�fen, ob der Fluchtpunkt g�ltig ist
            if (NavMesh.SamplePosition(potentialDestination, out NavMeshHit hit, checkDistance, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
            else
            {

                FindPathAlongEdge(directionToPlayer);
            }
        }
    }





    void FindPathAlongEdge(Vector3 directionToPlayer)
    {
        Vector3[] directions = new Vector3[] {
        Quaternion.Euler(0, 90, 0) * directionToPlayer,    // Rechts
        Quaternion.Euler(0, -90, 0) * directionToPlayer,   // Links
        Quaternion.Euler(0, 180, 0) * directionToPlayer,   // R�ckw�rts
        Quaternion.Euler(0, 45, 0) * directionToPlayer,    // Diagonal rechts vorw�rts
        Quaternion.Euler(0, -45, 0) * directionToPlayer,   // Diagonal links vorw�rts
        Quaternion.Euler(0, 135, 0) * directionToPlayer,   // Diagonal rechts r�ckw�rts
        Quaternion.Euler(0, -135, 0) * directionToPlayer   // Diagonal links r�ckw�rts
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

  



