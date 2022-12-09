using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    //Patrol waypoint
    public Transform patrolWayPoints;
    
    //Stop top shoot distance
    public float shootingDistance = 7f;
    
    //Distance for transitioning from attack state to chase state
    public float chasingDistance = 8f;
    
    //Reference to Blackboard
    private Blackboard bb;
    
    //Index to current patrol waypoint
    private int wayPointIndex = 0;
    
    //Latest detected player position
    private Vector3 playerLastSighted;
    
    //Previous detected player position;
    private Vector3 playerPreviousSighted;
    
    //Array for patrol waypoints
    private Vector3[] wayPoints;
    
    //Reference to SenseMemory
    private SenseMemory memory;
    
    //FSM State
    private FSMState state;
    
    private NavMeshAgent navMeshAgent;
    
    private void Start()
    {
        bb = FindObjectOfType<Blackboard>();
        playerLastSighted = bb.resetPosition;
        playerPreviousSighted = bb.resetPosition;
        
        memory = GetComponent<SenseMemory>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        state = FSMState.PATROL;
        wayPoints = new Vector3[patrolWayPoints.childCount];

        //Set the first patrol waypoint position
        int count = 0;
        foreach (Transform child in patrolWayPoints)
        {
            wayPoints[count] = child.position;
            count++;
        }

        navMeshAgent.SetDestination(wayPoints[0]);
    }

    private void Update()
    {
        //if the in player position in Blackboard is not equal to player 
        if (bb.playerLastPosition != playerPreviousSighted)
        {
            playerLastSighted = bb.playerLastPosition;
        }

        switch (state)
        {
            case FSMState.PATROL:
                UpdatePatrol();
                break;
            case FSMState.CHASE:
                UpdateChase();
                break;
            case FSMState.ATTACK:
                UpdateAttack();
                break;
            case FSMState.DIE:
                break;
        }
    }

    public void UpdatePatrol()
    {
        state = FSMState.PATROL;
        
        if (Vector3.Distance(transform.position, navMeshAgent.destination) < 3)
        {
            if (wayPointIndex == wayPoints.Length - 1)
            {
                wayPointIndex = 0;
            }
            else
            {
                wayPointIndex++;
            }
            navMeshAgent.SetDestination(wayPoints[wayPointIndex]);
        }

        if (playerLastSighted != bb.playerLastPosition)
        {
            state = FSMState.CHASE;
        }

        playerPreviousSighted = bb.playerLastPosition;
    }

    public void UpdateChase()
    {
        Debug.Log("Entering chase");
        state = FSMState.CHASE;

        //Set the AI to go check for player's last seen position 
        navMeshAgent.SetDestination(playerLastSighted);
        
        //Check if AI can see the player within shooting distance
        if (Vector3.Distance(transform.position, navMeshAgent.destination) < shootingDistance && CanSeePlayer())
        {
            
            state = FSMState.ATTACK;
        }
    }

    public void UpdateAttack()
    {
        Debug.Log("Entering attack");

        state = FSMState.ATTACK;

        if (playerLastSighted == bb.resetPosition)
        {
            state = FSMState.PATROL;
        }

        if (playerLastSighted != playerPreviousSighted &&
            Vector3.Distance(transform.position, playerPreviousSighted) > chasingDistance)
        {
            state = FSMState.CHASE;
        }
    }

    //Check if AI has player' position in its memory
    public bool CanSeePlayer()
    {
        if (memory != null)
        {
            return memory.FindInList();
        }

        return false;
    }
}
