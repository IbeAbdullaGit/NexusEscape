using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterManager : MonoBehaviour
{
    
    public Slider[] sliders;
    [Range(0, 10)]
    public int[] targetValues =new int[4];

    public bool[] correct = new bool[4];

    
    // Start is called before the first frame update
    void Start()
    {
        //initialize all to false
        for (int i=0; i< correct.Length; i++)
        {
            correct[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        for (int i=0; i<sliders.Length; i++)
        {
            if (CheckAnswer(i))
                correct[i] = true;
            else
                correct[i] = false;
        }

        //what do we do if correct?
    }

    bool CheckAnswer(int n) //n is the button number we're checking
    {

        //compare specific values
        if (sliders[n].value != targetValues[n])
            return false;

        //all values matched
        return true;
    }
}
