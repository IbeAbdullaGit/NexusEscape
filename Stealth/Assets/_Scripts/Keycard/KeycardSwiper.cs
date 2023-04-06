using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Riptide;
public class KeycardSwiper : Interactable 
{
    //Inventory inventory;
    //answer
    public int needed_id;

    public GameObject _otherdoor;
    DoorInvoker _doorInvoker;

    public bool for_hacker = false;

    bool revealed = false;
   public Canvas pipepuzzle;

    private FMODUnity.EventReference keycardAccept;

    // Start is called before the first frame update
    void Start()
    {

        keycardAccept.Path = "event:/Sound Effects/Interactions/Keypad/KeypadCorrect";
        keycardAccept.Guid = new FMOD.GUID(new System.Guid("{{ab51eecc-eaf6-4e3f-b576-c5e2f97118b2}}"));

        //inventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<Inventory>();
        _doorInvoker = new DoorInvoker();
        //pipepuzzle = GetComponentInChildren<Canvas>();
        pipepuzzle.enabled = false;

    }
    void TriggerDoor()
    {
        ICommand openDoor = new ToggleDoorCommand(_otherdoor.GetComponent<Door>());
        _doorInvoker.AddCommand(openDoor);
        
    }

    public void OnInteractNoCheck()
    {
        //Inventory.instance.DestroyKeycard(); //Delete the Keycard on usage.
        //this is what will happen when we want keycard being inserted to effect something on hacker screen
        {
            if (!revealed)
            {
                revealed = true;
                

            }
            else
            {
                pipepuzzle.enabled = true;
            }

        }
    }

    public override void OnInteract()
    {
        //make sure we have a key we're holding
        if (Inventory.instance.keycardNum != 0)
        {
            if (Inventory.instance.ids[0] == needed_id)
            {
                //Inventory.instance.DestroyKeycard(); //Delete the Keycard on usage.

                FMODUnity.RuntimeManager.PlayOneShotAttached(keycardAccept, gameObject);

                Inventory.instance.RemoveKeyCard(); //keycard is already deleted, so just remove from UI

                //this is what will normally happen
                if (!for_hacker)
                {//open door/do something, command
                //TriggerDoor();
                
                _otherdoor.GetComponent<Door>().ToggleDoor();
                }
                else //this is what will happen when we want keycard being inserted to effect something on hacker screen
                {
                    if (!revealed)
                    {
                        revealed = true;
                        pipepuzzle.enabled = true;

                    }
                    //turn on pipe puzzle
                    //send message!
                    Message message = Message.Create(MessageSendMode.Reliable, ServerToClientId.puzzleInteraction);
                    message.AddInt(1);
                    message.AddString("nexus 2 door 1");
                    NetworkManagerServer.Singleton.Server.SendToAll(message);
                }
            }
            else
            {
                //play error sound
            }
        }
    }
}
