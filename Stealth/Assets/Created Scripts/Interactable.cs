using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
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
