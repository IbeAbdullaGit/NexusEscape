using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneCheckpoints : MonoBehaviour
{
    //private CheckpointManager manager;
    private Inventory inventory;

    public Door connectedDoor;
    //bool triggered = false;
    private void OnTriggerEnter(Collider other) {
        
        if (other.tag == "Player" )
        {
           // triggered = true;
            //manager.lastCheckPointPos = transform.position;

                if (connectedDoor != null)
                    CheckpointManager.instance.SetConnectedDoor(connectedDoor);
            CheckpointManager.instance.lastCheckPointPos = transform.position;
        }
    }
    private void Start() {
        //manager = GameObject.FindGameObjectWithTag("CheckpointManager").GetComponent<CheckpointManager>();
        inventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<Inventory>();
    }
}
