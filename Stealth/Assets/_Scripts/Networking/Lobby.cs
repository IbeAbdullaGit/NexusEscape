using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Riptide;
using UnityEngine.UI;
using TMPro;
using System.Net.Sockets;
using System.Net;

public class Lobby : MonoBehaviour
{
    NetworkManagerServer server;
    NetworkManagerClient client;

    public Button clientButton;
    public Button serverButton;
    public Button startButton;

    public GameObject clientManager;
    public GameObject serverManager;
    public GameObject serverIPDisplay;
    public GameObject inputIP;
   
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

        //set client IP
        client.ip = inputIP.GetComponent<TMP_InputField>().text;

        //start client
        client.StartClient();
        
        serverButton.interactable = false;
        clientButton.interactable = false;

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
        serverButton.interactable = false;
        
        //we just want to start the server
        server.StartServer();
        //have we started it?

        //wait until player connects, then make start button interactable
        //start button changes scene for server and also changes scene for client
        //we also can change the managers so that we don't spawn anything because we don't need it

        //get ip
        string localIP;
        using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
        {
            socket.Connect("8.8.8.8", 8888);
            IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
            localIP = endPoint.Address.ToString();
        }

        Debug.Log("Ip address: " + localIP);
        serverIPDisplay.GetComponent<TMP_Text>().text = "IP address: " + localIP;

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

            //NetworkManagerServer.Singleton.Server.Stop();

            Debug.Log("Starting Game");
            //NetworkManagerClient.Singleton.Client.Connection.CanTimeout = false;
           // NetworkManagerClient.Singleton.Client.Connection.ResetTimeout();
           
            
        }
        
    }
}
