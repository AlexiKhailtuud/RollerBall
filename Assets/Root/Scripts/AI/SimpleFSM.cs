using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using Random = System.Random;

public class SimpleFSM : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    
    public FSMState curState;
    public bool isDead;
    
    private GameObject[] patrolPointList;
    private Vector3 destPos;
    public float arriveDistance = 2f;

    private int curIndex = 0;
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        FSMUpdate();
    }

    private void FSMUpdate()
    {
        switch (curState)
        {
            case FSMState.PATROL:
                UpdatePatrolState();
                break;
            case FSMState.CHASE:
                break;
            case FSMState.ATTACK:
                break;
            case FSMState.DIE:
                break;
        }
    }
    
    private void Init()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = 0f;
        
        curState = FSMState.PATROL;
        isDead = false;
        patrolPointList = GameObject.FindGameObjectsWithTag("PatrolPoints");
        
        FindNextPoint();
    }

    private void UpdatePatrolState()
    {
        Debug.Log("lengnth: "+ Vector3.Distance(transform.position, destPos));
        
        if (Vector3.Distance(transform.position, destPos) <= arriveDistance)
        {
            FindNextPoint();
        }

        //Vector3 v = new Vector3(1, 1, 1);
        //Vector3 v1 = Quaternion.Euler(0, 90, 0) * v;
        
        //Quaternion target = Quaternion.LookRotation(destPos - transform.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5f);
    }

    private void FindNextPoint()
    {
        Debug.Log("FindingNextDes");
        int randIndex = UnityEngine.Random.Range(0, patrolPointList.Length);
        
        // int nextIndex = curIndex + 1;
        // if (nextIndex++ >= patrolPointList.Length)
        // {
        //     nextIndex = 0;
        // }
        
        destPos = patrolPointList[randIndex].transform.position;
        navMeshAgent.SetDestination(destPos);
    }
}
