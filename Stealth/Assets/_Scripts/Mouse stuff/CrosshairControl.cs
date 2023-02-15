using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairControl : MonoBehaviour
{
    Image crosshair;
    
    // Start is called before the first frame update
    void Start()
    {
        crosshair = GetComponent<Image>();
        crosshair.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Cursor.lockState == CursorLockMode.None) //shouldnt be visisble
        {
            crosshair.enabled = false;
        }
        else if (Cursor.lockState == CursorLockMode.Locked) //we need the crosshair here
        {
            crosshair.enabled = true;
        }
    }
}
