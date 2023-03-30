using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Riptide;

public class EndTrigger : MonoBehaviour
{
    public bool nexus1, nexus2 = false;
   private void OnTriggerEnter(Collider other) {
    
    if (other.tag == "Player") //if the player comes in
    {
        //trigger change here
        Debug.Log("Switching Scene");
        //save
        GameObject.FindGameObjectWithTag("GameController").GetComponent<SavePlugin>().SaveProgress();
        //send change scene message
        //only if server we can change scene
        //also are we in nexus 1 or nexus 2
        if (NetworkManagerServer.Singleton != null)
        { 
             if (nexus1)
             {
                //networking, send
                Message message = Message.Create(MessageSendMode.Reliable, ServerToClientId.puzzleInteraction);
                message.AddInt(4); //button meter puzzle
                NetworkManagerServer.Singleton.Server.SendToAll(message);
          
                GameObject.FindGameObjectWithTag("GameController").GetComponent<SwitchScene>().ChangeScene("InbetweenScene");
             }
             else if (nexus2)
             {
                //networking, send
                Message message = Message.Create(MessageSendMode.Reliable, ServerToClientId.puzzleInteraction);
                message.AddInt(5); //button meter puzzle
                NetworkManagerServer.Singleton.Server.SendToAll(message);
          
                GameObject.FindGameObjectWithTag("GameController").GetComponent<SwitchScene>().ChangeScene("EndScene");
             }
        }
    }
   }
}
