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
    //functions for sending messages
    public void KeycardInteract()
    {
        Message message = Message.Create(MessageSendMode.Reliable, ServerToClientId.puzzleInteraction);
        message.AddInt(1); //swipe keycard swipe
        message.AddString("nexus 2 door 1"); //for nexus 2 specifically

        NetworkManagerServer.Singleton.Server.SendToAll(message);
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
         message.AddString(id.ToString());
         message.AddInt(power);

         NetworkManagerServer.Singleton.Server.SendToAll(message);
    }
    //receiving messages
    #region Messages
    [MessageHandler((ushort)ClientToServerId.pipePuzzleFinish)]
    private static void PipePuzzleAnswer(Message message)
    {
       //get int
       var n = message.GetInt(); //this is actually not too important, we don't do anything with this int
       //but now we need to activate the aftermath
       InteractionMessages.Singleton.pipeManager.GetComponent<PipeManager>().TriggerDoor();
       InteractionMessages.Singleton.pipeManager.GetComponent<PipeManager>()._otherdoor.GetComponent<Door>().isOpen = false;
        InteractionMessages.Singleton.pipeManager.GetComponent<PipeManager>()._otherdoor.GetComponent<Door>().OpenDoor();

    }
     [MessageHandler((ushort)ClientToServerId.typingPuzzleFinish)]
    private static void TypingPuzzleAnswer(Message message)
    {
       //let us know if this is the answer for 1st or 2nd type
       int type = message.GetInt();
       if (type ==0) //the text
       {
            InteractionMessages.Singleton.typer.GetComponent<LinkedPuzzle>().ActivateText();
            //in the future, may remove the typer from this version of the client entirely
            //in which case
           // InteractionMessages.Singleton.GetComponent<LinkedPuzzle>().ActivateText();
       }
       else //must equal 1, its the door
       {
            InteractionMessages.Singleton.typer.GetComponent<LinkedPuzzle>().ActivateDoor();
            //InteractionMessages.Singleton.GetComponent<LinkedPuzzle>().ActivateDoor();
       }
    }
     #endregion
}
   

