using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    private CheckpointManager manager;
    private Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("CheckpointManager").GetComponent<CheckpointManager>();
        inventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<Inventory>();
        transform.position = manager.lastCheckPointPos;


       
                {
                    //open door
                    if (manager.GetConnectedDoor() != null)
                        manager.GetConnectedDoor().ToggleDoor();
                  
                }
     
    }

}
