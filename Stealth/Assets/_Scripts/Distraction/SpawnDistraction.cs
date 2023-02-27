using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDistraction : Interactable
{
     GameObject currentHazard;
    public GameObject prefab;

    Vector3 position;

    public bool UsePooling;

    //CameraMenu cameras;

    public float distance = 10.0f;

    WaitForSeconds cooldown = new WaitForSeconds(10.0f);

    bool spawning = false;

    Animator anim;
    public bool isButton;

    Distraction currentDistraction;

    bool hasDistraction = false;

    //change color
    public Renderer render;
    Color original;

    public CameraMenu cameras;

    public override void OnInteract()
    { 
        if (!spawning)
        {     //play animation
            if (isButton)
            {
                anim.Play("Armature|Press");
                anim.SetTrigger("press");
            }
            StartCoroutine(Spawn());
        }
    }
    IEnumerator Spawn()
    {
        //we are spawning rn
        spawning = true;

        if (hasDistraction)
        {
            currentDistraction = cameras.GetCurrentCamera().GetComponent<CameraSettings>().distraction;
            //check if the distraction is enabled
            if (!currentDistraction.gameObject.activeSelf)
                
                currentDistraction.gameObject.SetActive(true);

            //now, start the cooldown
            yield return cooldown;
        }
        //otherwise do nothing
        //after cooldown, allow spawning again
        spawning = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Camera menu: " + cameras.name);
        //cameras = GameObject.FindGameObjectWithTag("GameController").GetComponent<CameraMenu>();
        if (isButton)
        {
            anim = GetComponent<Animator>();
            original = render.material.color;
        }
        //Debug.Log("Camera menu: " + cameras.name);
    }
    private void Update() {
        
        Debug.Log(cameras.GetCurrentCamera().name);
        if (cameras.GetCurrentCamera().GetComponent<CameraSettings>().hasDistraction)
        {
            hasDistraction = true;
            if (isButton)
                render.material.color = Color.green;
        }
        else
        {
            hasDistraction = false;
            if (isButton)
                render.material.color = original;
        }

    }

}
