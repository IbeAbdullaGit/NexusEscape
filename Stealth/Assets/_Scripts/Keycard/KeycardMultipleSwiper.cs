using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardMultipleSwiper : Interactable
{
         Inventory inventory;
    //answer
    //more answers
    public List<int> needed_id;
    int correct_count = 0;

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
        //make sure we have a key we're holding
        if (inventory.keycard != null)
        {
            //check if held card is needed for any of the answers
            for (int i=0; i< needed_id.Count; i++)
            {
                if (inventory.keycard.id == needed_id[i])
                {
                
                   //for now just acknowledge we got one of them right, perhaps show some visual indication
                   Debug.Log("That one was correct");
                    correct_count +=1;
                     //don't need to keep iterating
                    break;
                }
                else
                {
                    //play error sound
                }
            }
            //now check if we're done
            if (correct_count == needed_id.Count)
            {
                //open door/do something, command
                TriggerDoor();
                _otherdoor.GetComponent<Door>().isOpen = false;
                _otherdoor.GetComponent<Door>().OpenDoor();
            }
        }
    }
}
