using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//https://www.youtube.com/watch?v=VaDhk2eOQXM

public class PopUpSystem : MonoBehaviour
{
    public GameObject popUpBox;
    public Animator animator;
    public TMP_Text popUpText;

    public void PopUp(string text)
        {
            popUpBox.SetActive(true);
            popUpText.text = text;
            //animator.SetTrigger("pop");
        }
    
}
