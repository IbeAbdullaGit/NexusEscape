using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPressPuzzle : MonoBehaviour
{
    
    Button button;
    public bool canPress = true;

    ButtonPressOrder buttonOrder;

    [Range(1,8)] //was 3
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        buttonOrder = GameObject.FindGameObjectWithTag("GameController").GetComponent<ButtonPressOrder>();
        
    }

    public void isPressed()
    {
        if (canPress)
        //run this when the button is pressed
       { 
            //turn off the button
            button.interactable = false;
            buttonOrder.enteredOrder.Add(id);
            canPress = false;
       }
    }
}
