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
     private void Awake()
    {
        Singleton = this;
    }
    //functions for sending messages
    //we only need button press
    public void TyperInteract()
    {
        Message message = Message.Create(MessageSendMode.Reliable, ServerToClientId.puzzleInteraction);
        message.AddInt(1); //swiper
        
        NetworkManagerServer.Singleton.Server.SendToAll(message);
    }
}
   

