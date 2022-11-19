using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSubmit : Interactable
{
     Inventory inventory;

     //for where we will put the tiles
     public Transform[] positions;
     int counter =0;
     //hold our references
     List<TilePickUp> currRefs = new List<TilePickUp>();

     //hold reference to whatever is occupied
     bool[] occupied = {false, false, false};
    public override void OnInteract()
    {
        Debug.Log("We are interacting");
        //make sure we have a tile we're holding
        if (inventory.currentTile != null)
        {
            //helps us exit early
            bool early_exit = false;
            //make sure we have room for this
            for (int i=0; i< occupied.Length; i++)
            {
                if (!occupied[i] && !early_exit) //prioritize left to right
                {
                    //Debug.Log("We inside the statement");
                    //settings for being in place with the submission
                    currRefs.Add(inventory.currentTile);
                    //Debug.Log(currRefs.Count);
                    currRefs[currRefs.Count-1].SubmitTile();
                    //currRef.SubmitTile();
                    //change position, based on what is available rn
                    currRefs[currRefs.Count-1].transform.position = positions[i].position;
                    //now set this position to be occupied
                    occupied[i] = true;
                    //save what this position is for this tile
                    currRefs[currRefs.Count-1].tile_pos = i;
                    counter +=1;
                    //leave now, don't need to add more
                    early_exit = true;
                }
            }
            
        }

    }
    
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<Inventory>();
        //Debug.Log("Is this being started?");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(occupied[0] + ", " + occupied[1] + ", " + occupied[2]);
        //check if our references are in place; should have at least 1, and is anything being held
        if (currRefs.Count >=1 && inventory.currentTile)
        {
            for (int i=0; i < currRefs.Count; i++)
            {
                //check if each one is in place - if its been moved, it will be false   
                if (!currRefs[i].inPlace)
                {
                    //free up the spot, GOES BEFORE DUE TO LOGIC
                    occupied[currRefs[i].tile_pos] = false;
                    //remove from this spot
                    currRefs.RemoveAt(i);
                    //decrease counter
                    counter -=1;
                }
            }
        }
        
    }
}
