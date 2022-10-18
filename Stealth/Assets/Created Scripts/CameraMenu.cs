using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CameraMenu : MonoBehaviour
{

    public static CameraMenu instance;
    PlayerAction inputAction;
    public bool openMenu = false;

    public Canvas menuUI;

    public Material[] cameras;
    int cameraIndex = 0;
    public Image cameraView;


    // Start is called before the first frame update
    void Start()
    {
         if (instance == null)
        {
            instance = this;
        }
        
        inputAction = PlayerInputController.controller.inputAction;
        inputAction.Player2.Menu.performed += cntxt => OpenMenu();
        inputAction.Player2.NextCamera.performed += cntxt => SwitchCameras();
       
        menuUI.enabled = false;
        
    }
    void OpenMenu()
    {
        menuUI.enabled = !menuUI.enabled;
        openMenu = !openMenu;
        if (openMenu)
        {
            //set specifically the camera menu stuff to open, unneeded for now
            
        }
    }
    void SwitchCameras()
    {
        cameraIndex++;
        if (cameraIndex == cameras.Length)
        {
            cameraIndex = 0;
        }
        cameraView.material = cameras[cameraIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
