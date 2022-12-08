using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] 
    private Transform targetPosition;
    private NavMeshAgent navMeshAgent;
    
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        navMeshAgent.destination = targetPosition.position;
    }
}
