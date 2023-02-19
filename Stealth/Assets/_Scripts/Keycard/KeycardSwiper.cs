using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    // Start is called before the first frame update
    void Start()
    {
        //inventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<Inventory>();
        _doorInvoker = new DoorInvoker();
        pipepuzzle = GetComponentInChildren<Canvas>();
        pipepuzzle.enabled = false;

    }
    void TriggerDoor()
    {
        ICommand openDoor = new ToggleDoorCommand(_otherdoor.GetComponent<Door>());
        _doorInvoker.AddCommand(openDoor);
    }

    public override void OnInteract()
    {
        //make sure we have a key we're holding
        if (Inventory.instance.keycardNum != 0)
        {
            if (Inventory.instance.ids[0] == needed_id)
            {
                //Inventory.instance.DestroyKeycard(); //Delete the Keycard on usage.

                Inventory.instance.RemoveKeyCard(); //keycard is already deleted, so just remove from UI

                //this is what will normally happen
                if (!for_hacker)
                {//open door/do something, command
                TriggerDoor();
                _otherdoor.GetComponent<Door>().isOpen = false;
                _otherdoor.GetComponent<Door>().OpenDoor();
                }
                else //this is what will happen when we want keycard being inserted to effect something on hacker screen
                {
                    if (!revealed)
                    {
                        revealed = true;
                        pipepuzzle.enabled = true;

                    }
                    //turn on pipe puzzle

                }
            }
            else
            {
                //play error sound
            }
        }
    }
}
