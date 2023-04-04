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

    public List<Slider> buttonMeters;



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
    private void Start() {
        //try connecting client again?
        //NetworkManagerClient.Singleton.Connect();
        //Debug.Log("Connect again");
    }
     private void Update() {
        
        ////manually send for testing purposes
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
           
            
        //    //send network message, to open the door
        //    Message message = Message.Create(MessageSendMode.Reliable, ClientToServerId.testMessage);
        //    //add an id so we know what we're talking about
        //    //send message
        //    NetworkManagerClient.Singleton.Client.Send(message);
        //     Debug.Log("Send test message");
        //    //Destroy(transform.gameObject.GetComponentInParent<Canvas>().gameObject);
        //}
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    NetworkManagerClient.Singleton.Connect();
        //    Debug.Log("Connecting again");
        //}
    }
     public void DoInteractions(int type, string context, int context2 =0)
     {
        Debug.Log("Nexus 1 interaction");
        
        //receive button press
        if (type ==1)
        {
            //rest of the info doesnt matter
            //activate typer
            buttonPress.GetComponent<ActivateTyper>().OnInteract();
        }
        else if (type == 2) //button meter
        {
            switch (context)
            {
                case "0":
                 {   //take the value for this button
                    buttonMeters[0].value = context2;
                    break;
                 }
                case "1":
                 {   //take the value for this button
                    buttonMeters[1].value = context2;
                    break;
                 }
                case "2":
                 {   //take the value for this button
                    buttonMeters[2].value = context2;
                    break;
                 }
                case "3":
                  {  //take the value for this button
                    buttonMeters[3].value = context2;
                    break;
                  }
                default:
                    Debug.Log("Nothing worked");
                    break;
            }
        }
        else if (type ==4)
        {
            //switch level
            Debug.Log("Switching Scene");
            //save
            //GameObject.FindGameObjectWithTag("GameController").GetComponent<SavePlugin>().SaveProgress();
            GameObject.FindGameObjectWithTag("GameController").GetComponent<SwitchScene>().ChangeScene("Nexus2Client");
            //Time.timeScale = 0;
        }
        else if (type ==5) //go to nexus 2
        {
            //switch level
            Debug.Log("Switching Scene");
            //save
            //GameObject.FindGameObjectWithTag("GameController").GetComponent<SavePlugin>().SaveProgress();
            GameObject.FindGameObjectWithTag("GameController").GetComponent<SwitchScene>().ChangeScene("EndScene");
            //Time.timeScale = 0;
        }
       
     }
}
