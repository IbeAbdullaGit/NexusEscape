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
            //turn off/switch keypad, or other interactables
            if (GetComponentInParent<InteractKeypad>() != null) //keypad
                GetComponentInParent<InteractKeypad>().TurnOff();
            if (GetComponentInParent<InteractButtonMeter>() !=null) //buttons
                GetComponentInParent<InteractButtonMeter>().TurnOff();
            if (GetComponentInParent<PipeManager>() !=null) //pipe
                GetComponentInParent<PipeManager>().TurnOff();

            if (GetComponentInParent<InteractButtons>() != null)
                GetComponentInParent<InteractButtons>().TurnOff();
        }
   }

   private void OnTriggerEnter(Collider other) {
    
    if (other.tag == "Player")
    {
        if (GetComponentInParent<PipeManager>() !=null) //pipe
                GetComponentInParent<PipeManager>().TurnOn();
    }
   }
   
}
