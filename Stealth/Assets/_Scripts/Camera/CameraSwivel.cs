using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwivel : MonoBehaviour
{
    //how much to swivel
    public float amount;
    Transform initial;

    float currOffset = 0;
    bool moveRight = true;

    public float speed = 2.0f;


    public string axis;


    // Start is called before the first frame update
    void Start()
    {
        this.transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3 (0, -amount / 2, 0));
        initial = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }
    void RotateCamera()
    {
        Vector3 newRot;
        
        switch (axis)
        {
            case "x":
                newRot = transform.eulerAngles + new Vector3(currOffset, 0, 0);
                break;
            case "y":
                newRot = transform.eulerAngles + new Vector3(0, currOffset, 0);
                break;
            case "z":
                newRot = transform.eulerAngles + new Vector3(0, 0, currOffset);
                break;
            default:
                newRot = Vector3.zero;
                break;
        }
        
        if (currOffset < amount && moveRight)
        {
            //adding, moving in direction until we hit the stated amount
            //Vector3 newRot = transform.eulerAngles + new Vector3(0, currOffset, 0);
            //add
            currOffset += 0.1f;
             //transform.eulerAngles = Vector3.Lerp(newRot, transform.eulerAngles, Time.deltaTime*speed);
            //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, newRot, Time.deltaTime*speed);
        }
        else if (currOffset >= amount)
        {
            moveRight = false;
        }
        if (currOffset > -amount && !moveRight)
        {
            //move in the opposite direction
            //Vector3 newRot = transform.eulerAngles + new Vector3(0, currOffset, 0);
            //subtract
            currOffset -=0.1f;
            //transform.eulerAngles = Vector3.Lerp(newRot, transform.eulerAngles, Time.deltaTime*speed);
            

        }
        else if (currOffset <= - amount)
        {
            moveRight = true;
        }

        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, newRot, Time.deltaTime*speed);
       
    }
}
