using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLimitedLife : Trigger
{
   protected int lifetime;

   private void Start()
   {
      
   }

   public override void UpdateMe()
   {
      if (--lifetime <= 0)
      {
         toBeRemoved = true;
      }

      base.UpdateMe();
   }
}
