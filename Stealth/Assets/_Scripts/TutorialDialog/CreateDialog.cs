using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateDialog : MonoBehaviour
{
    public Canvas ui;
    public TMP_Text text;
    public TMP_Text text2;

    public void ChangeText(string s)
    {
        text.text = s;
        //ChangeUI();
    }
    public void ChangeTextPlayer2(string s)
    {
        text2.text = s;
    }

    // Start is called before the first frame update
    void Start()
    {
        //ui.enabled = false;
        //ChangeUI();
        ChangeText("Use WASD to move around. Use [Space] to jump, [Left Ctrl] to crouch");
        //manually change player2 text for this case
        ChangeTextPlayer2("Move with arrow keys. Press [Shift] to view the cameras, and use the arrows to toggle between them");
        Cursor.lockState = CursorLockMode.Locked;
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

    
}
