using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonPressOrder : MonoBehaviour
{
    
    [Range(1,8)] // was 3
    public List<int> buttonOrder; // make this a list so it can be dynamically chosen

    [Range(1, 8)]
    public List<int> enteredOrder;

    public Button[] buttons;

    public TMP_Text text;

    public bool correct = false;

    public bool openMenu = false;
     public Canvas menuUI;

     public GameObject[] lockers;

     public int id;
    // Start is called before the first frame update
    void Start()
    {
        //ChangeUI(); //disable at start

        menuUI.enabled = false;
        openMenu = false;
        text.text ="";
    }

    // Update is called once per frame
    void Update()
    {
        //means we have an answer entered
        if (enteredOrder.Count == buttonOrder.Count) //check if  they are the same no matter how many there are
        {
            if (AnswerCheck())
            {
                text.text = "Correct!";
                correct=true;
            }
            else{
                text.text = "Incorrect!";
                correct = false;
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
    public void ChangeUI()
    {
        menuUI.enabled = !menuUI.enabled;
        openMenu = !openMenu;
        if (Cursor.lockState == CursorLockMode.Locked)
        {
             Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; 
        }  
        else
        {
             Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false; 
        }
    }
    bool AnswerCheck()
    {
        for (int i=0; i< buttonOrder.Count; i++)
        {
            //check if current pair matches
            if (buttonOrder[i] != enteredOrder[i])
                return false;
        }
        //if the for loop finished, it means all the pairs matched
       
        return true;
    }
}
