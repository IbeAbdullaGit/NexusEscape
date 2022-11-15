using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateDialog : MonoBehaviour
{
    public Canvas ui;
    public TMP_Text text;

    public bool run_once = false;
    public void ChangeText(string s)
    {
        text.text = s;
    }
    // Start is called before the first frame update
    void Start()
    {
        ui.enabled = false;
        //ChangeUI();
        ChangeText("This is a test");
    }
    
    public void ChangeUI()
    {
        ui.enabled = !ui.enabled;
       
        if (Cursor.lockState == CursorLockMode.Locked)
        {
             Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; 
        }  
        else
        {
             Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!run_once)
        {
            //used to enable ui at the start right away
            ChangeUI();
            run_once = true;
        }
    }
}
