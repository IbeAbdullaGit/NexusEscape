using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PipeManager : MonoBehaviour
{
    //Pipes and where they are stored
    public GameObject PipeHolder; 
    public GameObject[] Pipes;

    [SerializeField]
    public int totalPipes = 0;
    [SerializeField]
    public int correctPipes = 0;

    public GameObject _otherdoor;
    DoorInvoker _doorInvoker;

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

        _doorInvoker = new DoorInvoker();
    }

    public void TriggerDoor()
    {
        ICommand openDoor = new ToggleDoorCommand(_otherdoor.GetComponent<Door>());
        _doorInvoker.AddCommand(openDoor);
    }

    // Update is called once per frame
    public void correctposition()
    {
        correctPipes += 1;

        if(correctPipes == totalPipes)
        {
            Debug.LogError("You did it!");
            //Open Door
        }   
    }

    public void wrongposition()
    {
        correctPipes -= 1;
    }
}
