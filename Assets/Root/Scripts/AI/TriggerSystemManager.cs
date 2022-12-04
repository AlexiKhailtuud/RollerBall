using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSystemManager : MonoBehaviour
{
    private List<Sensor> currentSensors = new List<Sensor>();
    private List<Trigger> currentTriggers = new List<Trigger>();

    private List<Sensor> sensorToRemove;
    private List<Trigger> triggerToRemove;

    private void Start()
    {
        sensorToRemove = new List<Sensor>();
        triggerToRemove = new List<Trigger>();
    }

    private void Update()
    {
        UpdateTriggers();
        TryTriggers();
    }

    void UpdateTriggers()
    {
        foreach (var trigger in currentTriggers)
        {
            if (trigger.toBeRemoved)
            {
                triggerToRemove.Add(trigger);
            }
            else
            {
                trigger.UpdateStatus();
            }
        }

        foreach (var trigger in triggerToRemove)
        {
            currentTriggers.Remove(trigger);
        }
    }

    void TryTriggers()
    {
        foreach (var sensor in currentSensors)
        {
            if (sensor != null)
            {
                foreach (var trigger in currentTriggers)
                {
                    trigger.Try(sensor);
                }
            }
            else
            {
                sensorToRemove.Add(sensor);
            }
        }
    }

    public void RegisterTrigger(Trigger trigger)
    {
        currentTriggers.Add(trigger);
    }

    public void RegisterSensor(Sensor sensor)
    {
        currentSensors.Add(sensor);
    }
}
