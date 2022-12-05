using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLimitedLife : Trigger
{
   protected float lifetime;

   private void Start()
   {
      base.Start();
   }

   public override void UpdateMe()
   {
      lifetime -= Time.deltaTime;
      Mathf.Clamp(lifetime, 0, lifetime);
      
      if (lifetime <= 0)
      {
         toBeRemoved = true;
      }

      base.UpdateMe();
   }
}
