using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.Characters.FirstPerson;

public class InteractKeypad : MonoBehaviour
{
    public bool openMenu = false;

    public Canvas menuUI;

    //keypad reference
    public GameObject keypad;
    Keypad instance;

    public string answer;
    
    bool canTurnOff = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = keypad.GetComponent<Keypad>();
        //change answer depending on need
        instance.answer = answer;
        menuUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
            
        if (openMenu)
        {
            if (Input.GetKey(KeyCode.Z)){
                ChangeUI();
                
            }
        }
        else{
             if (menuUI.enabled && Input.GetKey(KeyCode.Z)){
                ChangeUI();
               
                
            }
        } 
        /* if (menuUI.enabled)
        {
            if (Input.GetKey(KeyCode.Z)){
                menuUI.enabled = !menuUI.enabled;  
                openMenu = false;  
            }
        } */
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag =="Player2")
        {
            openMenu = true;
        }
    }
    private void OnTriggerEnter(Collider other) {
        
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag =="Player2")
        {
            openMenu = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag =="Player2")
        {
            openMenu = false;
        }
    }
    public void ChangeUI()
    {
        menuUI.enabled = !menuUI.enabled;
        if (Cursor.lockState == CursorLockMode.Locked)
        {
             Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; 
        }  
        else
        {
             Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false; 
        }
    }
   

}
