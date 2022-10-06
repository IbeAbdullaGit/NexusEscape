using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy2 : MonoBehaviour
{
    public abstract string Name {get;}

    public abstract GameObject Create(GameObject prefab); 
}

public class Crab : Enemy2
{
    public override string Name => "crab";
    //public override string Name{get {return "crab";}}

    public override GameObject Create(GameObject prefab)
    {
        GameObject enemy = Instantiate(prefab);
        Debug.Log("Crab enemy is created");
        return enemy;
    }
}

public class Monster : Enemy2
{
    public override string Name => "monster";
    public override GameObject Create(GameObject prefab)
    {
        GameObject enemy = Instantiate(prefab);
        Debug.Log("Monster enemy is created");
        return enemy;
    }
}
