using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Riptide;

public class Typer : MonoBehaviour
{
    public WordBank wordBank = null;
    public TMP_Text wordOutput = null;

    public TMP_Text correctText = null;
    public Canvas typingCanvas;

    private string remainingWord = string.Empty;
    private string currentWord = string.Empty;

    //int wrongCount = 0;
    int rightCount = 0;

    

    Timer currentTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        SetCurrentWord();
        //get the timer
        currentTimer = GetComponent<Timer>();
        //disable
        typingCanvas.enabled = false;
    }
    private void SetCurrentWord()
    {
        //get bank word
        currentWord = wordBank.GetWord();
        SetRemainingWord(currentWord);
    }
    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;
        wordOutput.text = remainingWord;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        //update label
        correctText.text = rightCount.ToString();
        if (rightCount == 3) //typed 3 correct words, within time limit
        {
           
            //hide interface, reset everything
            typingCanvas.enabled = false;
            wordBank.ResetBank();
            SetCurrentWord();

            var possibles = GetComponent<LinkedPuzzle>().amount_linked;

            //DO SOMETHING HERE
            for (int i=0; i< possibles.Length; i++)
            {
                if (possibles[i])
                {
                    if (i==0) //first one
                    {
                        GetComponent<LinkedPuzzle>().ActivateText(); //activates the text
                        //send message
                         //send network message, to open the door
                        Message message = Message.Create(MessageSendMode.Reliable, ClientToServerId.typingPuzzleFinish);
                        //add an id so we know what we're talking about
                        message.AddInt(0); //0 - for this case, means activate text
                        //send message
                        NetworkManagerClient.Singleton.Client.Send(message);
                    }
                    else if (i ==1) //second one
                    {
                        GetComponent<LinkedPuzzle>().ActivateDoor(); //activates the door
                        //send message
                        //send network message, to open the door
                        Message message = Message.Create(MessageSendMode.Reliable, ClientToServerId.typingPuzzleFinish);
                        //add an id so we know what we're talking about
                        message.AddInt(1); //1 - for this case, means activate door
                        //send message
                        NetworkManagerClient.Singleton.Client.Send(message);

                    }

                    break;
                }
                
            }
        }
        //losing condition
        if (currentTimer.hitLimit)
        {
            //reset
            //typingCanvas.enabled = false;
            //reset puzzle so hacker can keep trying
            ResetTyping();
            
        }

        //reset everything
        //replace later
       /*  if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ResetTyping();
        } */
    }
    public void ResetTyping()
    {
        rightCount = 0;
        wordBank.ResetBank();
        SetCurrentWord();
        currentTimer.ResetTimer();
        typingCanvas.enabled = true;
    }
    private void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;

            if (keysPressed.Length ==1) //only one letter get was pressed
                EnterLetter(keysPressed);
        }
    }
    private void EnterLetter(string typedLetter)
    {
        if (IsCorrectLetter(typedLetter))
        {
            RemoveLetter();

            if (IsWordComplete())
            {
                SetCurrentWord();
                rightCount +=1;
            }
        }
        else //didn't type right letter
        {
           
        }
    }
    private bool IsCorrectLetter(string letter)
    {
        return remainingWord.IndexOf(letter) == 0;
    }
    private void RemoveLetter()
    {
        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }
    private bool IsWordComplete()
    {
        return remainingWord.Length == 0;
    }
}
