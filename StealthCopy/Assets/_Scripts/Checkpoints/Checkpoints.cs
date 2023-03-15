using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    private CheckpointManager manager;
    private Inventory inventory;
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
             //check specific conditions
            if (name == "1") //first checkpoint
            {
                //check if the player got the keycard, we want to save that
                if (inventory.keycardNum ==1) //they have at least one keycard
                
                {
                    //now save
                    manager.checkpointNum = 1; 
                    manager.lastCheckPointPos = transform.position;
                }

            }
            else 
            {
                manager.lastCheckPointPos = transform.position;
                
            }
            
            
           
        }
    }
    private void Start() {
        manager = GameObject.FindGameObjectWithTag("CheckpointManager").GetComponent<CheckpointManager>();
        inventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<Inventory>();
    }
}
