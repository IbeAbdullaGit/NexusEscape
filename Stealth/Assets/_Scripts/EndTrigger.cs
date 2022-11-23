using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
   private void OnTriggerEnter(Collider other) {
    
    if (other.tag == "Player") //if the player comes in
    {
        //trigger change here
        GameObject.FindGameObjectWithTag("GameController").GetComponent<SwitchScene>().ChangeScene("Section 1");
    }
   }
}
