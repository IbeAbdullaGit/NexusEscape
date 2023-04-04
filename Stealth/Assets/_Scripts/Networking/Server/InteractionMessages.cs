using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Riptide;
using UnityEngine.UI;
using TMPro;

public class InteractionMessages : MonoBehaviour
{
    private static InteractionMessages _singleton;  

    public GameObject typer; 

    public GameObject pipeManager;

    public GameObject distractionButton;

    public static InteractionMessages Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(InteractionMessages)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }
     private void Awake()
    {
        Singleton = this;
    }
    private void Start()
    {
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
    public void KeycardInteract()
    {
        Message message = Message.Create(MessageSendMode.Reliable, ServerToClientId.puzzleInteraction);
        message.AddInt(1); //swipe keycard swipe
        message.AddString("nexus 2 door 1"); //for nexus 2 specifically

        NetworkManagerServer.Singleton.Server.SendToAll(message);

         Debug.Log("Sending to client");
    }
    public void TyperInteract()
    {
        Message message = Message.Create(MessageSendMode.Reliable, ServerToClientId.puzzleInteraction);
        message.AddInt(2); //swiper
        message.AddString("nexus 2 activate typer 1"); //for nexus 2 specifically

        NetworkManagerServer.Singleton.Server.SendToAll(message);
    }
     public void TyperInteract2()
    {
        Message message = Message.Create(MessageSendMode.Reliable, ServerToClientId.puzzleInteraction);
        message.AddInt(2); //swiper
        message.AddString("nexus 2 activate typer 2"); //for nexus 2 specifically

        NetworkManagerServer.Singleton.Server.SendToAll(message);
    }
    public void ButtonMeterInteract(int id, int power)
    {
         Message message = Message.Create(MessageSendMode.Reliable, ServerToClientId.puzzleInteraction);
         message.AddInt(3); //button meter puzzle
         message.AddString((id).ToString());
         message.AddInt(power);

         NetworkManagerServer.Singleton.Server.SendToAll(message);
    }
    
}
   

