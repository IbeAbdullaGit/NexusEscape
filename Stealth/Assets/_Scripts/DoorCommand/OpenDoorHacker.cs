using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorHacker : Interactable
{
    public Door connectedDoor;
     Animator anim;


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

        if (anim != null)
         //play animation
       { 
        anim.Play("Armature|Press");
        anim.SetTrigger("press");
       }
    }
    
    // Start is called before the first frame update
    void Start()
    {
         anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
