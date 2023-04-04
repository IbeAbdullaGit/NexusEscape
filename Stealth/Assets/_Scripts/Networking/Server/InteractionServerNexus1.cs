using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Riptide;
using UnityEngine.UI;
using TMPro;

public class InteractionServerNexus1 : MonoBehaviour
{
    private static InteractionServerNexus1 _singleton;  

    public GameObject typer; 

     public GameObject distractionButton;

    public static InteractionServerNexus1 Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(InteractionServerNexus1)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }
   //for this, we dont actually need it to be on the same manager as the main client stuff
     private void Awake()
    {
        Singleton = this;
        //make sure server is connected
        //NetworkManagerServer.Singleton.StartServer();
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(NetworkManagerServer.Singleton.Server.IsRunning);
        }
    }

    private void Start() {
        //at the start of this script, send spawn message to hacker
        //so that our player can be in
        //spawn our player
        //our player should have the player server script
        PlayerServer player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerServer>();

        player.name = "Player ground";
        player.Id = 2;
        player.Username = "ground";

        //send spawn to other player, and this should send movement messages
        
        player.SendSpawned();

        Debug.Log("Sending spawn message");
    }
    //functions for sending messages
    //we only need button press
    public void TyperInteract()
    {
        Message message = Message.Create(MessageSendMode.Reliable, ServerToClientId.puzzleInteraction);
        message.AddInt(1); //swiper
        
        NetworkManagerServer.Singleton.Server.SendToAll(message);
    }
     public void TyperInteract2()
    {
        Message message = Message.Create(MessageSendMode.Reliable, ServerToClientId.puzzleInteraction);
        
        message.AddInt(1); //swiper
        message.AddString("nexus 2 activate typer 2"); //for nexus 2 specifically

        NetworkManagerServer.Singleton.Server.SendToAll(message);
    }
    public void ButtonMeterInteract(int id, int power)
    {
         Message message = Message.Create(MessageSendMode.Reliable, ServerToClientId.puzzleInteraction);
         message.AddInt(2); //button meter puzzle for nexus 1
         message.AddString(((id)).ToString());
         message.AddInt(power);

         NetworkManagerServer.Singleton.Server.SendToAll(message);

        Debug.Log("Sending message");
    }
}
   

