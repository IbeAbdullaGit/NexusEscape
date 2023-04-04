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


        //also other management of checkpoints
        //for nexus 2 specifically
         switch (manager.checkpointNum)
        {
            //for the first checkpoint, give keycard back
            case 1:
            {
                //keep inventory
                //inventory.ResetCards();
                //inventory.AddCard(1,1); //for now
                                        //open door
                manager.GetConnectedDoor().ToggleDoor();
                    Debug.Log("Loading in checkpoint 1");
                    break;
            }
            case 3:
            {
                //also open door
                manager.GetConnectedDoor().OpenDoor();
                break;
            }
            case 4:
            {
                //open door that we need opened
                manager.GetConnectedDoor().OpenDoor();
                break;
            }
            default:
            //reset inventory
                inventory.ResetCards();
                break;
        } 
    }

}
