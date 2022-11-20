using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorHacker : Interactable
{
    public Door connectedDoor;

    public override void OnInteract()
    {
       //open the connected door
       //need the current door to not already be open
       if (!connectedDoor.isOpen)
       {
            connectedDoor.OpenDoor();
            Debug.Log("Opened door!");
       }
       else
        connectedDoor.CloseDoor();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
