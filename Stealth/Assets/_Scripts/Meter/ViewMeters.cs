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
        //menuUI.enabled = !menuUI.enabled;
        openMenu = !openMenu;
        if (openMenu)
            menuUI.gameObject.SetActive(true);
        else
            menuUI.gameObject.SetActive(false);
        
        Debug.Log("Changing UI");
    }
    // Start is called before the first frame update
    void Start()
    {
        //disable at start
        //menuUI.enabled = false;
        menuUI.gameObject.SetActive(false);
        if (NetworkManagerServer.Singleton !=null)
        {
            menuUI.gameObject.SetActive(true); //we want it to be enabled
            //but inivislbe
            menuUI.GetComponent<CanvasRenderer>().cull = false;
            menuUI.GetComponent<Renderer>().enabled = false;
            menuUI.scaleFactor = 0;
        }
        anim = GetComponent<Animator>();
        openMenu = false;
        //soundInstance = GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundManager>().instance;

        //SOUND STUFF
        buttonEvent.Path = "event:/Sound Effects/Interactions/ButtonPress";
        buttonEvent.Guid = new FMOD.GUID(new System.Guid("{cddf1de5-b51e-4239-8159-157ec49145d7}"));
    }

    
}
