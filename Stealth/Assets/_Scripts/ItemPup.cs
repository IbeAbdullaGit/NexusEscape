using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPup : MonoBehaviour
{
    public GameObject PickupPos;
    bool pup;
    GameObject ItemtoPup;
    bool hasup;
   
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
            if (Input.GetKeyDown(KeyCode.M))  
            {

                ItemtoPup.GetComponent<Rigidbody>().isKinematic = true;
                ItemtoPup.transform.position = PickupPos.transform.position; //Moves object to the object position
                ItemtoPup.transform.parent = PickupPos.transform; //Makes obhect the parent
                Debug.Log("Picked up");
                hasup = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.L) && hasup) 
        {
            ItemtoPup.GetComponent<Rigidbody>().isKinematic = false;
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
            
        }
    }
    private void OnCollisionStay(Collision other) // to see when the player enters the collider
    {
        if (other.collider.tag == "pickup") 
        {
            pup = true;
            ItemtoPup = other.gameObject;
            
        }
    }
    private void OnCollisionExit(Collision other)
    {
        pup = false;
        
    }
}
