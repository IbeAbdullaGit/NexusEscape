using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))] //makes it so this will always have box collider so it can be clicked
public abstract class Interactable : MonoBehaviour
{
  
  private void OnMouseEnter() {
        MouseControl.instance.Clickable();
    }

    private void OnMouseExit() {
        MouseControl.instance.Default();
    }
  public virtual void Awake()
  {
    gameObject.layer = 10;
  }
  
   public abstract void OnInteract();
   public virtual void OnFocus()
   {

   }
   public virtual void OnLoseFocus()
   {

   }
}
