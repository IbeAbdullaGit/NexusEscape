using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.Characters.FirstPerson;

public class InteractKeypad : Interactable
{
    public bool openMenu = false;

    public Canvas menuUI;

    //keypad reference
    public GameObject keypad;
    Keypad instance;

    public string answer;
    GameObject triggeredObject;
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
        instance = keypad.GetComponent<Keypad>();
        //change answer depending on need
        instance.answer = answer;
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
        //did we enter the right answer?
        if (instance.correct)
        {
            //reset the keypad
            instance.correct = false;
            instance.answer = null;

            //triggeredObject.open();?
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