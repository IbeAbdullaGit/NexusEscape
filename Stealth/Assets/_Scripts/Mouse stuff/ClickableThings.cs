using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))] //makes it so this will always have box collider so it can be clicked
public class ClickableThings : MonoBehaviour
{
    private void OnMouseEnter() {
        MouseControl.instance.Clickable();
    }

    private void OnMouseExit() {
        MouseControl.instance.Default();
    }
}
