using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Riptide;
using UnityEngine.UI;

public class InteractionHandlerNexus1 : MonoBehaviour
{
     //public List<GameObject> connections;
   
     private static InteractionHandlerNexus1 _singleton;

    public GameObject buttonPress;



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

     public void DoInteractions(int type)
     {
        //receive button press
        if (type ==1)
        {
            //rest of the info doesnt matter
            //activate typer
            buttonPress.GetComponent<ActivateTyper>().OnInteract();
        }
       
     }
}
