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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
        
        //use an int to make calculating target ranges easier
        //disable here for now for client side
        //meter.value = (int)power;
    }
    public void HoldButton()
    {
        buttonHeldDown = true;
        meter.value = (int)power;
    }
    public void ReleaseButton()
    {
        buttonHeldDown = false;
        meter.value = (int)power;
        //power = 0;
        //ad some way to reset
    }
}
