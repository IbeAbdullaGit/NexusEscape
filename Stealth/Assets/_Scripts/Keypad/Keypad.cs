using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Keypad : MonoBehaviour
{
   public TMP_Text Ans;
   
   //will need to have several answers depending on the situation
   public string answer;

    //mostly to keep track of resetting the display
   int counter =0;

   public InteractKeypad currentInstance;

   public bool correct = false;

    public FMODUnity.EventReference correctSound;

    public FMODUnity.EventReference incorrectSound;

    public FMODUnity.EventReference buttonPress;
    public void Number(int number)
    {
        FMODUnity.RuntimeManager.PlayOneShot(buttonPress);
        if (counter ==0)
        {
            Ans.text = "";
            counter +=1;
        }
        Ans.text += number.ToString();
    }
   
   public void Execute()
   {
        if (Ans.text == answer)
        {
            Ans.text = "Correct";
             counter = 0;
            //do something
            currentInstance.correct = true;
            FMODUnity.RuntimeManager.PlayOneShot(correctSound);

        }
        else
        {
            Ans.text = "INCORRECT";
            counter = 0;
            //do something else, penalty
            currentInstance.correct = false;
            FMODUnity.RuntimeManager.PlayOneShot(incorrectSound);
        }
   }
}
