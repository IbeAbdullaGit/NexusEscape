using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewMeters : Interactable
{
      public bool openMenu = false;
      Animator anim;

    public Canvas menuUI;


    private FMODUnity.EventReference buttonEvent;
    //SoundManager soundInstance;
    public override void OnInteract()
    {
        //Debug.Log("Changing UI");
        ChangeUI();
        //play animation
        anim.Play("Armature|Press");
        anim.SetTrigger("press");
        //play sound
        //soundInstance.PlaySound(SoundManager.Sound.ButtonPress);
        FMODUnity.RuntimeManager.PlayOneShotAttached(buttonEvent, gameObject);

    }
    public void ChangeUI()
    {
        menuUI.enabled = !menuUI.enabled;
        openMenu = !openMenu;
        //for this one, we don't need to see the cursor
        /* if (Cursor.lockState == CursorLockMode.Locked)
        {
             Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; 
        }  
        else
        {
             Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false; 
        } */
    }
    // Start is called before the first frame update
    void Start()
    {
        //disable at start
        menuUI.enabled = false;
        anim = GetComponent<Animator>();
        //soundInstance = GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundManager>().instance;

        //SOUND STUFF
        buttonEvent.Path = "event:/Sound Effects/Interactions/ButtonPress";
        buttonEvent.Guid = new FMOD.GUID(new System.Guid("{cddf1de5-b51e-4239-8159-157ec49145d7}"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
