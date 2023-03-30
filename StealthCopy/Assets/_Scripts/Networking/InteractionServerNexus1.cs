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
    /* //receiving messages
    #region Messages
     [MessageHandler((ushort)ClientToServerId.typingPuzzleFinish)]
    private static void TypingPuzzleAnswer(ushort fromClientId, Message message)
    {
       //let us know if this is the answer for 1st or 2nd type
       int type = message.GetInt();
       if (type ==0) //the text
       {
            InteractionServerNexus1.Singleton.typer.GetComponent<LinkedPuzzle>().ActivateText();
            //in the future, may remove the typer from this version of the client entirely
            //in which case
           // InteractionMessages.Singleton.GetComponent<LinkedPuzzle>().ActivateText();
       }
       else //must equal 1, its the door
       {
            InteractionServerNexus1.Singleton.typer.GetComponent<LinkedPuzzle>().ActivateDoor();
            //InteractionMessages.Singleton.GetComponent<LinkedPuzzle>().ActivateDoor();
       }
    }
     #endregion */
}
   

