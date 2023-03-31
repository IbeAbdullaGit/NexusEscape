using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Riptide;
using UnityEngine.UI;

public class InteractionHandler : MonoBehaviour
{
     //public List<GameObject> connections;
     public GameObject keySwiper1;
     private static InteractionHandler _singleton;

    public GameObject button1;
    public GameObject button2;
    public List<Slider> buttonMeters;
 

    public static InteractionHandler Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(InteractionHandler)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }
     private void Awake()
    {
        Singleton = this;
    }

    private void Update() {
        
        //manually send for testing purposes
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Completing pipe puzzle?");
            
            //send network message, to open the door
            Message message = Message.Create(MessageSendMode.Reliable, ClientToServerId.pipePuzzleFinish);
            //add an id so we know what we're talking about
            message.AddInt(1); //1 - for this case
            //send message
            NetworkManagerClient.Singleton.Client.Send(message);
            
            //Destroy(transform.gameObject.GetComponentInParent<Canvas>().gameObject);
        }
    }
    public void DoInteractions(int type, string context, int context2 = 0)
     {
        Debug.Log("Nexus 2 interaction");
        //convert to switch case after
        if (type == 1)
        {
            //which door to open?
            if (context == "nexus 2 door 1")
            {
                //activates pipe puzzle on this need
                keySwiper1.GetComponent<KeycardSwiper>().OnInteractNoCheck();
                //need to answer send message to other side, when finished and door open
            }
        }
        else if (type ==2)
        {
            if (context == "nexus 2 activate typer 1")
            {
                //activate typing puzzle
                button1.GetComponent<ActivateTyper>().OnInteract();
                //then make sure to send answer back
            }
            else if (context =="nexus 2 activate typer 2")
            {
                //activate typing puzzle
                button2.GetComponent<ActivateTyper>().OnInteract();
                //then make sure to send answer back
            }
        }
        else if (type ==3)
        {
            //main important thing is to be changing the value of the meters accordingly, basically we just want the value
            //of the button from the server
            switch (context)
            {
                case "1":
                 {   //take the value for this button
                    buttonMeters[0].value = context2;
                    break;
                 }
                case "2":
                 {   //take the value for this button
                    buttonMeters[1].value = context2;
                    break;
                 }
                case "3":
                 {   //take the value for this button
                    buttonMeters[2].value = context2;
                    break;
                 }
                case "4":
                  {  //take the value for this button
                    buttonMeters[3].value = context2;
                    break;
                  }
                case "5":
                  {  //take the value for this button
                    buttonMeters[4].value = context2;
                    break;
                  }
                case "6":
                   { //take the value for this button
                    buttonMeters[5].value = context2;
                    break;
                   }
                case "7":
                 {   //take the value for this button
                    buttonMeters[6].value = context2;
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
            GameObject.FindGameObjectWithTag("GameController").GetComponent<SavePlugin>().SaveProgress();
            GameObject.FindGameObjectWithTag("GameController").GetComponent<SwitchScene>().ChangeScene("InbetweenScene");
        }
        else if (type ==5) //go to nexus 2
        {
            //switch level
            Debug.Log("Switching Scene");
            //save
            GameObject.FindGameObjectWithTag("GameController").GetComponent<SavePlugin>().SaveProgress();
            GameObject.FindGameObjectWithTag("GameController").GetComponent<SwitchScene>().ChangeScene("EndScene"); //ending scene
        }
     }
     
    
}
