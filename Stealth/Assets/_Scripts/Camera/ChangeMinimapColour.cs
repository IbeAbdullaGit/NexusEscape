using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMinimapColour : MonoBehaviour
{
    public Material changeColour;
    private Material current;

    CameraMenu cameras;
    // Start is called before the first frame update
    void Start()
    {
        current = GetComponent<MeshRenderer>().material; //save beginning material
        cameras = GameObject.FindGameObjectWithTag("GameController").GetComponent<CameraMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        /* //check camera menu
        if (cameras.GetCurrentCamera() == GetComponentInParent<Camera>())
        {
            //same camera
            GetComponent<MeshRenderer>().material = changeColour;
        }
        else
        {
            GetComponent<MeshRenderer>().material = current;
        } */
        
    }

    public void ChangeColour()
    {
        GetComponent<MeshRenderer>().material = changeColour;
    }
    public void OriginalColour()
    {
        GetComponent<MeshRenderer>().material = current;
    }
}
