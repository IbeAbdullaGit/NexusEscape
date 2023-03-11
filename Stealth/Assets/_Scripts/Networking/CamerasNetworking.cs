using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Riptide;

public class CamerasNetworking : MonoBehaviour
{
     bool hasNumber = false;
    public int numberCams;

    public Material baseMat;
     public List<Material> allcams;

    public GameObject cameraView;

    bool runOnce = false;

    public void SetInitial(int m)
    {
        if (!hasNumber)
        {
            numberCams = m;
            hasNumber = true;
            //run once
            for (int i=0; i< numberCams; i++)
            {
                allcams.Add(baseMat);
            }
            
        }
    }
    public void SetTexture(int index, Texture2D tex)
    {
        //apply it to the object
        allcams[index].mainTexture = tex;
    }
    
    /* #region Messages 
    //handle cameras
    [MessageHandler((ushort)ServerToClientId.cameras)]
    private static void GetCameras(Message message) //client side DOES NOT HAVE USHORT
    {
        if (!hasNumber)
        {
            numberCams = message.GetInt();
            hasNumber = true;
            //run once
            for (int i=0; i< numberCams; i++)
            {
                allcams.Add(baseMat);
            }
            
        }
        for (int i=0; i< numberCams; i++) //cams is how many messages we sent
        {
            
            //unwrap the messages
            var cams = message.GetBytes();
            Texture2D tex = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            tex.LoadRawTextureData(cams);
            tex.Apply();
            //apply it to the object
            allcams[i].mainTexture = tex;
        }

        //for now apply to object?   

    }
     #endregion */
    private void Update() {
        if (hasNumber && !runOnce)
        {
            //this means we ran it at least once
            SetCamera();
            runOnce = true;
            Debug.Log("Changing!");
        }
    }
     void SetCamera()
     {
        var materialsCopy = cameraView.GetComponent<MeshRenderer>().materials;
        //change depending on the active camera
        materialsCopy[1] = allcams[0]; //2ND MATERIAL FOR THIS CONTEXT
        cameraView.GetComponent<MeshRenderer>().materials = materialsCopy;
     }
}
