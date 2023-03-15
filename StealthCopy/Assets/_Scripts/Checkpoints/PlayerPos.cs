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

         switch (manager.checkpointNum)
        {
            //for the first checkpoint, give keycard back
            case 1:
            {
                //keep inventory
                //inventory.ResetCards();
                inventory.AddCard(1,1); //for now
                break;
            }
            default:
            //reset inventory
                inventory.ResetCards();
                break;
        } 
    }

}
