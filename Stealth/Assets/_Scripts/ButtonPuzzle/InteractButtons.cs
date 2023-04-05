using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Riptide;

public class InteractButtons : Interactable
{
     public bool openMenu = false;
     public Canvas menuUI;

     ButtonPressOrder instance;
    public GameObject _otherdoor;
    DoorInvoker _doorInvoker;

    public Canvas LightUpUI;
    //private float stayLit = 2;
    public WaitForSeconds stayLit = new WaitForSeconds(5f);
    public WaitForSeconds changeColor = new WaitForSeconds(1f);
    public Image[] colors;
    private bool work = true;

    public int id;


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
       
          //server side
          if (InteractionMessages.Singleton!=null)
        {
            //send message
            Message message = Message.Create(MessageSendMode.Reliable, ServerToClientId.buttonMessage);
            //add an id so we know what we're talking about
            message.AddInt(id); //send id so we know which one
            //send message
            NetworkManagerServer.Singleton.Server.SendToAll(message);
        }
       
    }
    public void ManuallyTurnOn()
    {
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

        //Don't forget to change to false
        menuUI.enabled = false;
        LightUpUI.enabled = false;
        
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

        if (work)
        {
            StartCoroutine(ButtonLit());
            work = false;
        }
    }

       
    public void TurnOff()
    {
        menuUI.enabled = false;
        LightUpUI.enabled = false;
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
        LightUpUI.enabled = !LightUpUI.enabled;
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

    private IEnumerator ButtonLit()
    {
            int colorindex;

            for (int j = 0; j < buttonOrder.Count;)
            {
                //Get the value and set it as the index --> change the color to green of that index
                colorindex = buttonOrder[j] - 1;



            colors[colorindex].color = Color.green;
            yield return stayLit;

            colors[colorindex].color = Color.white;
            j++;
                
                //If they user does not get the correct reset the order, and give a longer time so the players realize that this is the start
                if (j == buttonOrder.Count)
                {
                    j = 0;
                    stayLit = new WaitForSeconds(5f);
                }
            else
            {
                stayLit = new WaitForSeconds(2f);
            }

            }
        }
    }

