using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    //reference to the TriggerSystemManager
    protected TriggerSystemManager manager;

    public SensorType sensorType;
    public string sensorName;
    
    private void Awake()
    {
        manager = FindObjectOfType<TriggerSystemManager>();
    }
    
    public virtual void Notify(Trigger trigger)
    {
        
    }
}
