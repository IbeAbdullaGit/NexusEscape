using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButtons : Interactable
{
     public bool openMenu = false;
     public Canvas menuUI;

     ButtonPressOrder instance;
    public GameObject _otherdoor;
    DoorInvoker _doorInvoker;


    //the answer
    [Range(1,8)]
    public List<int> buttonOrder;
     public override void OnFocus()
    {
        Debug.Log("looking at");
         //perhaps highlight it
    }
    public override void OnInteract()
    {
        Debug.Log("Changing UI");
        ChangeUI();
       
       
        //will need to change when having multiple instances (all answers will be set at once instead of individually)
        //set the answer for when this is opened, more elegant solution later
        instance.buttonOrder = buttonOrder;
        instance.text.text = "";
       
       
    }
    public override void OnLoseFocus()
    {
        Debug.Log("looking away");
        
    }
    // Start is called before the first frame update
    void Start()
    {
         //get the manager
        instance = GameObject.FindGameObjectWithTag("GameController").GetComponent<ButtonPressOrder>();
        //set the answer
        //will need to change this eventually as well for multiple instance
        //instance.buttonOrder = this.buttonOrder;
       _doorInvoker = new DoorInvoker();
        menuUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        //correct answer
        if (instance.correct)
        {
            instance.correct = false;
            instance.buttonOrder = null;
            TriggerDoor();
            _otherdoor.GetComponent<Door>().isOpen = false;
            _otherdoor.GetComponent<Door>().OpenDoor();

            //do something, activate object
        }
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
            CrosshairControl.instance.SetNormal();
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
            CrosshairControl.instance.SetNormal();
            
        }
    }

    void TriggerDoor()
    {
        ICommand openDoor = new ToggleDoorCommand(_otherdoor.GetComponent<Door>());
        _doorInvoker.AddCommand(openDoor);
    }

}
