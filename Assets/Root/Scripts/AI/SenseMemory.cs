using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenseMemory : MonoBehaviour
{
    private bool alreadyInList = false;
    public float memoryTime = 4.0f;
    
    public List<MemoryItem> memoryList = new List<MemoryItem>();
    private List<MemoryItem> removeList = new List<MemoryItem>();

    private void Update()
    {
        removeList.Clear();

        foreach (MemoryItem memo in memoryList)
        {
            memo.memoryTimeLeft -= Time.deltaTime;
            
            if (memo.memoryTimeLeft < 0)
            {
                removeList.Add(memo);
            }
            else
            {
                if (memo.go != null)
                {
                    Debug.DrawLine(transform.position, memo.go.transform.position, Color.blue);
                }
            }
        }
        
        foreach (MemoryItem memo in removeList)
        {
            memoryList.Remove(memo);
        }
    }

    public bool FindInList()
    {
        foreach (MemoryItem memo in memoryList)
        {
            if (memo.go.tag == "Player")
                return true;
        }

        return false;
    }

    public void AddToList(GameObject go, float type)
    {
        alreadyInList = false;
        
        foreach (MemoryItem memo in memoryList)
        {
            if (memo.go == go)
            {
                alreadyInList = true;
                memo.lastMemoryTime = Time.time;
                if (type > memo.sensorType)
                {
                    memo.sensorType = type;
                }
                //what if I put "break" here? 
            }
            break;
        }

        if (!alreadyInList)
        {
            MemoryItem newItem = new MemoryItem(go, Time.time, memoryTime, type);
            memoryList.Add(newItem);
        }
    }
}
