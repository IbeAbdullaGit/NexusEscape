using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    public List<KeyPickUp> keys;

    public TilePickUp currentTile;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void AddKey(KeyPickUp key)
    {
        keys.Add(key);
    }

    public void AddTile(TilePickUp tile)
    {
        currentTile = tile;
    }
    public void RemoveTile()
    {
        currentTile = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
