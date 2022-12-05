using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenseMemory : MonoBehaviour
{
    private bool alreadyInList = false;
    public float memoryTime = 4.0f;
    public List<MemoryItem> memoryList = new List<MemoryItem>();
    private List<MemoryItem> removeList = new List<MemoryItem>();

    public bool FindInList()
    {
        foreach (MemoryItem memo in memoryList)
        {
            if (memo.g.tag == "Player")
                return true;
        }

        return false;
    }

    public void AddToList(GameObject go, float type)
    {
        alreadyInList = false;

        foreach (MemoryItem memo in memoryList)
        {
            if (memo.g == go)
            {
                alreadyInList = true;
                memo.lastMemoryTime = Time.time;
            }
        }
    }
}
