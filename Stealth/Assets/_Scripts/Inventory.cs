using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public static Inventory instance;

    List<KeyPickUp> keys;

    public int keycardNum = 0;
    public List<int> ids;

    public TilePickUp currentTile;

    public List<Image> images;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        for (int i=0; i< images.Count; i++)
        {
            images[i].enabled = false;
        }
    }
    public void AddKey(KeyPickUp key)
    {
        keys.Add(key);

    }

    public void AddCard(int n, int id)
    {
        keycardNum +=1;
        ids.Add(id);
        images[keycardNum-1].enabled = true;
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
        images[keycardNum-1].enabled = false;
        keycardNum -=1;
    }
    
   

}
