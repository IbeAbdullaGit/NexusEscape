using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static Inventory instance;

    List<KeyPickUp> keys;

    public KeycardPickup keycard;

    public TilePickUp currentTile;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void AddKey(KeyPickUp key)
    {
        keys.Add(key);
    }

    public void AddCard(KeycardPickup key)
    {
        keycard = key;
    }

    public void AddTile(TilePickUp tile)
    {
        currentTile = tile;
    }
    public void RemoveTile()
    {
        currentTile = null;
    }
    public void RemoveKeyCard()
    {
        //keycard.transform.parent = null; //Drop Keycard (not delete)

        keycard = null;
    }
    
    public void DestroyKeycard()
    {
        Destroy(keycard.gameObject);
    }

}
