using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Riptide;

public class MessageHandlerServer : MonoBehaviour
{
    //receiving messages
    #region Messages
    [MessageHandler((ushort)ClientToServerId.pipePuzzleFinish)]
    private static void PipePuzzleAnswer(ushort fromClientId, Message message)
    {
       Debug.Log("Opening Door");
       if (InteractionMessages.Singleton != null)
       {//get int
       var n = message.GetInt(); //this is actually not too important, we don't do anything with this int
       //but now we need to activate the aftermath
       InteractionMessages.Singleton.pipeManager.GetComponent<PipeManager>().TriggerDoor();
       InteractionMessages.Singleton.pipeManager.GetComponent<PipeManager>()._otherdoor.GetComponent<Door>().isOpen = false;
        InteractionMessages.Singleton.pipeManager.GetComponent<PipeManager>()._otherdoor.GetComponent<Door>().OpenDoor();
       }

    }
     [MessageHandler((ushort)ClientToServerId.typingPuzzleFinish)]
    private static void TypingPuzzleAnswer(ushort fromClientId, Message message)
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
            Debug.Log("Got typer message");
            //this is for both nexus 1 and nexus 2
            if (InteractionMessages.Singleton != null)
                InteractionMessages.Singleton.typer.GetComponent<LinkedPuzzle>().ActivateDoor();
            if (InteractionServerNexus1.Singleton!= null)
                InteractionServerNexus1.Singleton.typer.GetComponent<LinkedPuzzle>().ActivateDoor();
            //InteractionMessages.Singleton.GetComponent<LinkedPuzzle>().ActivateDoor();
       }
       
    }
    [MessageHandler((ushort)ClientToServerId.distraction)]
    private static void SpawnDistraction(ushort fromClientId, Message message)
    {
       if (InteractionMessages.Singleton != null)
       {//simulate button press for the camera distraction, need to know which camera we're looknig at
       int cam = message.GetInt();
       Debug.Log(cam);
       //just spawn distraction where needed, calling button
        InteractionMessages.Singleton.distractionButton.GetComponent<SpawnDistraction>().RemoteInteract(cam);
       

       Debug.Log("Distracting");
       }
       if (InteractionServerNexus1.Singleton != null)
       {//simulate button press for the camera distraction, need to know which camera we're looknig at
            int cam = message.GetInt();
             Debug.Log(cam);
            //just spawn distraction where needed, calling button
       
            InteractionServerNexus1.Singleton.distractionButton.GetComponent<SpawnDistraction>().RemoteInteract(cam);
       

        Debug.Log("Distracting");
       }
       
    }
    [MessageHandler((ushort)ClientToServerId.testMessage)]
    private static void TestConnection(ushort fromClientId, Message message)
    {
        Debug.Log("Got message");
    }
     #endregion
}
