using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Reciever

public class Door : Observer
{
    public bool isOpen = false;
    bool lerping = false;

    private static bool willSpawn = false;

    [Tooltip("The Y positional distance between the current position and the end position.")]
    public float openOffset = 14f; //The offset on the Y axis to have the door open

    private Vector3 openPosition;
    private Vector3 closedPosition;
    public float duration = 3f;
    private float elapsedTime;
    private float percentComplete;

    //public GameObject soundObject;
    public bool puzzle_complete = false;

    private FMOD.Studio.EventInstance doorsoundInstance;
    private FMODUnity.EventReference doorEvent;
    private void Start()
    {
        closedPosition = transform.position;
        openPosition = new Vector3(closedPosition.x, closedPosition.y + openOffset, closedPosition.z);
        //soundObject = gameObject.transform.Find("DoorSound").gameObject; //Get the sound gameobject -- DEPRECATED BY NATE

        //SOUND STUFF
        //doorEvent.Path = "event:/Sound Effects/Interactions/DoorMove";
        //doorEvent.Guid = new FMOD.GUID(new System.Guid("{97cda655-00e4-4cfb-a183-076ee2849f8f}")); //set GUID, all of this hard work is so abdu doesn't gotta redo anything <3
        doorEvent = FMODUnity.RuntimeManager.PathToEventReference("event:/Sound Effects/Interactions/DoorMove");
        doorsoundInstance = FMODUnity.RuntimeManager.CreateInstance(doorEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(doorsoundInstance, gameObject.transform);

        //OpenDoor(); //debug
    }
    public void SetSpawnAgain()
    {
        willSpawn = true;
    }
    private void Awake()
    {
        
        if (willSpawn)
        {
            //will spawn again
            DestroyImmediate(gameObject); //dont need second game copy
        }
    }

    private void Update()
    {
        //OpenDoor();
        if (lerping == true)
        {
            lerpDoor();
        }
        else
        {
            //soundObject.SetActive(false);
            
        }
        //Debug.Log(isOpen);
    }

    public override void OnNotify()
    {
        OpenDoor();
    }
    public void ToggleDoor()
    {
        if (isOpen == false)
        {
            OpenDoor();

        }
        else if (isOpen == true)
        {
            CloseDoor();
        }
    }

    public void OpenDoor()
    {
        if (isOpen == false)
        {
            doorsoundInstance.setParameterByName("EndLoop", 0f);
            doorsoundInstance.setTimelinePosition(0);
            doorsoundInstance.start();

            lerping = true;
            elapsedTime = 0;
        }
        isOpen = true;
    }

    public void CloseDoor()
    {
        if (isOpen == true)
        {
            doorsoundInstance.setParameterByName("EndLoop", 0f);
            doorsoundInstance.setTimelinePosition(0);
            doorsoundInstance.start();

            lerping = true;
            elapsedTime = duration;
        }
        isOpen = false;
    }


    public void lerpDoor()
    {
        if (isOpen) //If the door is open go the opposite way
        {
            elapsedTime += Time.deltaTime;
        }
        else
        {
            elapsedTime -= Time.deltaTime;
        }

        percentComplete = elapsedTime / duration;

        transform.position = Vector3.Lerp(closedPosition, openPosition, percentComplete);

        if (percentComplete >= 1 || percentComplete < 0)
        {
            doorsoundInstance.setParameterByName("EndLoop", 1f);
            lerping = false;
        }
    }

}
