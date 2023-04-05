using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Riptide;

public class EndTrigger : MonoBehaviour
{
   
   private void OnTriggerEnter(Collider other) {
    
    if (other.tag == "Player") //if the player comes in
    {
        //trigger change here
        Debug.Log("Switching Scene");
        //save
        //GameObject.FindGameObjectWithTag("GameController").GetComponent<SavePlugin>().SaveProgress();
        //send change scene message
        //only if server we can change scene
        //also are we in nexus 1 or nexus 2
        if (NetworkManagerServer.Singleton != null)
        {
                Debug.Log("server");
                if (InteractionServerNexus1.Singleton != null) //nexus 1
                {
                    Debug.Log("Go into nexus2");
                    //Time.timeScale = 0;
                ////networking, send
                //Message message = Message.Create(MessageSendMode.Reliable, ServerToClientId.puzzleInteraction);
                //message.AddInt(4); 
                //NetworkManagerServer.Singleton.Server.SendToAll(message);

                //Debug.Log("Scene switch");
          
                //GameObject.FindGameObjectWithTag("GameController").GetComponent<SwitchScene>().ChangeScene("InbetweenScene");

                /// send message to client to switch scene
               GameObject.FindGameObjectWithTag("GameController").GetComponent<SwitchScene>().ChangeScene("Nexus2Server");

                   Message message = Message.Create(MessageSendMode.Reliable, ServerToClientId.puzzleInteraction);
                    message.AddInt(4);
                    NetworkManagerServer.Singleton.Server.SendToAll(message);

                    //NetworkManagerServer.Singleton.Server.Stop();

                    Debug.Log("Starting Game");
                    //NetworkManagerClient.Singleton.Client.Connection.CanTimeout = false;
             }
             else if (InteractionMessages.Singleton !=null) //nexus 2
             {
                //networking, send
                Message message = Message.Create(MessageSendMode.Reliable, ServerToClientId.puzzleInteraction);
                message.AddInt(5); //button meter puzzle
                NetworkManagerServer.Singleton.Server.SendToAll(message);

                    GameObject.FindGameObjectWithTag("GameController").GetComponent<SwitchScene>().ChangeScene("Cutscene");
                }
        }
        //For testing purposes
            GameObject.FindGameObjectWithTag("GameController").GetComponent<SwitchScene>().ChangeScene("Cutscene");
        }
   }
}
