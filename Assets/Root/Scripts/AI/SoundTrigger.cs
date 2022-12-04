using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : TriggerLimitedLife
{
    private void Start()
    {
        lifetime = 3;
        base.Start();
        
        manager.RegisterTrigger(this);
    }
    
    public override void Try(Sensor sensor)
    {
        base.Try(sensor);

        if (isTouchingTrigger(sensor))
        {
            sensor.Notify(this);
        }
    }

    protected override bool isTouchingTrigger(Sensor sensor)
    {
        GameObject g = sensor.gameObject;

        if (sensor.sensorType == SensorType.Sound)
        {
            if (Vector3.Distance(transform.position, g.transform.position) < radius)
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
