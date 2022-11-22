using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDistraction : Interactable
{
     GameObject currentHazard;
    public GameObject prefab;

    Vector3 position;

    public bool UsePooling = true;

    CameraMenu cameras;

    public float distance = 10.0f;

    WaitForSeconds cooldown = new WaitForSeconds(10.0f);

    bool spawning = false;

    public override void OnInteract()
    {
        if (!spawning)
            StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        //we are spawning rn
        spawning = true;
        //current camera
        position = cameras.GetCurrentCamera().transform.position;
        //the actual position should be offset a bit
        position += cameras.GetCurrentCamera().transform.forward * distance;
        //move the y so it's not floating in the air, could change
        position.y = 5.0f;
        
        if (UsePooling)
        {
            currentHazard = ObjectPooler.instance.SpawnFromPool("Distraction", position, new Quaternion(0f, 0f, 0f, 0f));
        }
        else
        {
            //spawn the hazard
            currentHazard = Instantiate(prefab);

            //set the location
            currentHazard.transform.position = position;
        }

        //now, start the cooldown
        yield return cooldown;
        
        //after cooldown, allow spawning again
        spawning = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        cameras = GameObject.FindGameObjectWithTag("GameController").GetComponent<CameraMenu>();
    }

   /*  // Update is called once per frame
    void Update()
    {
        //for testing purposes
        if (Input.GetKeyDown(KeyCode.Q)) //could change this key
            {
                OnInteract();
            }
    } */
}