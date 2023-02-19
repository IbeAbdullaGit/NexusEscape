using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairControl : MonoBehaviour
{
    Image crosshair;

    public Sprite hightlightCursor;
    Sprite normalCursor;

    public static CrosshairControl instance;
    
    // Start is called before the first frame update
    void Start()
    {
        crosshair = GetComponent<Image>();
        crosshair.enabled = true;

        normalCursor = GetComponent<Image>().sprite;

        //not really ever going to be multiple
        instance = this;
    }

    public void SetNormal()
    {
        crosshair.sprite = normalCursor;
    }
    public void SetHighlighted()
    {
        crosshair.sprite = hightlightCursor;
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
