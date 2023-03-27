using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Riptide;
using UnityEngine.UI;

public class InteractionHandlerNexus1 : MonoBehaviour
{
     //public List<GameObject> connections;
   
     private static InteractionHandlerNexus1 _singleton;


    public static InteractionHandlerNexus1 Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(InteractionHandlerNexus1)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }
     private void Awake()
    {
        Singleton = this;
    }

     void DoInteractions(int type, string context, int context2 = 0)
     {
        
       
     }
     
     #region Messages    
    [MessageHandler((ushort)ServerToClientId.puzzleInteraction)]
    private static void PuzzleInteraction(Message message)
    {
        Debug.Log("Getting interaction");
        
        int type = message.GetInt();

        //1 - swipe keycard
        //2 - button press
        //3 - button meter puzzle

        string context = message.GetString();

        int context2 = message.GetInt(); //will not always have
        InteractionHandlerNexus1.Singleton.DoInteractions(type, context, context2);
        
    }
    #endregion
}
