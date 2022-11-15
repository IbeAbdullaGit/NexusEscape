using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    public List<KeyPickUp> keys;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void AddKey(KeyPickUp key)
    {
        keys.Add(key);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
