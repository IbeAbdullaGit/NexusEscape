using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMeterScript : MonoBehaviour
{
    public float power;

    float maxPower = 10;

    public float chargeSpeed = 3;

    bool buttonHeldDown;

    public Slider meter;
    //easy way to have id, may change
    public int id;
    
   

    // Update is called once per frame
    void Update()
    {
        if (buttonHeldDown && power <= maxPower)
        {
            power +=Time.deltaTime * chargeSpeed;
            
            if (power >=maxPower)
            {
                power = maxPower;
            }
        }
        
        
    }
    public void HoldButton()
    {
        buttonHeldDown = true;
        //use an int to make calculating target ranges easier
        meter.value = (int)power;
        //send message
        //if (InteractionMessages.Singleton != null)
        //    InteractionMessages.Singleton.ButtonMeterInteract(id, (int)power);
        //if (InteractionServerNexus1.Singleton !=null)
        //    InteractionServerNexus1.Singleton.ButtonMeterInteract(id, (int)power);
        
    }
    public void ResetPower()
    {
        power = 0;
        //use an int to make calculating target ranges easier
        meter.value = (int)power;
        //send message
        //if (InteractionMessages.Singleton != null)
        //    InteractionMessages.Singleton.ButtonMeterInteract(id, (int)power);
        //if (InteractionServerNexus1.Singleton !=null)
        //    InteractionServerNexus1.Singleton.ButtonMeterInteract(id, (int)power);
    }
    public void ReleaseButton()
    {
        buttonHeldDown = false;
        //use an int to make calculating target ranges easier
        meter.value = (int)power;
        //power = 0;
        //ad some way to reset

        //send message
        //if (InteractionMessages.Singleton != null)
        //    InteractionMessages.Singleton.ButtonMeterInteract(id, (int)power);
        //if (InteractionServerNexus1.Singleton !=null)
        //    InteractionServerNexus1.Singleton.ButtonMeterInteract(id, (int)power);
       
    }
}
