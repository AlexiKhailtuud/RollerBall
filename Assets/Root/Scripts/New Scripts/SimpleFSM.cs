using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using Random = System.Random;

public class SimpleFSM : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    
    public FSMState curState;
    
    public bool isDead;
    
    private GameObject[] patrolPointList;
    private Vector3 destPos;
    public float arriveDistance = 2f;

    private Transform playerTransform;
    private float chaseDistance = 17.5f;
    private float attackDistance = 10f;

    public GameObject bulletPrefab;
    public GameObject bulletSpawnPoint;

    private float fireRate;
    private float elapsedTime;
    
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
                UpdateChaseState();
                break;
            case FSMState.ATTACK:
                UpdateAttackState();
                break;
            case FSMState.DIE:
                break;
        }
        elapsedTime += Time.deltaTime;
    }
    
    private void Init()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = 0f;
        
        curState = FSMState.PATROL;
        isDead = false;
        elapsedTime = 0f;
        fireRate = 0.35f;
        patrolPointList = GameObject.FindGameObjectsWithTag("PatrolPoints");
        
        FindNextPoint();
        
        GameObject objPlayer = GameObject.FindWithTag("Player");
        playerTransform = objPlayer.transform;
    }

    private void UpdatePatrolState()
    {
        //Debug.Log("length: "+ Vector3.Distance(transform.position, destPos));
        
        if (Vector3.Distance(transform.position, destPos) <= arriveDistance)
        {
            FindNextPoint();
        }
        else if (Vector3.Distance(transform.position, playerTransform.position) <= chaseDistance)
        {
            Debug.Log("entering chase");
            curState = FSMState.CHASE;
        }
    }
    
    private void UpdateChaseState()
    {
        destPos = playerTransform.position;
        navMeshAgent.SetDestination(destPos);
        
        float dist = Vector3.Distance(transform.position, playerTransform.position);

        if (dist <= attackDistance)
        {
            Debug.Log("Entering attack state");
            curState = FSMState.ATTACK;
        }
        else if (dist >= chaseDistance)
        {
            Debug.Log("Back to patrol");
            FindNextPoint();
            curState = FSMState.PATROL;
        }
    }

    public void UpdateAttackState()
    {
        destPos = playerTransform.position;
        navMeshAgent.SetDestination(destPos);
        
        float dist = Vector3.Distance(transform.position, playerTransform.position);

        if (dist >= attackDistance)
        {
            curState = FSMState.CHASE;
            return;
        }
        
        ShootBullet();
    }

    private void FindNextPoint()
    {
        //Debug.Log("FindingNextDes");
        int randIndex = UnityEngine.Random.Range(0, patrolPointList.Length);

        destPos = patrolPointList[randIndex].transform.position;
        navMeshAgent.SetDestination(destPos);
    }

    private void ShootBullet()
    {
        if (elapsedTime >= fireRate)
        {
            GameObject bulletObj = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, transform.rotation);
            bulletObj.GetComponent<BulletBehaviour>().Init();
            elapsedTime = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, destPos);
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, arriveDistance);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
