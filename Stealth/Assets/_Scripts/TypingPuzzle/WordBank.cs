using System.Linq;
using System.Collections.Generic;
using UnityEngine;


public class WordBank : MonoBehaviour
{
    private List<string> originalWords = new List<string>()
    {
        "monkas", "pog", "hack", "mine", "crypto", "coding", "matrix", "monkey", "ooga", "epic", "leet", "break", "mainframe"
    };
    private List<string> workingWords = new List<string>();
    
    private void Awake() {
        workingWords.AddRange(originalWords);
        Shuffle(workingWords);
        ConvertToLower(workingWords);
    }

    private void Shuffle(List<string> list)
    {
        for (int i=0; i< list.Count; i++)
        {
            int random = Random.Range(i, list.Count); //random place
            string temporary = list[i]; //temporary hold

            list[i] = list[random]; //shuffle
            list[random] = temporary; //shuffle of other word
        }
    }
    private void ConvertToLower(List<string> list)
    {
        for (int i=0; i< list.Count; i++)
        {
            list[i] = list[i].ToLower();
            //also shuffle the word
            list[i] = Shuffle(list[i]);
        }
    }
    private string Shuffle(string str)
    {
        char[] array = str.ToCharArray();
        //Random rng = new Random();
        
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0,n + 1);
            var value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
        return new string(array);
    }
    public string GetWord()
    {
        string newWord = string.Empty;
        if (workingWords.Count != 0) //if there are remaining words
        {
            //get the last word, so we don't need to shift down
            newWord = workingWords.Last();
            //now remove the word
            workingWords.Remove(newWord);
        }

        return newWord;
    }

    public void ResetBank()
    {
        workingWords.Clear();
        workingWords.AddRange(originalWords);
        Shuffle(workingWords);
        ConvertToLower(workingWords);
    }
}
