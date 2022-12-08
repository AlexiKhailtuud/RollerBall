using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MemoryItem
{
    public GameObject go;
    public float lastMemoryTime;
    public float memoryTimeLeft;
    public float sensorType;

    public MemoryItem(GameObject objectToAdd, float time, float timeLeft, float type)
    {
        go = objectToAdd;
        lastMemoryTime = time;
        memoryTimeLeft = timeLeft;
        sensorType = type;
    }
}
