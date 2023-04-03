using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Riptide;

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

    private FMODUnity.EventReference buttonEvent;
    private FMODUnity.EventReference buttonpositiveEvent;

    public override void OnInteract()
    {
        if (!spawning)
        {     //play animation
            if (isButton)
            {
                anim.Play("Armature|Press");
                anim.SetTrigger("press");
                if (hasDistraction)
                {
                    FMODUnity.RuntimeManager.PlayOneShotAttached(buttonpositiveEvent, gameObject);
                }
                else
                {
                    FMODUnity.RuntimeManager.PlayOneShotAttached(buttonEvent, gameObject);
                }
            }
            
            StartCoroutine(Spawn());
            
        }
    }
    public void RemoteInteract(int n)
    {
        StartCoroutine(Spawn2(n));
    }
    IEnumerator Spawn()
    {
        //we are spawning rn
        spawning = true;

        if (hasDistraction)
        {
            currentDistraction = cameras.GetCurrentCamera().GetComponent<CameraSettings>().distraction;
            if (!currentDistraction.gameObject.activeSelf)
                currentDistraction.gameObject.SetActive(true);
            //also start on server side
            //we need to send current camera
            Message message = Message.Create(MessageSendMode.Reliable, ClientToServerId.distraction);
            //send index of which camera this is
            message.AddInt(cameras.GetCameraIndex());
            //send message
            NetworkManagerClient.Singleton.Client.Send(message);
            Debug.Log("Sending message");
            //check if the distraction is enabled

            //now, start the cooldown
            yield return cooldown;
        }
        //otherwise do nothing
        //after cooldown, allow spawning again
        spawning = false;
    }
    IEnumerator Spawn2(int n)
    {
        //we are spawning rn
        spawning = true;

        //if (hasDistraction) //dont need to check ehre since we will know
        {
            currentDistraction = cameras.GetCameraFromIndex(n).GetComponent<CameraSettings>().distraction;
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

        //SOUND STUFF
        buttonEvent.Path = "event:/Sound Effects/Interactions/ButtonPress";
        buttonEvent.Guid = new FMOD.GUID(new System.Guid("{cddf1de5-b51e-4239-8159-157ec49145d7}"));

        buttonpositiveEvent.Path = "event:/Sound Effects/Interactions/ButtonPressPos";
        buttonpositiveEvent.Guid = new FMOD.GUID(new System.Guid("{ec62b5c4-693e-4fc8-aa36-5bf61805a2e1}"));
    }
    private void Update() {
        
        //Debug.Log(cameras.GetCurrentCamera().name);
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
