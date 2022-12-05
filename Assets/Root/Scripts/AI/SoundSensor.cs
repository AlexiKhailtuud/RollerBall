using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSensor : Sensor
{
    private void Start()
    {
        sensorType = SensorType.Sound;
        manager.RegisterSensor(this);
    }

    public override void Notify(Trigger trigger)
    {
        Debug.Log("I heard something strange!");
    }
}
