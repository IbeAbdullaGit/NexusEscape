using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Riptide;

public class MessageHandler : MonoBehaviour
{
     #region Messages    
    [MessageHandler((ushort)ServerToClientId.puzzleInteraction)]
    private static void PuzzleInteraction(Message message)
    {
        Debug.Log("Getting interaction");
        
        int type = message.GetInt();

        //1 - swipe keycard
        //2 - button press
        //3 - button meter puzzle
        //4 - end level

        Debug.Log("Interaction type: " + type);

        string context = message.GetString();

        int context2 = message.GetInt(); //will not always have

        Debug.Log("Extras: " + context + " " + context2);
        if (InteractionHandler.Singleton != null)
            InteractionHandler.Singleton.DoInteractions(type, context, context2);
        if (InteractionHandlerNexus1.Singleton != null)
            InteractionHandlerNexus1.Singleton.DoInteractions(type, context, context2);
        
    }
    #endregion
}
