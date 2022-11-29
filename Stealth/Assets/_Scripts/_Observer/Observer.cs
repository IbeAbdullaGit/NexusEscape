using System.Collections.Generic;
using UnityEngine;

// (1/2 of Observer Design Pattern)
//Observer serves as a Abstract (template/base) class for other Observers to derive from
//--------------
//Observers are basically just the stuff waiting for something else to happen.
//When the subject that they're attached to notify them, then they execute their functions in OnNotify
// - Nate
public abstract class Observer : MonoBehaviour
{
    public abstract void OnNotify();
}