using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorHacker : Interactable
{
    public Door connectedDoor;
     Animator anim;

SoundManager soundInstance;

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
       //play sound
        soundInstance.PlaySound(SoundManager.Sound.InteractPress);
    }
    
    // Start is called before the first frame update
    void Start()
    {
         anim = GetComponent<Animator>();
         soundInstance = GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundManager>().instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
