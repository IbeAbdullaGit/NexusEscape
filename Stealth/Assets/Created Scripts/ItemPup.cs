using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPup : MonoBehaviour
{
    public GameObject PickupPos;
    bool pup;
    GameObject ItemtoPup;
    bool hasup;
    Subject subject = new Subject();
   
    void Start()
    {
        pup = false;    //setting both to false
        hasup = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pup == true) // if you enter thecollider of the objecct
        {
            if (Input.GetKeyDown(KeyCode.X))  
            {
                
                Items pick = new Items(ItemtoPup, new Pickup()); //makes the rigidbody not be acted upon by forces
                subject.AddObserver(pick);
                ItemtoPup.transform.position = PickupPos.transform.position; //Moves object to the object position
                ItemtoPup.transform.parent = PickupPos.transform; //Makes obhect the parent
                Debug.Log("Picked up");
                hasup = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.L) && hasup) 
        {
            Items D = new Items(ItemtoPup, new Drop());
            subject.AddObserver(D);
            ItemtoPup.transform.parent = null;
            hasup = false;

        }
    }
    private void OnCollisionEnter(Collision other) // to see when the player enters the collider
    {
        if (other.collider.tag == "pickup") 
        {
            pup = true;
            ItemtoPup = other.gameObject;
            subject.Notify();
        }
    }
    private void OnCollisionStay(Collision other) // to see when the player enters the collider
    {
        if (other.collider.tag == "pickup") 
        {
            pup = true;
            ItemtoPup = other.gameObject;
            Debug.Log("collided");
            subject.Notify();
        }
    }
    private void OnCollisionExit(Collision other)
    {
        pup = false;
        subject.Notify();
    }
}
