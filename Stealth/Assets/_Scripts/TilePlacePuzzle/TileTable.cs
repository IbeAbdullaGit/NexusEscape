using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTable : Interactable
{
    Inventory inventory;
    public override void OnInteract()
    {
        //make sure we have a tile we're holding
        if (inventory.currentTile != null)
        {
            //place tile back
            inventory.currentTile.PutDown();
            
        }

    }
    // Start is called before the first frame update
    void Start()
    {
         inventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
