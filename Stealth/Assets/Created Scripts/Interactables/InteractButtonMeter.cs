using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButtonMeter : Interactable
{
    public bool openMenu = false;

    public Canvas menuUI;

    MeterManager instance;

    [Range(0, 10)]
    public int[] targetValues = new int[4];
    private void Start() {
        //get the manager
        instance = GameObject.FindGameObjectWithTag("GameController").GetComponent<MeterManager>();
        //set the answer
        instance.targetValues = this.targetValues;
        menuUI.enabled = false;
    }
    private void Update() {
        
        //close menu
        if (openMenu)
        {
            if (Input.GetKeyDown(KeyCode.Z)) //could change this key
            {
                ChangeUI();
            }
        }
        
        if (instance.correct)
        {
            //do something, activate object
            
            //make incorrect
            instance.correct = false;
        }
    }
   public override void OnFocus()
    {
        //Debug.Log("looking at");
        
       
        //perhaps highlight it
    }
    public override void OnInteract()
    {
        //Debug.Log("Changing UI");
        ChangeUI();
    }
    public override void OnLoseFocus()
    {
        //Debug.Log("looking away");
        
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
