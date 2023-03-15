using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
   [Header("Component")]
   public TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    
    public float currentTime;
    public bool countDown;

    [Header ("Limit Settings")]
    public bool hasLimit;
    public float timerLimit; 

    public bool hitLimit = false;

    float originalTime;

    // Start is called before the first frame update
    void Start()
    {
       originalTime = currentTime;
    }

    // Update is called once per frame
    void Update()
    {

        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;
        if (hasLimit && ((countDown && currentTime <= timerLimit) || (!countDown && currentTime >= timerLimit)))
        {
            currentTime = timerLimit;
            SetTimerText();
            timerText.color = Color.red;

            hitLimit = true;

            enabled = false;

            
        }

            SetTimerText();
    }
  
    private void SetTimerText()
    {
        //set how many decimal points
        timerText.text = currentTime.ToString("0.0");
    }
    public void ResetTimer()
    {
        currentTime = originalTime;
        countDown = true;
        hasLimit = true;
        hitLimit = false;
        timerLimit = 0;
        enabled = true;
        //reset color
        timerText.color = Color.white;
    }
}
