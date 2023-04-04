using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderChange : MonoBehaviour
{
    public int objectNumber;

    MeterManager instance;

    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
         //get the manager
        instance = GameObject.FindGameObjectWithTag("GameController").GetComponent<MeterManager>();

        //subtract one from number, because of array indexing
        objectNumber -=1;

        //set current value
        slider = GetComponent<Slider>();

        //Debug.Log("SLider: " + objectNumber);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(slider.value);
    }
    public void ChangeColor()
    {
        //check if the current value is the desired value for this object
        if (slider.value == instance.targetValues[objectNumber])
        {
            slider.fillRect.GetComponent<Image>().color = Color.green;
            //send message here
            if (InteractionMessages.Singleton != null)
                InteractionMessages.Singleton.ButtonMeterInteract(objectNumber, (int)slider.value);
            if (InteractionServerNexus1.Singleton != null)
                InteractionServerNexus1.Singleton.ButtonMeterInteract(objectNumber, (int)slider.value);
        }
        else //if we aren't matching the value
        {
            slider.fillRect.GetComponent<Image>().color = Color.red;
            //send message here
            //send message here
            if (InteractionMessages.Singleton != null)
                InteractionMessages.Singleton.ButtonMeterInteract(objectNumber, (int)slider.value);
            if (InteractionServerNexus1.Singleton != null)
                InteractionServerNexus1.Singleton.ButtonMeterInteract(objectNumber, (int)slider.value);
        }
    }
}
