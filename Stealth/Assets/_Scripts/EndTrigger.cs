using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    public string nextLevel;
   private void OnTriggerEnter(Collider other) {
    
    if (other.tag == "Player") //if the player comes in
    {
        //trigger change here
        Debug.Log("Switching Scene");
        //save
        GameObject.FindGameObjectWithTag("GameController").GetComponent<SavePlugin>().SaveProgress();
        GameObject.FindGameObjectWithTag("GameController").GetComponent<SwitchScene>().ChangeScene(nextLevel);
    }
   }
}
