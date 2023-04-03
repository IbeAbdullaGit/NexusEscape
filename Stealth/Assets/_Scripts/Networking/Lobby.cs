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

    public GameObject clientManager;
    public GameObject serverManager;
   
    public void ConnectClient()
    {
        //turn on client manager
        clientManager.gameObject.SetActive(true);
        //we will keep this manager throughout the whole time
       // DontDestroyOnLoad(clientManager);
        
        //make client
        client = clientManager.GetComponent<NetworkManagerClient>();
        //client = GetComponent<NetworkManagerClient>();
        //client settings
        //client.ip = "127.0.0.1";
        //client.port = 8888;

        //start client
        client.StartClient();
        
        serverButton.interactable = false;

        GetComponent<UIManager>().ConnectClicked();
    }

    public void ConnectServer()
    {
        serverManager.gameObject.SetActive(true); //turn on server
        //DontDestroyOnLoad(serverManager);
        //make server
        server = serverManager.GetComponent<NetworkManagerServer>();
        //set server settings
        //server.port = 8888;
        //server.maxClientCount = 2;
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
            //the server is the one starting the game
            //send message to client to switch scene
             GetComponent<SwitchScene>().ChangeScene("Nexus1Server");

            Message message = Message.Create(MessageSendMode.Reliable, ServerToClientId.startGame);
            NetworkManagerServer.Singleton.Server.SendToAll(message);

            NetworkManagerServer.Singleton.Server.Stop();

            Debug.Log("Starting Game");
            // NetworkManagerClient.Singleton.Client.Connection.CanTimeout = false;
           // NetworkManagerClient.Singleton.Client.Connection.ResetTimeout();
           
            
        }
        
    }
}
