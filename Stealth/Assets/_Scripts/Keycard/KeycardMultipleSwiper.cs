using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardMultipleSwiper : Interactable
{
    //answer
    //more answers
    public List<int> needed_id;
    int correct_count = 0;

    public GameObject _otherdoor;
    DoorInvoker _doorInvoker;

    private FMODUnity.EventReference keycardAccept;

    // Start is called before the first frame update
    void Start()
    {
        keycardAccept = FMODUnity.RuntimeManager.PathToEventReference("event:/Sound Effects/Interactions/Keypad/KeypadCorrect");
       
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
        if (Inventory.instance.keycardNum  >= 2)
        {
            
                FMODUnity.RuntimeManager.PlayOneShotAttached(keycardAccept, gameObject);
                   //for now just acknowledge we got one of them right, perhaps show some visual indication
                   Debug.Log("That one was correct");
                   Inventory.instance.RemoveKeyCard(); //keycard is already deleted, so just remove from UI
                    Inventory.instance.RemoveKeyCard();
                
                 //open door/do something, command
                //TriggerDoor();
                //_otherdoor.GetComponent<Door>().isOpen = false;
                _otherdoor.GetComponent<Door>().ToggleDoor();
        }
    }
}
