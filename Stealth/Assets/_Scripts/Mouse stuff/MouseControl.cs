using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public Texture2D defaultCursor, clickableCursor;

    public static MouseControl instance;
    
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    private void Start() {
        Default();
    }

   public void Clickable()
   {
        Cursor.SetCursor(clickableCursor, Vector3.zero, CursorMode.Auto);
   }
   public void Default()
   {
        Cursor.SetCursor(defaultCursor, Vector3.zero, CursorMode.Auto);
   }
}
