using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Riptide;
using UnityEngine.UI;
using TMPro;

public class Lobby : MonoBehaviour
{
    NetworkManagerServer server;
    NetworkManagerClient client;

    public Button clientButton;
    public Button serverButton;
    public Button startButton;

   
    public void ConnectClient()
    {
        //make client
        client = gameObject.AddComponent<NetworkManagerClient>();

        client.StartClient();
        
        serverButton.interactable = false;

        GetComponent<UIManager>().ConnectClicked();
    }

    public void ConnectServer()
    {
        //make server
        server = gameObject.AddComponent<NetworkManagerServer>();

        //cant interact as client now
        clientButton.interactable = false;
        
        //we just want to start the server
        server.StartServer();
        //have we started it?

        //wait until player connects, then make start button interactable
        //start button changes scene for server and also changes scene for client
        //we also can change the managers so that we don't spawn anything because we don't need it

    }
    public void StartGame()
    {
        //send both client and server to their respective scenes
        if (server != null)
        {
            GetComponent<SwitchScene>().ChangeScene("Nexus1Server");
            //when loading scene, just load normal as we would expect
            //but we will have to figure out who is who
        }
        if (client != null)
        {
            GetComponent<SwitchScene>().ChangeScene("Nexus1Client");
        }
    }
}
