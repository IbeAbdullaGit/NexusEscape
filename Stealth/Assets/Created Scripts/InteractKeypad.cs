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

    // Start is called before the first frame update
    void Start()
    {
        instance = keypad.GetComponent<Keypad>();
        //change answer depending on need
        instance.answer = answer;
        menuUI.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
            
        if (openMenu)
        {
            if (Input.GetKey(KeyCode.Z)){
                ChangeUI();
                openMenu = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;   
                
                //break;
            }
        }
        else{
             if (Input.GetKey(KeyCode.Z)){
                ChangeUI();
                //Cursor.lockState = CursorLockMode.Locked;
                
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
    public void ChangeUI()
    {
        menuUI.enabled = !menuUI.enabled;  
    }
   

}
