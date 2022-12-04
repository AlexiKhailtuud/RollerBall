using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
   //reference to the TriggerSystemManager
   protected TriggerSystemManager manager;
   //Trigger position
   protected Vector3 position;
   //Trigger radius
   public int radius;
   public bool toBeRemoved;

   private void Awake()
   {
      manager = FindObjectOfType<TriggerSystemManager>();
   }

   public virtual void Try(Sensor sensor)
   {
         
   }

   public virtual void UpdateStatus()
   {
      
   }

   protected virtual bool isTrigger(Sensor sensor)
   {
      return false;
   }
   
   
}
