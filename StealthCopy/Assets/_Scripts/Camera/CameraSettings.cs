using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    public bool hasDistraction;
    public Distraction distraction;
    // Start is called before the first frame update
    void Start()
    {
        if (distraction != null)
            hasDistraction = true;
        else
            hasDistraction = false;
    }

}
