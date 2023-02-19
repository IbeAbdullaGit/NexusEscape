using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))] //makes it so this will always have box collider so it can be clicked
public abstract class Interactable : MonoBehaviour
{
  
  public virtual void Awake()
  {
    gameObject.layer = 10;
  }
  
   public abstract void OnInteract();
   public virtual void OnFocus()
   {
      //can interact, change cursor
      CrosshairControl.instance.SetHighlighted();
   }
   public virtual void OnLoseFocus()
   {
      //change back to normal
      CrosshairControl.instance.SetNormal();
   }
}
