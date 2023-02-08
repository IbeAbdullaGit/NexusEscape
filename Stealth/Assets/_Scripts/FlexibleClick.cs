using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlexibleClick : Interactable
{
    public UnityEvent eventToTrigger;
    Animator anim;
    SoundManager soundInstance;


    public override void OnInteract()
    {
        
        //plays the given event/function
        eventToTrigger.Invoke();

        //play animation, for buttons
        anim.Play("Armature|Press");
        anim.SetTrigger("press");
        //play sound
        //soundInstance.PlaySound(SoundManager.Sound.ButtonPress);

    }
    private void Start() {
        anim = GetComponent<Animator>();
        soundInstance = GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundManager>().instance;
    }
}
