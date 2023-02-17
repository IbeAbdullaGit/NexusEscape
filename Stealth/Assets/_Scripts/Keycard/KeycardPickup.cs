using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardPickup : Interactable
{
    GameObject player;

   Vector3 offset = new Vector3(1, 0, 1);

   public int id;

    public override void OnInteract()
    {
         //only add if we don't already have a keycard we're holding, and not holding anything else
       if (Inventory.instance.keycard == null && Inventory.instance.currentTile == null)
       {
            //add to inventory
            Inventory.instance.AddCard(this);
         //add to player "hand"
         this.transform.parent = player.transform;
         //move it to the player
         //this.transform.position = player.transform.position + offset;
         this.transform.position = player.transform.position;
         //give offset, increase float value for further distance
         this.transform.position += player.transform.forward * 2.0f;
       }
    }

    //put down the keycard
     public void PutDown()
    {
        //remove tile
        Inventory.instance.RemoveKeyCard(); //Using this function to remove/delete keycards (If you use "this" in PutDown() to delete, there's a bug where it has problems detecting)
      //remove parent so we can move it
      //this.transform.parent = null;
      //set position to the original one
      //this.transform.position = position;

      //maybe rely on gravity here for it to drop? or maybe throw
      Vector3 direction = Camera.main.transform.forward;
      
      //GetComponent<Rigidbody>().isKinematic = false;
      //Debug.Log("We are setting");
      GetComponent<Rigidbody>().AddForce(direction *100);
      //Debug.Log(GetComponent<Rigidbody>().isKinematic);
      
      
    }
    // Start is called before the first frame update
    void Start()
    {
        //inventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<Inventory>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //want to make sure we're holding something before we put it down
        /* //Drop keycard with Right Click
        if (Input.GetKeyDown(KeyCode.Mouse1) && inventory.keycard != null) //right click
        {
            GetComponent<Rigidbody>().isKinematic = false;
            PutDown();
        }
        */
    }
}
