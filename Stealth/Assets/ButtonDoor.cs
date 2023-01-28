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
    public override void OnInteract()
    {
        if (isButton)
        {
            anim.Play("Armature|Press");
            anim.SetTrigger("press");
            activateDoor.OpenDoor();
        }
    }

    void Start()
    {
        
        if (isButton)
        {
            anim = GetComponent<Animator>();
            original = render.material.color;
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
