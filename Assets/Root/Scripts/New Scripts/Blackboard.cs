using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Blackboard : MonoBehaviour
{
    //last player position detected  
    public Vector3 playerLastSeenPosition;
    
    //default position if player position not detected
    public Vector3 resetPosition;
    
    //last time player detected
    public float lastSenseTime = 0;
    
    //Time interval before resetting player position
    public float resetTime = 1.0f;

    private void Start()
    {
        playerLastSeenPosition = new Vector3(100, 100, 100);
        resetPosition = new Vector3(100, 100, 100);
    }

    private void Update()
    {
        if (Time.time - lastSenseTime > resetTime)
        {
            playerLastSeenPosition = resetPosition;
        }
    }
}
