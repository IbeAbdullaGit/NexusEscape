using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CameraMenu : MonoBehaviour
{

    public static CameraMenu instance;
    PlayerAction inputAction;
    public bool openMenu = false;

    //public Canvas menuUI;

    public Material[] cameras;

    public Camera[] cams;
    int cameraIndex = 0;
    public GameObject cameraView;

    PopUpSystem pop;


    // Start is called before the first frame update
    void Start()
    {
         if (instance == null)
        {
            instance = this;
        }
        
        inputAction = PlayerInputController.controller.inputAction;
        inputAction.Player2.Menu.performed += cntxt => OpenMenu();
        //inputAction.Player2.NextCamera.performed += cntxt => SwitchCameras();
       
        //menuUI.enabled = false;

        //pop = GameObject.FindGameObjectWithTag("GameController").GetComponent<PopUpSystem>();

        cameraIndex = 0;
        //let first camera change colour
        GetCurrentCamera().GetComponentInChildren<ChangeMinimapColour>().ChangeColour();
        
    }

    public Camera GetCurrentCamera()
    {
        return cams[cameraIndex];
    }
    public Camera GetCameraFromIndex(int n)
    {
        return cams[n];
    }
    public int GetCameraIndex()
    {
        return cameraIndex;
    }
    void OpenMenu()
    {
       
        //menuUI.enabled = true;
        //menuUI.enabled = !menuUI.enabled;

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
        openMenu = !openMenu;
        if (openMenu)
        {
            //set specifically the camera menu stuff to open, unneeded for now
            pop.PopUp();
            
        }
        else{
            pop.ClosePop();
        }
        
    }
    public void SwitchCameras()
    {
        //Debug.Log("Switching cameras");
        //swap current camera material back
        GetCurrentCamera().GetComponentInChildren<ChangeMinimapColour>().OriginalColour();

        cameraIndex++;
        if (cameraIndex == cameras.Length)
        {
            cameraIndex = 0;
        }
        //cameraView.material = cameras[cameraIndex];
       
       //swap new camera plane texture
       GetCurrentCamera().GetComponentInChildren<ChangeMinimapColour>().ChangeColour();

        var materialsCopy = cameraView.GetComponent<MeshRenderer>().materials;
        materialsCopy[1] = cameras[cameraIndex]; //2ND MATERIAL FOR THIS CONTEXT
        cameraView.GetComponent<MeshRenderer>().materials = materialsCopy;
    }
    public void SwitchCamerasBack()
    {
         //Debug.Log("Switching cameras");
        cameraIndex--;
        if (cameraIndex <= 0)
        {
            cameraIndex = cameras.Length - 1;
        }
        //cameraView.material = cameras[cameraIndex];

        var materialsCopy = cameraView.GetComponent<MeshRenderer>().materials;
        materialsCopy[1] = cameras[cameraIndex]; //2ND MATERIAL FOR THIS CONTEXT
        cameraView.GetComponent<MeshRenderer>().materials = materialsCopy;
    }
}
