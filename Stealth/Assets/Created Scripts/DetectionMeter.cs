using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionMeter : MonoBehaviour
{
    //Creating Instance just incase
    public static DetectionMeter instance;

    public GameObject FieldofViewTest;
    FieldOfView test;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        test = FieldofViewTest.GetComponent<FieldOfView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Meter()
    {
        if (test.canSeePlayer == true)
        {
            float delay = 2.7f;
            WaitForSeconds wait = new WaitForSeconds(delay);
            Debug.Log("1 second rolling");
        }
    }
}
