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
    
    //最近感知到的玩家位置
    private Vector3 playerLastSighted;
    
    //上次的玩家位置
    private Vector3 playerLastSightBySelf;
    
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
        playerLastSightBySelf = bb.resetPosition;
        
        memory = GetComponent<SenseMemory>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        //set initial state
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
        //if the in player position in Blackboard is not equal to the location last detected by this enemy
        if (bb.playerLastSeenPosition != playerLastSightBySelf)
        {
            Debug.Log("not equal");
            playerLastSighted = bb.playerLastSeenPosition;
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
            default:
                break;
        }

        playerLastSightBySelf = bb.playerLastSeenPosition;
    }

    public void UpdatePatrol()
    {
        state = FSMState.PATROL;
        
        //if the enemy gets close enough to current patrol point, set next or reset patrol point by index 
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

        if (playerLastSighted != bb.resetPosition)
        {
            state = FSMState.CHASE;
        }
    }

    public void UpdateChase()
    {
        Debug.Log("Entering chase");
        state = FSMState.CHASE;

        //Set the AI to go check for player's last seen position 
        navMeshAgent.SetDestination(playerLastSighted);
        
        //Check if AI can see the player within shooting distance
        if (Vector3.Distance(transform.position, navMeshAgent.destination) < shootingDistance && HavePlayerSightMemory())
        {
            state = FSMState.ATTACK;
        }
        else
        {
            Debug.Log("Lose sight of the player");
            state = FSMState.PATROL;
        }
    }

    public void UpdateAttack()
    {
        Debug.Log("Entering attack");

        state = FSMState.ATTACK;

        if (playerLastSighted == bb.resetPosition)
        {
            Debug.Log("Returning to patrol");
            state = FSMState.PATROL;
        }

        if (playerLastSighted != playerLastSightBySelf &&
            Vector3.Distance(transform.position, playerLastSightBySelf) > chasingDistance)
        {
            state = FSMState.CHASE;
        }
    }

    //Check if AI has player' position in its memory
    public bool HavePlayerSightMemory()
    {
        if (memory != null)
        {
            return memory.FindInList();
        }

        return false;
    }
}
