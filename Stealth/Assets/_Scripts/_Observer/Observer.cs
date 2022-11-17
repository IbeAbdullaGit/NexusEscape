using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Observer
{
    public abstract void OnNotify();
}

public class Items : Observer
{
    GameObject PickupObjs;
    ItemPickup Pickup;
    public Items(GameObject PickupObjs, ItemPickup Pickup)
    {
        this.PickupObjs = PickupObjs;
        this.Pickup = Pickup;
    }

    public override void OnNotify()
    {
       ItemCheck(Pickup.ItemPickupCheck());
    }

    void ItemCheck( bool check)
    {
        
        if (PickupObjs)
        {
            PickupObjs.GetComponent<Rigidbody>().isKinematic = check;
        }
    }
}







