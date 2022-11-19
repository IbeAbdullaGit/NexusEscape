using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePickUp : Interactable
{
   GameObject player;

   Inventory inventory;

   Vector3 offset = new Vector3(1, 0, 1);

   Vector3 originalPlace;

   public bool inPlace = false;

   public int tile_pos = 0;

   public override void OnInteract()
    {
       //only add if we don't already have a tile we're holding
       if (inventory.currentTile == null)
       {
         
         //add to inventory
         inventory.AddTile(this);
         //add to player "hand"
         this.transform.parent = player.transform;
         //move it to the player
         this.transform.position = player.transform.position + offset;
         //this.transform.position = player.transform.position;
         //give offset, increase float value for further distance
         //this.transform.position += player.transform.forward * 2.0f;
         //it is not in place anywhere
         inPlace = false;
       }

    }
    public void SubmitTile()
    {
      //remove tile
      inventory.RemoveTile();
      //delete this (or object pooling)?
      //gameObject.SetActive(false);
      //remove parent so we can move it
      this.transform.parent = null;
      //it is in place now
      inPlace =true;
    }
    public void PutDown()
    {
      //remove tile
      inventory.RemoveTile();
      //remove parent so we can move it
      this.transform.parent = null;
      //set position to the original one
      this.transform.position = originalPlace;
    }
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<Inventory>();
        player = GameObject.FindGameObjectWithTag("Player");
        originalPlace = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
