using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerController playerController;
     void Start() {
        
        playerController.SetGrounded(true);
    }
    private void OnTriggerEnter(Collider other) {
        
       /*  if (other.gameObject == playerController.gameObject)
        {
            return;
        } */
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Ground")
            playerController.SetGrounded(true);
    }
    private void OnTriggerExit(Collider other) {
        
        // if (other.gameObject == playerController.gameObject)
        // {
        //     return;
        // }
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Ground")
            playerController.SetGrounded(false);
    }
    private void OnTriggerStay(Collider other) {
        
        /* if (other.gameObject == playerController.gameObject)
        {
            return;
        } */
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Ground")
            playerController.SetGrounded(true);
    }

    private void Update() {
        //Debug.Log(playerController.grounded);
    }
}
