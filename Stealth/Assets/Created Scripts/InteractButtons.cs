using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButtons : Interactable
{
     public bool openMenu = false;
     public Canvas menuUI;
     public override void OnFocus()
    {
        Debug.Log("looking at");
         //perhaps highlight it
    }
    public override void OnInteract()
    {
        Debug.Log("Changing UI");
        ChangeUI();
       
    }
    public override void OnLoseFocus()
    {
        Debug.Log("looking away");
        
    }
    // Start is called before the first frame update
    void Start()
    {
         menuUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (openMenu)
        {
            if (Input.GetKeyDown(KeyCode.Z)) //could change this key
            {
                ChangeUI();
            }
        }
    }
    public void ChangeUI()
    {
        menuUI.enabled = !menuUI.enabled;
        openMenu = !openMenu;
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
