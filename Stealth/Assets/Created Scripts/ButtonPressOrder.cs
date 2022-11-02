using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonPressOrder : MonoBehaviour
{
    
    [Range(1,3)]
    public int[] buttonOrder = new int[3];

    [Range(1, 3)]
    public List<int> enteredOrder;

    public Button[] buttons;

    public GameObject linkedObject;

    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //means we have an answer entered
        if (enteredOrder.Count ==3)
        {
            if (AnswerCheck())
            {
                text.text = "Correct!";
                //do something with the linked object
            }
            else{
                text.text = "Incorrect!";
            }
            //reset buttons
            for (int i=0; i< buttons.Length; i++)
            {
                buttons[i].interactable = true;
                buttons[i].GetComponent<ButtonPressPuzzle>().canPress = true;
            }
            //reset answer
            enteredOrder.Clear();
        }
        
        
    }
    bool AnswerCheck()
    {
        for (int i=0; i< buttonOrder.Length; i++)
        {
            //check if current pair matches
            if (buttonOrder[i] != enteredOrder[i])
                return false;
        }
        //if the for loop finished, it means all the pairs matched
       
        return true;
    }
}
