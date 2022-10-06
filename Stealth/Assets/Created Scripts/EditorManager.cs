using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EditorManager : MonoBehaviour
{
    
    public static EditorManager instance;
    PlayerAction inputAction;

    public Camera mainCam;
    public Camera editorCam;

    public GameObject prefab1;
    public GameObject prefab2;

    public GameObject prefab_enemy;

    public GameObject item;

    public bool editorMode = false;
    public bool instantiated = false;

    Vector3 mousePos;

    UIManager ui;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        inputAction = PlayerInputController.controller.inputAction;
        inputAction.Editor.EnableEditor.performed += cntxt => SwitchCamera();
         inputAction.Editor.DropItem.performed += cntxt => DropItem();


        mainCam.enabled = true;
        editorCam.enabled = false;

        ui = GetComponent<UIManager>();
    }

    private void SwitchCamera()
    {
        mainCam.enabled = !mainCam.enabled;
        editorCam.enabled = !editorCam.enabled;

        ui.ToggleEditorUI();

    }
     private void DropItem()
    {
        if (editorMode && instantiated)
       { 
            item.GetComponent<Rigidbody>().useGravity = true;
            item.GetComponent<Collider>().enabled = true;

            instantiated = false;

            //command = new PlaceItemCommand(item.transform.position, item.transform);
            //CommandInvoker.AddCommand(command);
       }
    }

    
    // Update is called once per frame
    void Update()
    {
        if (mainCam.enabled ==false && editorCam.enabled == true)
        {
            editorMode = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            editorMode = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (instantiated)
        {
            mousePos = Mouse.current.position.ReadValue();
            mousePos = new Vector3(mousePos.x, mousePos.y, 10f);

            item.transform.position = editorCam.ScreenToWorldPoint(mousePos);
        }
    }
}
