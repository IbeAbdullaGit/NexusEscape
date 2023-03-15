using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialog : MonoBehaviour
{
    public string message;
     public string message2;


    private void OnTriggerEnter(Collider other) {
        
        if (other.tag == "Player")
        {
            Debug.Log("Activating dialog");
            
            //send message to manager
            GameObject.FindGameObjectWithTag("GameController").GetComponent<CreateDialog>().ChangeText(message);
            GameObject.FindGameObjectWithTag("GameController").GetComponent<CreateDialog>().ChangeTextPlayer2(message2);
            GameObject.FindGameObjectWithTag("GameController").GetComponent<CreateDialog>().ChangeUI();

            //we can destroy this now
            Destroy(gameObject);
        }
    }
}
