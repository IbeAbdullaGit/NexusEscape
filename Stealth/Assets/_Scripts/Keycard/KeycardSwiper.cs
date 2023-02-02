using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardSwiper : Interactable
{
     Inventory inventory;
    //answer
    public int needed_id;

    public GameObject _otherdoor;
    DoorInvoker _doorInvoker;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<Inventory>();
        _doorInvoker = new DoorInvoker();
    }
    void TriggerDoor()
    {
        ICommand openDoor = new ToggleDoorCommand(_otherdoor.GetComponent<Door>());
        _doorInvoker.AddCommand(openDoor);
    }

    public override void OnInteract()
    {
        //make sure we have a tile we're holding
        if (inventory.keycard != null)
        {
            if (inventory.keycard.id == needed_id)
            {
                //open door/do something, command
                TriggerDoor();
                _otherdoor.GetComponent<Door>().isOpen = false;
                _otherdoor.GetComponent<Door>().OpenDoor();
            }
            else
            {
                //play error sound
            }
        }
    }
}
