using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

            //DO SOMETHING HERE
        }
        //losing condition
        if (currentTimer.hitLimit)
        {
            //reset
           
            wordBank.ResetBank();
            SetCurrentWord();
            typingCanvas.enabled = false;
            
        }

        //reset everything
        //replace later
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ResetTyping();
        }
    }
    void ResetTyping()
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
