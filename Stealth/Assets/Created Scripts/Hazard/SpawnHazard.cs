using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHazard : Interactable
{
    GameObject currentHazard;
    public GameObject prefab;

    public Vector3 position;
    public override void OnFocus()
    {
        //Debug.Log("looking at");
        
       
        //perhaps highlight it
    }
    public override void OnInteract()
    {
        //Debug.Log("Changing UI");
        
        //spawn the hazard
        currentHazard = Instantiate(prefab);
        
        //set the location
        currentHazard.transform.position = this.position;


    }
    public override void OnLoseFocus()
    {
        //Debug.Log("looking away");
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //for testing purposes
        if (Input.GetKeyDown(KeyCode.Q)) //could change this key
            {
                OnInteract();
            }
    }
}
