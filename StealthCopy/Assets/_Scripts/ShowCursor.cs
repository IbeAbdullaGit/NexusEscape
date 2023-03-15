using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCursor : Interactable
{
    

    // Start is called before the first frame update
    public override void OnInteract()
    {
  
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        

       
    }

    public void TurnOff()
    {

        //turn off cursor
        if (Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }


}
