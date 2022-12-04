using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    //reference to the TriggerSystemManager
    protected TriggerSystemManager manager;
    
    public enum SensorType
    {
        Sight,
        Sound
    }

    public SensorType sensorType;
    public string sensorName;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public virtual void Notify(Trigger trigger)
    {
        
    }
}
