using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RevealAnswer : Observer
{
    TMP_Text text;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        //hide at the start
        text.enabled = false;

    }

    public override void OnNotify()
    {
        text.enabled = true;
    }
}
