using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPuzzlePiece : Interactable
{
    public int id;
    public int place;
    SlidingPuzzle manager;
    bool canMove = true;
    public override void OnInteract()
    {
       if (canMove)
      //tell the manager that we hit something, and send in what we hit
        {
            manager.CheckHit(name);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        manager = gameObject.GetComponentInParent<SlidingPuzzle>();
    }
    public void Interactable(bool val)
    {
        canMove = val;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
