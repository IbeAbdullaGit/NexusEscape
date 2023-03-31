using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoor : Interactable
{
    // Start is called before the first frame update

    public bool isButton;
    Animator anim;
    public Renderer render;
    Color original;
    public Door activateDoor;

    private FMODUnity.EventReference buttonEvent;
    public override void OnInteract()
    {
        if (isButton)
        {
            anim.Play("Armature|Press");
            anim.SetTrigger("press");
            activateDoor.OpenDoor();
            FMODUnity.RuntimeManager.PlayOneShotAttached(buttonEvent, gameObject);

        }
    }

    void Start()
    {
        
        if (isButton)
        {
            anim = GetComponent<Animator>();
            original = render.material.color;
            //SOUND STUFF
            buttonEvent.Path = "event:/Sound Effects/Interactions/ButtonPress";
            buttonEvent.Guid = new FMOD.GUID(new System.Guid("{cddf1de5-b51e-4239-8159-157ec49145d7}"));
        }
    }

    // Update is called once per frame
    void Update()
    {
       if(activateDoor.isOpen == false)
        {
            render.material.color = Color.green;
        }

       if(activateDoor.isOpen == true)
        {
            render.material.color = original;
        }
    }
}
