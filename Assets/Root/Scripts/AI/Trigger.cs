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
   public bool toBeRemoved;
   private void Awake()
   {
      manager = FindObjectOfType<TriggerSystemManager>();
   }

   protected void Start()
   {
      
   }

   public virtual void Try(Sensor sensor)
   {
         
   }

   public virtual void UpdateMe()
   {
      
   }

   protected virtual bool isTouchingTrigger(Sensor sensor)
   {
      return false;
   }
}
