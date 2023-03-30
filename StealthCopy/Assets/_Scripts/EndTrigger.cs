using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Riptide;

public class EndTrigger : MonoBehaviour
{
   public bool networking = false;
   private void OnTriggerEnter(Collider other) {
    
    if (other.tag == "Player") //if the player comes in
    {
        //trigger change here
        Debug.Log("Switching Scene");
        //save
        GameObject.FindGameObjectWithTag("GameController").GetComponent<SavePlugin>().SaveProgress();
        GameObject.FindGameObjectWithTag("GameController").GetComponent<SwitchScene>().ChangeScene("InbetweenScene");

        if (networking)
        {//networking, send
        Message message = Message.Create(MessageSendMode.Reliable, ServerToClientId.puzzleInteraction);
         message.AddInt(4); //button meter puzzle
         NetworkManagerServer.Singleton.Server.SendToAll(message);
        }
    }
   }
}
