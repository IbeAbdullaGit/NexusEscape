using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LaptopView : Interactable
{
    
  
    bool revelead= false;
    TMP_Text text;
    
    public override void OnInteract()
    {
        if (!revelead)
        {
            revelead = true;
            text.enabled = true;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TMP_Text>();
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
