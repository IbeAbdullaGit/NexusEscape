using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Riptide;

public class ChatManager : MonoBehaviour
{
    public string username;
    
    public int maxMessages = 25;

    public GameObject chatPanel, textObject;
    public TMP_InputField chatbox;

    public Color playerMessage, info;
    [SerializeField]
    List<Message2> messageList = new List<Message2>();
     private static ChatManager _singleton;  

     public static ChatManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(ChatManager)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }
     private void Awake()
    {
        Singleton = this;
    }
    
    // Update is called once per frame
    void Update()
    {
        //is chatbox activate
        if (chatbox.text != "")
        {
           
            //check for enter key
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //pass in username as prefix to text
                SendMessageToChat(username + ": " + chatbox.text, Message2.MessageType.playerMessage);
                //now clear chatbox
                chatbox.text = "";
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //chatbox.ActivateInputField();
            }
        }
    }

    public void SendMessageToChat(string text, Message2.MessageType mType)
    {
        if (messageList.Count >= maxMessages)
        {
            //destroy it, dont have excess resources
            Destroy(messageList[0].textObject.gameObject);
            
            //remove the oldest
            messageList.Remove(messageList[0]);
        }
        
        Message2 newMessage = new Message2();
        newMessage.text = text;

        GameObject newText = Instantiate(textObject, chatPanel.transform); //create text object so we can display it

        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.text = newMessage.text;
        //newMessage.textObject.color = MessageTypeColor(mType);

        messageList.Add(newMessage);


        //send between client and server
        if (NetworkManagerClient.Singleton !=null) //client
        {
            Message message = Message.Create(MessageSendMode.Reliable, ClientToServerId.chatMessage);
            message.AddString(text);
        
            NetworkManagerClient.Singleton.Client.Send(message);
        }
        if (NetworkManagerServer.Singleton !=null) //server
        {
             Message message = Message.Create(MessageSendMode.Reliable, ServerToClientId.chatMessage);
            message.AddString(text);
        
            NetworkManagerClient.Singleton.Client.Send(message);
        }
    }
    //basically an add that doesnt send to server
    public void AddMessageToChat(string text, Message2.MessageType mType)
    {
        if (messageList.Count >= maxMessages)
        {
            //destroy it, dont have excess resources
            Destroy(messageList[0].textObject.gameObject);
            
            //remove the oldest
            messageList.Remove(messageList[0]);
        }
        
        Message2 newMessage = new Message2();
        newMessage.text = text;

        GameObject newText = Instantiate(textObject, chatPanel.transform); //create text object so we can display it

        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.text = newMessage.text;
        //newMessage.textObject.color = MessageTypeColor(mType);

        messageList.Add(newMessage);

    }

    Color MessageTypeColor(Message2.MessageType mType)
    {
        Color color = info;
        switch(mType)
        {
            case Message2.MessageType.info:
                color = playerMessage;
                break;
            default:
                break;
        }
        return color;
    }
    //server side
     [MessageHandler((ushort)ClientToServerId.chatMessage)]
    private static void GetChatMessage(ushort fromClientId, Message message)
    {
        string text = message.GetString();
        //add to own chat
        ChatManager.Singleton.AddMessageToChat(text, Message2.MessageType.playerMessage);

    }
    //client side
    [MessageHandler((ushort)ServerToClientId.chatMessage)]
    private static void GetChatClient(Message message)
    {
       string text = message.GetString();
        //add to own chat
        ChatManager.Singleton.AddMessageToChat(text, Message2.MessageType.playerMessage);
    }

}

[System.Serializable]
public class Message2
{
    public string text;
    public Text textObject;
    public MessageType mesasgeType;

    public enum MessageType
    {
        playerMessage,
        info,
    }
}
