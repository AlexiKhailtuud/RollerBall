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
    
    public virtual void Notify(Trigger trigger)
    {
        
    }
}
