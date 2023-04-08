using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSubmit : Interactable
{
     Inventory inventory;

     //for where we will put the tiles
     public Transform[] positions;
     int counter =0;
     //hold our references
     List<TilePickUp> currRefs = new List<TilePickUp>();

     //answer
     public int[] answer = new int[3];
     private int[] currAnswer = new int[3];

     public Door activateDoor;

     //hold reference to whatever is occupied
     bool[] occupied = {false, false, false};
    public override void OnInteract()
    {
        Debug.Log("We are interacting");
        //make sure we have a tile we're holding
        if (inventory.currentTile != null)
        {
            //helps us exit early
            bool early_exit = false;
            //make sure we have room for this
            for (int i=0; i< occupied.Length; i++)
            {
                if (!occupied[i] && !early_exit) //prioritize left to right
                {
                    //Debug.Log("We inside the statement");
                    //settings for being in place with the submission
                    currRefs.Add(inventory.currentTile);
                    //Debug.Log(currRefs.Count);
                    currRefs[currRefs.Count-1].SubmitTile();
                    //currRef.SubmitTile();
                    //change position, based on what is available rn
                    currRefs[currRefs.Count-1].transform.position = positions[i].position;
                    //change rotation so it looks nice
                    //depends on level
                    if (InteractionMessages.Singleton !=null)
                        currRefs[currRefs.Count-1].transform.rotation = Quaternion.Euler(90.0f, 180.0f, 0.0f);
                    else if (InteractionServerNexus1.Singleton !=null)
                        currRefs[currRefs.Count-1].transform.rotation = Quaternion.Euler(90.0f, -90.0f, 0.0f);
                    //now set this position to be occupied
                    occupied[i] = true;
                    //save what this position is for this tile
                    currRefs[currRefs.Count-1].tile_pos = i;
                    counter +=1;
                    //leave now, don't need to add more
                    early_exit = true;
                }
            }
            
        }

    }
    
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<Inventory>();
        //Debug.Log("Is this being started?");
    }
    void ArrangeAnswer()
    {
        //the current references may not be in order, so ansemble in order for checking the answer
        for (int i=0; i< currRefs.Count; i++)
        {
            //right now, manually doing it for each one since we only have 3.
            if (currRefs[i].tile_pos == 0)
            {
                currAnswer[0] = currRefs[i].id;
            }
            else if (currRefs[i].tile_pos == 1)
            {
                currAnswer[1] = currRefs[i].id;
            }
             else if (currRefs[i].tile_pos == 2)
            {
                currAnswer[2] = currRefs[i].id;
            }
        }

    }
    bool AnswerCheck()
    {
        for (int i=0; i< answer.Length; i++)
        {
            //does the id match, if not we know answer isnt right
            if (currAnswer[i] != answer[i])
            {
                return false;
            }
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(occupied[0] + ", " + occupied[1] + ", " + occupied[2]);
        //check if our references are in place; should have at least 1, and is anything being held
        if (currRefs.Count >=1 && inventory.currentTile)
        {
            for (int i=0; i < currRefs.Count; i++)
            {
                //check if each one is in place - if its been moved, it will be false   
                if (!currRefs[i].inPlace)
                {
                    //free up the spot, GOES BEFORE DUE TO LOGIC
                    occupied[currRefs[i].tile_pos] = false;
                    //remove from this spot
                    currRefs.RemoveAt(i);
                    //decrease counter
                    counter -=1;
                }
            }
        }
        //check for answer, when we are holding 3 references
        if (currRefs.Count >=3)
        {
            ArrangeAnswer();
            if (AnswerCheck())
            {
                //do something
                activateDoor.ToggleDoor();
            }
        }
        
    }
}
