using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    private CheckpointManager manager;
    private Inventory inventory;

    public Door connectedDoor;
    bool triggered = false;
    private void OnTriggerEnter(Collider other) {
        //hard coded for nexus2
        
        if (other.tag == "Player" && !triggered)
        {
            triggered = true;
            manager.lastCheckPointPos = transform.position;
            
             //check specific conditions
            if (name == "1") //first checkpoint
            {
                //check if the player got the keycard, we want to save that
                if (inventory.keycardNum ==1) //they have at least one keycard
                
                {
                    //now save
                    manager.checkpointNum = 1; 
                    
                }

            }
            else if (name =="2") //no other extra stuff here
            {
                manager.checkpointNum = 2;
                
            }
            else if (name == "3")
            {
                manager.checkpointNum = 3;
                //has door
                manager.SetConnectedDoor(connectedDoor);
               
            }
            else if (name == "4")
            {
                manager.checkpointNum = 4;
                //has door
                manager.SetConnectedDoor(connectedDoor);

            }    
        }
    }
    private void Start() {
        manager = GameObject.FindGameObjectWithTag("CheckpointManager").GetComponent<CheckpointManager>();
        inventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<Inventory>();
    }
}
