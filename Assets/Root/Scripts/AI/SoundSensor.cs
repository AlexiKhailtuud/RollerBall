using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSensor : Sensor
{
    public SenseMemory senseMemory;
    
    private void Start()
    {
        sensorType = SensorType.Sound;
        manager.RegisterSensor(this);
        
        senseMemory = GetComponent<SenseMemory>();
    }

    private void Update()
    {
        
    }

    public override void Notify(Trigger trigger)
    {
        Debug.Log("I heard something strange!");
        
        if (senseMemory != null)
        {
            senseMemory.AddToList(trigger.gameObject, 0.3f);
        }
    }
}
