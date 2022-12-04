using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightTrigger : Trigger
{
    void Start()
    {
        base.Start();
        manager.RegisterTrigger(this);
    }
    
    public override void UpdateMe()
    {
        base.UpdateMe();
        position = transform.position;
    }

    public override void Try(Sensor sensor)
    {
        if (isTouchingTrigger(sensor))
        {
            sensor.Notify(this);
        }
    }
    
    protected override bool isTouchingTrigger(Sensor sensor)
    {
        GameObject tempSensor = sensor.gameObject;
        if (sensor.sensorType == SensorType.Sight)
        {
            var rayDistance = transform.position - tempSensor.transform.position;

            // if (Vector3.Angle(rayDistance, tempSensor.transform.forward) < (sensor as SightSensor).fieldOfView)
            // {
            //     if (Physics.Raycast(tempSensor.transform.position + new Vector3(0, 1, 0), rayDistance,
            //             out RaycastHit hit))
            //     {
            //         if (hit.collider.gameObject == gameObject)
            //         {
            //             return true;
            //         }
            //     }
            // }
        }

        return false;
    }
}
