using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButtonMeter : Interactable
{
    public bool openMenu = false;

    public Canvas menuUI;

    MeterManager instance;

    //[Range(0, 10)]
    //public int[] targetValues = new int[4];

    //this buttons value for the answer
    [Range(0,10)]
    public int ownValue;

    //which button is this
    public int number;

    Animator anim;

    SoundManager soundInstance;

    private void Start() {
        //get the manager
        instance = GameObject.FindGameObjectWithTag("GameController").GetComponent<MeterManager>();
        //set the answer
        //instance.targetValues = this.targetValues;
        instance.targetValues[number-1] = this.ownValue;
        menuUI.enabled = false;
         anim = GetComponent<Animator>();
         soundInstance = GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundManager>().instance;
    }
    public void TurnOff()
    {
        menuUI.enabled = false;
        openMenu = false;
        //turn off cursor
        if (Cursor.lockState == CursorLockMode.None)
        {
             Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false; 
        } 
    }
    private void Update() {
        
        //close menu
        if (openMenu)
        {
            if (Input.GetKeyDown(KeyCode.Z)) //could change this key
            {
                //will always be close
                ChangeUI();   
            }
        }
        
        //checking for right answer, this will check ALL BUTTONS for an OVERALL answer
        if (instance.correct[number-1])
        {
            //do something, activate object, reset
            
            //make incorrect
            //instance.correct[number-1] = false;
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
        anim.Play("Armature|Press");
        anim.SetTrigger("press");
        //play sound
        soundInstance.PlaySound(SoundManager.Sound.ButtonPress);
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
