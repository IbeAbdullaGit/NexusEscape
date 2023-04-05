using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Riptide;
using UnityEngine.SceneManagement;

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
    [MessageHandler((ushort)ServerToClientId.buttonMessage)]
    private static void LightUpButtons(Message message)
    {
        //for nexus 2 specifically
        int type = message.GetInt();
        InteractionHandler.
    }
    [MessageHandler((ushort)ServerToClientId.resetGame)]
    private static void RestartGame(Message message)
    {
        Debug.Log("Getting interaction");
        int type = message.GetInt();

        if (type == 1)
        {//simply reload game
         //send in current scene name to scene switcher, so the level restarts
            GameObject.FindGameObjectWithTag("GameController").GetComponent<SwitchScene>().ChangeScene(SceneManager.GetActiveScene().name);
        }
        else if (type ==2)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<SwitchScene>().ChangeScene("Menu");
            //also disconnect - by destroying game object
            Destroy(GameObject.FindGameObjectWithTag("NetworkClient"));
            
        }
        //set back the time scale just in case
        Time.timeScale = 1;

    }
    #endregion
}
