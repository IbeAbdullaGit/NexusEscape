using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDistraction : MonoBehaviour
{
     GameObject currentHazard;
    public GameObject prefab;

    public Vector3 position;

    public bool UsePooling = true;

    public void OnFocus()
    {
        //Debug.Log("looking at");
        
       
        //perhaps highlight it
    }
    public  void OnInteract()
    {
        //Debug.Log("Changing UI");


        if (UsePooling)
        {
            currentHazard = ObjectPooler.instance.SpawnFromPool("Distraction", this.position, new Quaternion(0f, 0f, 0f, 0f));
        }
        else
        {
            //spawn the hazard
            currentHazard = Instantiate(prefab);

            //set the location
            currentHazard.transform.position = this.position;
        }


    }
    public void OnLoseFocus()
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
