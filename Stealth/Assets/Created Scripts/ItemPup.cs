using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPup : MonoBehaviour
{
    public Transform PickupPos;
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
        if (pup) // if you enter thecollider of the objecct
        {
            if (Input.GetKeyDown(KeyCode.X))  
            {
                Rigidbody monkey = Instantiate(ItemtoPup, PickupPos.position, Quaternion.identity).GetComponent<Rigidbody>();
                Items pick = new Items(ItemtoPup, new Pickup()); //makes the rigidbody not be acted upon by forces
                subject.AddObserver(pick);
                Debug.Log("Picked up");
                hasup = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.X) && hasup) 
        {
            Items D = new Items(ItemtoPup, new Drop());
            subject.AddObserver(D);
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
            subject.Notify();
        }
    }
    private void OnCollisionExit(Collision other)
    {
        pup = false; 

    }
}
