using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityTrigger : MonoBehaviour
{
   private void OnTriggerExit(Collider other) 
   {
        //if player leaves the trigger, want to make sure that keypad isnt showing
        if (other.tag == "Player")
        {
            //turn off/switch keypad
            GetComponentInParent<InteractKeypad>().TurnOff();
        }
   }
}
