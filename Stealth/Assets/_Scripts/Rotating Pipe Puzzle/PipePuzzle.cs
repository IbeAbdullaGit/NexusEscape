using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePuzzle : MonoBehaviour
{
    float[] rotations = { 0, 90, 180, 270 }; //The random rotations the pipes can start off


    //checks if correctly solved
    public float [] solution; //Make an array to make sure straight pipes have the correct solution in both rotations
    [SerializeField]
    bool isPlaced = false;
    int PossSolutions = 1;

    PipeManager pipeManager;

    private void Awake()
    {
        pipeManager = GameObject.Find("PipeManager").GetComponent<PipeManager>();
    }

    private void Start()
    {
        //Allows change the size of possible solutions of each pipe
        PossSolutions = solution.Length;

        
        int startposition = Random.Range(0, rotations.Length); //Picks between the range of rotations
        transform.eulerAngles = new Vector3(0, 0, rotations[startposition]);//Rotates them from the picked rotation randomly

        //If Possible solutions are more than 1... only 2, checks the array of solutions if they match then the pipe is correctly placed 
        if (PossSolutions > 1)
        {
            if (Mathf.Round(transform.eulerAngles.z) == solution[0] || Mathf.Round(transform.eulerAngles.z) == solution[1])
            {
                isPlaced = true;
                pipeManager.correctposition();
            }
        }
        else
        {
            if (Mathf.Round(transform.eulerAngles.z) == solution[0])
            {
                isPlaced = true;
                pipeManager.correctposition();
            }
        }

    }

    private void OnMouseDown()
    {
        transform.Rotate(new Vector3(0, 0, 90)); //When click on a pipe, would rotate it in 90 degrees


        //If Possible solutions are more than 1... only 2, checks the array of solutions if they match then the pipe is correctly placed, if not then it will be counted as false
        if (PossSolutions > 1)
        {
            if (Mathf.Round(transform.eulerAngles.z) == solution[0] || Mathf.Round(transform.eulerAngles.z) == solution[1])
            {
                isPlaced = true;
                pipeManager.correctposition();
            }
            else if (isPlaced == true)
            {
                isPlaced = false;
                pipeManager.wrongposition();
            }
        }
        else
        {
            if (Mathf.Round(transform.eulerAngles.z) == solution[0])
            {
                isPlaced = true;
                pipeManager.correctposition();
            }
            else if (isPlaced == true)
            {
                isPlaced = false;
                pipeManager.wrongposition();
            }
        }
    }
}
