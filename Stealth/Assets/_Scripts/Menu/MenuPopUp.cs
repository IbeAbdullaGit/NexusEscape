using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuPopUp : MonoBehaviour
{
    public Canvas subMenu;

    public void ChangeMenu()
    {
        subMenu.enabled = !subMenu.enabled;
    }
}
