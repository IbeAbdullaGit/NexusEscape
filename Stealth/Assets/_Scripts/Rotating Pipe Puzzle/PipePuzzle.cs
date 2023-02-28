using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PipePuzzle : MonoBehaviour
{
    float[] rotations = {0, 90, 180, 270 }; //The random rotations the pipes can start off

    RectTransform rotateimage;

    [SerializeField] private Button pipe = null;
    [SerializeField] PipeManager PipeManagers;
    //checks if correctly solved
    public float [] solution; //Make an array to make sure straight pipes have the correct solution in both rotations
    [SerializeField]
    bool isPlaced = false;
    int PossSolutions = 1;
    bool solved = false;
    PipeManager pipeManager;
    Canvas canvas;

    //For Sprite chaniging
    public Image original;
    public Sprite newcolor;
    public Sprite wrongcolor;
    public GameObject[] PipeConnected;

    private void Awake()
    {
        pipeManager = GameObject.Find("PipeManager").GetComponent<PipeManager>();
        pipe.onClick.AddListener(ParameterOnClick);
        if (!isPlaced)
        {
            int startposition = Random.Range(0, rotations.Length); //Picks between the range of rotations
            transform.eulerAngles = new Vector3(0, 0, rotations[startposition]);//Rotates them from the picked rotation randomly
        }
    }

    private void Start()
    {
        //Allows change the size of possible solutions of each pipe
        PossSolutions = solution.Length;
       

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

 

    private void ParameterOnClick()
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

    private void RemoveButtons()
    {
        pipe.onClick.RemoveAllListeners();
        Button.Destroy(pipe);
        
    }

    private void Update()
    {
        if (pipeManager.totalPipes == pipeManager.correctPipes && !solved)
        {
            solved = true;
            RemoveButtons();
           pipeManager.TriggerDoor();
           pipeManager._otherdoor.GetComponent<Door>().isOpen = false;
           pipeManager._otherdoor.GetComponent<Door>().OpenDoor();
           Destroy(transform.gameObject.GetComponentInParent<Canvas>().gameObject);
 
        }
    }
}
