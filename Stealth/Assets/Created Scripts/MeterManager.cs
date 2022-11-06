using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterManager : MonoBehaviour
{
    
    public Slider[] sliders;
    [Range(0, 10)]
    public int[] targetValues =new int[4];

    public bool correct = false;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckAnswer())
        {
            correct = true;
        }
        else{
            correct = false;
        }
    }

    bool CheckAnswer()
    {
        //compare the values in the sliders to the values we want - note that the ordering must match
        for (int i=0; i < sliders.Length; i++)
        {
            if (sliders[i].value != targetValues[i])
            {
                //if any of the values don't match, return false
                return false;
            }
        }
        //all values matched
        return true;
    }
}
