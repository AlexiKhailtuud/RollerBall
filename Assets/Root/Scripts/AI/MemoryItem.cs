using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class MemoryItem : MonoBehaviour
{
    public GameObject go;
    public float lastMemoryTime;
    public float memoryTimeLeft;
    public float sensorType;

    public MemoryItem(GameObject objectToAdd, float time, float timeLeft, float type)
    {
        go = objectToAdd;
        memoryTimeLeft = timeLeft;
        sensorType = type;
    }
}
