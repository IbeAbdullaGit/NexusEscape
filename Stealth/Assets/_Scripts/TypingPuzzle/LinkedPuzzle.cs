using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LinkedPuzzle : MonoBehaviour
{
    public TMP_Text revealed_answer;

    DoorInvoker _doorInvoker;
    public GameObject _otherdoor;

    public bool[] amount_linked;
    
    // Start is called before the first frame update
    void Start()
    {
        _doorInvoker = new DoorInvoker();
        for (int i=0; i< amount_linked.Length; i++)
        {
            amount_linked[i] = false;
        }
        //basically the first linked puzzle/thing is true
        //amount_linked[0] = true;

        //hide answer to start
        revealed_answer.enabled = false;
    }

    public void ZeroEverything()
    {
        for (int i=0; i< amount_linked.Length; i++)
        {
            amount_linked[i] = false;
        }
    }

    public void ActivateText()
    {
        revealed_answer.enabled = true;
    }

    public void ActivateDoor()
    {
        TriggerDoor();
        _otherdoor.GetComponent<Door>().isOpen = false;
        _otherdoor.GetComponent<Door>().OpenDoor();
    }
    void TriggerDoor()
    {
        ICommand openDoor = new ToggleDoorCommand(_otherdoor.GetComponent<Door>());
        _doorInvoker.AddCommand(openDoor);
    }
}
