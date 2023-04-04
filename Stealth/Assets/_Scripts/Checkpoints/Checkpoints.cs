using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    private CheckpointManager manager;
    private Inventory inventory;

    public Door connectedDoor;
    //bool triggered = false;
    private void OnTriggerEnter(Collider other) {
        //hard coded for nexus2
        
        if (other.tag == "Player" )
        {
           // triggered = true;
            //manager.lastCheckPointPos = transform.position;
            
             //check specific conditions
            if (name == "1") //first checkpoint for n2
            {
                //check if the player got the keycard, we want to save that
                if (inventory.keycardNum ==1) //they have at least one keycard
                
                {
                    //now save
                    manager.checkpointNum = 1;
                    manager.SetConnectedDoor(connectedDoor);
                    manager.lastCheckPointPos = transform.position;


                }

            }
            else
            {
                if (connectedDoor != null)
                    manager.SetConnectedDoor(connectedDoor);
                manager.lastCheckPointPos = transform.position;
            }
        }
    }
    private void Start() {
        manager = GameObject.FindGameObjectWithTag("CheckpointManager").GetComponent<CheckpointManager>();
        inventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<Inventory>();
    }
}
