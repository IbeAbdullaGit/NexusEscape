using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemPickup
{
    public abstract bool ItemPickupCheck();
}

public class Pickup : ItemPickup
{
    public override bool ItemPickupCheck()
    {
        
        return true;
        
    }
}

public class Drop : ItemPickup
{
    public override bool ItemPickupCheck()
    {
     
        return false;
        
    }
}
