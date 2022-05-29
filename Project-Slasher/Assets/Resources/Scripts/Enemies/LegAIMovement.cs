using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class LegAIMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private LayerMask whatIsGround;
    private Vector3 walkPoint;
    bool walkPointSet;
    [SerializeField] private float walkPointRange;

    private void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(!GetComponent<HeadAIMovement>().GetPlayerSpotted() && GetComponent<HeadAIMovement>().Activate())
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        else
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f) 
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(this.transform.position.x + randomX, this.transform.position.y, this.transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
}
