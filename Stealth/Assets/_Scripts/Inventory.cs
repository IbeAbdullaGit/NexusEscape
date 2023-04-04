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
    private void Start()
    {
        if(instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
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
        ids.RemoveAt(keycardNum-1);
        keycardNum -=1;
        //remove id
    
    }
    public void ResetCards()
    {
        keycardNum = 0;
        for (int i=0; i< images.Count; i++)
        {
            images[i].enabled = false;
        }
        currentTile = null;
        ids.Clear();
    }
    
   

}
