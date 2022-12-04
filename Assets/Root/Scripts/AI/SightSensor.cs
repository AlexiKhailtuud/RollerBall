using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightSensor : Sensor
{
    public float fieldOfView = 45;
    public float viewDistance = 100f;

    private AIController controller;

    private void Start()
    {
        controller = GetComponent<AIController>();
        sensorType = SensorType.Sight;
        manager.RegisterSensor(this);
    }

    private void Update()
    {
        
    }

    public override void Notify(Trigger trigger)
    {
        Debug.Log($"I see a {trigger.gameObject.name}");
        Debug.DrawLine(transform.position, trigger.transform.position, Color.magenta);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Vector3 frontRayPoint = transform.position + (transform.forward * viewDistance);
        float fieldOfViewInRadius = fieldOfView * 3.14f / 180f;
        Vector3 leftRayPoint = transform.TransformPoint(new Vector3(viewDistance * Mathf.Sin(fieldOfViewInRadius), 0,
            viewDistance * Mathf.Cos(fieldOfViewInRadius)));
        Vector3 rightRayPoint = transform.TransformPoint(new Vector3(-viewDistance * Mathf.Sin(fieldOfViewInRadius), 0,
            viewDistance * Mathf.Cos(fieldOfViewInRadius)));
        
        Debug.DrawLine(transform.position + new Vector3(0, 1, 0), frontRayPoint + new Vector3(0, 1, 0), Color.green);
        Debug.DrawLine(transform.position + new Vector3(0,1, 0), leftRayPoint + new Vector3(0, 1, 0), Color.green);
        Debug.DrawLine(transform.position + new Vector3(0, 1, 0), rightRayPoint + new Vector3(0, 1, 0) , Color.green);
    }
}
