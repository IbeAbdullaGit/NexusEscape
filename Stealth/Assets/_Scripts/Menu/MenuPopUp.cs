using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuPopUp : MonoBehaviour
{
    public Canvas subMenu;
    private void Start() {
        subMenu.enabled = false;
    }

    public void ChangeMenu()
    {
        subMenu.enabled = true;
    }

    public void CloseOptions()
    {
        subMenu.enabled = false;
    }
}
