using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePuzzle : MonoBehaviour
{
    float[] rotations = { 0, 90, 180, 270 }; //The random rotations the pipes can start off


    //checks if correctly solved
    public float solution;
    bool isPlaced = false;

    private void Start()
    {
        int startposition = Random.Range(0, rotations.Length); //Picks between the range of rotations
        transform.eulerAngles = new Vector3(0, 0, rotations[startposition]);//Rotates them from the picked rotation randomly
        if (Mathf.Round(transform.eulerAngles.z) == solution)
        {
            isPlaced = true;
        }

    }

    private void OnMouseDown()
    {
        transform.Rotate(new Vector3(0, 0, 90)); //When click on a pipe, would rotate it in 90 degrees

        if (Mathf.Round(transform.eulerAngles.z) == solution && isPlaced == false)
        {
            isPlaced = true;
        }
        else if (isPlaced == true)
        {
            isPlaced = false;
        }
    }
}
