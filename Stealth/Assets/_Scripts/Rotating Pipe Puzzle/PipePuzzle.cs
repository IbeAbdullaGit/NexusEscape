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
    public float [] triggerincorrect;
    [SerializeField]
    bool isPlaced = false;
    public bool placedincorrect = false;
    public bool dontswitch;
    int PossSolutions = 1;
    int PossInccorect = 1;
    bool solved = false;
    PipeManager pipeManager;
    Canvas canvas;

    //For Sprite chaniging
    public Image original;
    public Sprite newcolor;
    public Sprite wrongcolor;
    public Sprite theogsprite;
    public GameObject[] PipeConnected;
   


    private void Awake()
    {
        pipeManager = GameObject.Find("PipeManager").GetComponent<PipeManager>();
        pipe.onClick.AddListener(ParameterOnClick);
        if (!isPlaced && !dontswitch)
        {
            int startposition = Random.Range(0, rotations.Length); //Picks between the range of rotations
            transform.eulerAngles = new Vector3(0, 0, rotations[startposition]);//Rotates them from the picked rotation randomly
        }
    }

    private void Start()
    {
        //Allows change the size of possible solutions of each pipe
        PossSolutions = solution.Length;
        PossInccorect = triggerincorrect.Length;
        //If Possible solutions are more than 2...checks the array of solutions if they match then the pipe is correctly placed
        if (PossSolutions > 2)
        {
            if (Mathf.Round(transform.eulerAngles.z) == solution[0] || Mathf.Round(transform.eulerAngles.z) == solution[1] || Mathf.Round(transform.eulerAngles.z) == solution[2] || Mathf.Round(transform.eulerAngles.z) == solution[3])
            {
                isPlaced = true;
            
                 pipeManager.correctposition();
                 placedincorrect = false;
                


            }
        }
        //If Possible solutions are more than 1... only 2, checks the array of solutions if they match then the pipe is correctly placed 
        else if (PossSolutions > 1 && PossSolutions <=3)
        {
            if (Mathf.Round(transform.eulerAngles.z) == solution[0] || Mathf.Round(transform.eulerAngles.z) == solution[1])
            {
                isPlaced = true;
                pipeManager.correctposition();
                placedincorrect = false;

                
            }
        }
        else
        {
            if (Mathf.Round(transform.eulerAngles.z) == solution[0])
            {
                isPlaced = true;
                pipeManager.correctposition();
                placedincorrect = false;
            }
        }

        if (PossInccorect > 1)
        {
            if (Mathf.Round(transform.eulerAngles.z) == triggerincorrect[0] || Mathf.Round(transform.eulerAngles.z) == triggerincorrect[1])
            {
                placedincorrect = true;
            }
        }
        else if (PossInccorect == 1)
        {
            if (Mathf.Round(transform.eulerAngles.z) == triggerincorrect[0])
            {
                placedincorrect = true;
            }
        }
        else
        {

        }
    }

 

    private void ParameterOnClick()
    {
        transform.Rotate(new Vector3(0, 0, 90)); //When click on a pipe, would rotate it in 90 degrees


        //If Possible solutions are more than 2...checks the array of solutions if they match then the pipe is correctly placed, if not then it will be counted as false
        if (PossSolutions > 2)
        {
            if (Mathf.Round(transform.eulerAngles.z) == solution[0]  || Mathf.Round(transform.eulerAngles.z) == solution[1]  || Mathf.Round(transform.eulerAngles.z) == solution[2] || Mathf.Round(transform.eulerAngles.z) == solution[3])
            {
                isPlaced = true;
                if (isPlaced == false)
                {
                    pipeManager.correctposition();
                    
                }
                placedincorrect = false;

            }
            else if (isPlaced == true)
            {

                isPlaced = false;
                pipeManager.wrongposition();
               
            }
        }
        //If Possible solutions are more than 1 but less than = 3...checks the array of solutions if they match then the pipe is correctly placed, if not then it will be counted as false
        else if (PossSolutions > 1 && PossSolutions <= 3)
        {
            if (Mathf.Round(transform.eulerAngles.z) == solution[0] && isPlaced == false || Mathf.Round(transform.eulerAngles.z) == solution[1] && isPlaced == false)
            {
                isPlaced = true;
                
                 pipeManager.correctposition();
                placedincorrect = false;
               

            }
            else if (isPlaced == true)
            {
               
                isPlaced = false;
                pipeManager.wrongposition();
                placedincorrect = false;
            }
        }
        else
        {
            if (Mathf.Round(transform.eulerAngles.z) == solution[0])
            {
               
                isPlaced = true;
                pipeManager.correctposition();
                placedincorrect = false;


            }
            else if (isPlaced == true)
            {
              
                isPlaced = false;
                pipeManager.wrongposition();
                placedincorrect = false;
            }
        }


        if (PossInccorect > 1)
        {
            if (Mathf.Round(transform.eulerAngles.z) == triggerincorrect[0] || Mathf.Round(transform.eulerAngles.z) == triggerincorrect[1])
            {
                placedincorrect = true;
            }
        }
        else if (PossInccorect == 1)
        {
            if (Mathf.Round(transform.eulerAngles.z) == triggerincorrect[0])
            {
                placedincorrect = true;
            }
        }
        else
        {

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

        
        if (PipeConnected.Length == 3)
        {
            if (PipeConnected[1].GetComponent<PipePuzzle>().placedincorrect == true && PipeConnected[2].GetComponent<PipePuzzle>().isPlaced == true && isPlaced == true)
            {
                original.sprite = wrongcolor;
                placedincorrect = true;
              
            }
            else if (PipeConnected[0].GetComponent<PipePuzzle>().isPlaced == true && PipeConnected[1].GetComponent<PipePuzzle>().isPlaced == true && isPlaced == true)
            {
                original.sprite = newcolor;
                placedincorrect = false;
               
            }
            //else if(placedincorrect == true && PipeConnected
            else
            {
                original.sprite = theogsprite;
           
               
               
               
                
                
            }
        }
        else if(PipeConnected.Length == 2)
        {
            if(PipeConnected[0].GetComponent<PipePuzzle>().placedincorrect == true &&  PipeConnected[0].GetComponent<PipePuzzle>().isPlaced == true &&
               PipeConnected[1].GetComponent<PipePuzzle>().isPlaced == true && PipeConnected[1].GetComponent<PipePuzzle>().placedincorrect == true && isPlaced == true)
            {
                original.sprite = wrongcolor;
                PipeConnected[0].GetComponent<PipePuzzle>().isPlaced = false;
            }
             else  if (PipeConnected[0].GetComponent<PipePuzzle>().isPlaced == true && PipeConnected[1].GetComponent<PipePuzzle>().isPlaced == true && isPlaced == true)
            {
                original.sprite = newcolor;
            }
            else if (PipeConnected[0].GetComponent<PipePuzzle>().placedincorrect == true && PipeConnected[1].GetComponent<PipePuzzle>().placedincorrect == true && isPlaced == true)
            {
                original.sprite = wrongcolor;
            }
            else if (PipeConnected[0].GetComponent<PipePuzzle>().placedincorrect == true && PipeConnected[1].GetComponent<PipePuzzle>().isPlaced==true && isPlaced == true)
            {
                original.sprite = wrongcolor;
            }
            else
            {
                original.sprite = theogsprite;
            }

        
        }
        else if(PipeConnected.Length == 1)
        {
            if(PipeConnected[0].GetComponent<PipePuzzle>().isPlaced == false)
            {
                placedincorrect = false;
                original.sprite = wrongcolor;
            }
            else if(PipeConnected[0].GetComponent<PipePuzzle>().isPlaced == true && Mathf.Round(transform.eulerAngles.z) ==solution[1])
            {
                if (isPlaced)
                {
                    pipeManager.wrongposition();
                }
                isPlaced = false;
                
            }
        }

    }
}
