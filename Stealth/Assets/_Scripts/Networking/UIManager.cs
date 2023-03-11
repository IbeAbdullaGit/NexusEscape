using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Riptide;
using Riptide.Utils;
using TMPro;

public class UIManager : MonoBehaviour
{
   
   [Header("Connect")]
    [SerializeField] private GameObject connectUI;
    [SerializeField] private TMP_InputField usernameField;
   private static UIManager _singleton;
    public static UIManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(UIManager)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }
    

    private void Awake()
    {
        Singleton = this;
    }
    
    private void Start() {
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    

    public void ConnectClicked()
    {
        usernameField.interactable = false;
        connectUI.SetActive(false);
        GameObject.Destroy(connectUI);

        NetworkManagerClient.Singleton.Connect();

        //mouse stuff
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        
    }

    public void BackToMain()
    {
        usernameField.interactable = true;
        connectUI.SetActive(true);
    }

    public void SendName()
    {
        Message message = Message.Create(MessageSendMode.Reliable, ClientToServerId.name);
        message.AddString(usernameField.text);
        NetworkManagerClient.Singleton.Client.Send(message);
    }
}
