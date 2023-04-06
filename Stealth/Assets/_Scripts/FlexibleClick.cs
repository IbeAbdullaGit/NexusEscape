using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlexibleClick : Interactable
{
    public UnityEvent eventToTrigger;
    Animator anim;
    //SoundManager soundInstance;
    private FMODUnity.EventReference buttonEvent;

    public override void OnInteract()
    {
        
        //plays the given event/function
        eventToTrigger.Invoke();

        //play animation, for buttons
        anim.Play("Armature|Press");
        anim.SetTrigger("press");
        //play sound
        //soundInstance.PlaySound(SoundManager.Sound.ButtonPress);
        FMODUnity.RuntimeManager.PlayOneShotAttached(buttonEvent, gameObject);

    }
    private void Start() {
        anim = GetComponent<Animator>();
        //soundInstance = GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundManager>().instance;

        //SOUND STUFF
        //buttonEvent.Path = "event:/Sound Effects/Interactions/ButtonPress";
        //buttonEvent.Guid = new FMOD.GUID(new System.Guid("{cddf1de5-b51e-4239-8159-157ec49145d7}"));
        buttonEvent = FMODUnity.RuntimeManager.PathToEventReference("event:/Sound Effects/Interactions/ButtonPress");
    }
}
