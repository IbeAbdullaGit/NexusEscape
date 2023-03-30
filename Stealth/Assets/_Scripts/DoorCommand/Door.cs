using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Reciever

public class Door : Observer
{
    public bool isOpen = false;
    bool lerping = false;

    [Tooltip("The Y positional distance between the current position and the end position.")]
    public float openOffset = 14f; //The offset on the Y axis to have the door open

    private Vector3 openPosition;
    private Vector3 closedPosition;
    public float duration = 3f;
    private float elapsedTime;
    private float percentComplete;

    //public GameObject soundObject;


    private FMOD.Studio.EventInstance doorsoundInstance;
    private FMODUnity.EventReference doorEvent;
    private void Start()
    {
        closedPosition = transform.position;
        openPosition = new Vector3(closedPosition.x, closedPosition.y + openOffset, closedPosition.z);
        //soundObject = gameObject.transform.Find("DoorSound").gameObject; //Get the sound gameobject -- DEPRECATED BY NATE

        //SOUND STUFF
        doorEvent.Path = "event:/Sound Effects/Interactions/DoorMove";
        doorEvent.Guid = new FMOD.GUID(new System.Guid("{97cda655-00e4-4cfb-a183-076ee2849f8f}")); //set GUID, all of this hard work is so abdu doesn't gotta redo anything <3
        doorsoundInstance = FMODUnity.RuntimeManager.CreateInstance(doorEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(doorsoundInstance, gameObject.transform);

        //OpenDoor(); //debug
    }

    private void Update()
    {

        if (lerping == true)
        {
            lerpDoor();
        }
        else
        {
            //soundObject.SetActive(false);
            
        }
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
    }


    public void lerpDoor()
    {
        if (!isOpen) //If the door is open go the opposite way
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
            isOpen = !isOpen;
        }
    }

}
