using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTyper : Interactable
{
    public GameObject typer;

    public bool isButton = false;

    public int puzzle_type; //0 = text, 1 = door, FOR NOW

     Animator anim;
    public Renderer render;
    private FMODUnity.EventReference buttonEvent;


    public override void OnInteract()
    {
        //typer.GetComponent<Typer>().ResetTyping();

        if (isButton)
        {
            //play button animation
            //play animation
            anim.Play("Armature|Press");
            anim.SetTrigger("press");
            //play sound
            //soundInstance.PlaySound(SoundManager.Sound.ButtonPress);
            FMODUnity.RuntimeManager.PlayOneShotAttached(buttonEvent, gameObject);

        }
        //render.material.color = Color.red;
        //makes all the "answers" false
        typer.GetComponent<LinkedPuzzle>().ZeroEverything();
        //now set specific answer
        typer.GetComponent<LinkedPuzzle>().amount_linked[puzzle_type] = true;

        typer.GetComponent<Typer>().typerActive = true; //set flag

        //activate typer
        typer.GetComponent<Typer>().ResetTyping();

        //send message
        if (puzzle_type == 0)
        {
            //text
            if (InteractionMessages.Singleton != null)
                InteractionMessages.Singleton.TyperInteract();
            if (InteractionServerNexus1.Singleton != null)
                InteractionServerNexus1.Singleton.TyperInteract();
        }
        else //should equal 1
        {
            //door
            if (InteractionMessages.Singleton != null)
                InteractionMessages.Singleton.TyperInteract2();
            if (InteractionServerNexus1.Singleton != null)
                InteractionServerNexus1.Singleton.TyperInteract2();
            Debug.Log("sent typer message");
        }

    }

    private void Start() {
        if (isButton)
        {
            anim = GetComponent<Animator>();
            buttonEvent.Path = "event:/Sound Effects/Interactions/ButtonPress";
            buttonEvent.Guid = new FMOD.GUID(new System.Guid("{cddf1de5-b51e-4239-8159-157ec49145d7}"));
            //render.material.color = Color.green;
        }

    }
}
