using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
   
    public void ResetPower(Button but)
    {
        but.GetComponent<ButtonMeterScript>().ResetPower();
    }
}
