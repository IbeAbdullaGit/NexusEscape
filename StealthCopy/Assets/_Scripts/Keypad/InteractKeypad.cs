using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.Characters.FirstPerson;

public class InteractKeypad : Interactable
{
    public bool openMenu = false;

    public Canvas menuUI;

    //keypad reference
    Keypad instance;

    public string answer;
    GameObject triggeredObject;

    public GameObject _otherdoor;
    DoorInvoker _doorInvoker;

    public bool correct=false;
     SoundManager soundInstance;

    public override void OnFocus()
    {
        //Debug.Log("looking at");
        
       
        //perhaps highlight it
    }
    public override void OnInteract()
    {
        //Debug.Log("Changing UI");
        //reset whats shown
         instance.Ans.text = null;
        ChangeUI();
        //will need to change when having multiple instances (all answers will be set at once instead of individually)
        //set the answer for when this is opened, more elegant solution later
        instance.answer = answer;
        //let manager know this is the current instance
        instance.currentInstance = this;
        //play sound
        soundInstance.PlaySound(SoundManager.Sound.InteractPress);
        
    }
    public override void OnLoseFocus()
    {
        //Debug.Log("looking away");
        //disable ui so it doesnt just stay there
        
       
        
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

    // Start is called before the first frame update
    void Start()
    {
        instance = GameObject.FindGameObjectWithTag("GameController").GetComponent<Keypad>();
        //change answer depending on need
        
        menuUI.enabled = false;
        _doorInvoker = new DoorInvoker();
         soundInstance = GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundManager>().instance;
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
        if (correct)
        {
            //reset the keypad
            correct = false;
            instance.answer = null;
            print("door toggled");
            //triggeredObject.open();?
            TriggerDoor();
            _otherdoor.GetComponent<Door>().isOpen = false;
            _otherdoor.GetComponent<Door>().OpenDoor();
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

    void TriggerDoor()
    {
        ICommand openDoor = new ToggleDoorCommand(_otherdoor.GetComponent<Door>());
        _doorInvoker.AddCommand(openDoor);
    }
    //call when player exits trigger around keypad, implement later
    private void OnTriggerExit(Collider other) {
        
    }
}
