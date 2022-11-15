using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUp : Interactable
{
    Inventory inventory;
     public override void OnInteract()
    {
       //delete this (or object pooling)
       //add to inventory
       inventory.AddKey(this);
       //disable for now
       gameObject.SetActive(false);
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
