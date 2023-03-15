using System.Collections.Generic;
using UnityEngine;

// (1/2 of Observer Design Pattern)
//Subject is the object that Observer objects are "watching/waiting" for something to happen
//Serves as the class for other subject classes to derive from
//--------------
//When they 
//When the subject that they're attached to notify them, then they execute their functions in OnNotify
// - Nate

public abstract class Subject : MonoBehaviour
{
    List<Observer> _observers = new List<Observer>();

    public void AddObserver(Observer observer)
    {
        _observers.Add(observer);
    }
    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.OnNotify();
        }
    }

    
}