using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Username : MonoBehaviour
{
    public TMP_InputField inputBox;

    string user;

    // Update is called once per frame
    void Update()
    {
        if (inputBox.text != "" && inputBox.isFocused)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                user = inputBox.text;
                inputBox.interactable = false;
                //change for the chatbox
                GetComponent<ChatManager>().username = user;
                Debug.Log("Username changed");
            }
        }
    }
    public void ChangeUsername()
    {
        GetComponent<ChatManager>().username = inputBox.text;
    }
}
