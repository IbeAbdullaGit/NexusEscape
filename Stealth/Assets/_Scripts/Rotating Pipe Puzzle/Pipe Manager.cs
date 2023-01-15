using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    //Pipes and where they are stored
    public GameObject PipeHolder; 
    public GameObject[] Pipes;
   
    [SerializeField]
    int totalPipes = 0;

    int correctPipes = 0;

    // Start is called before the first frame update
    void Start()
    {
        totalPipes = PipeHolder.transform.childCount; //Get amount of Pipes

        Pipes = new GameObject[totalPipes];


        //Arranging the Pipes in the Pipes array
        for(int i = 0; i < Pipes.Length; i++)
        {
            Pipes[i] = PipeHolder.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    public void correctposition()
    {
        correctPipes += 1;

        if(correctPipes == totalPipes)
        {
            //Open Door
        }
    }

    public void wrongposition()
    {
        correctPipes -= 1;
    }
}
