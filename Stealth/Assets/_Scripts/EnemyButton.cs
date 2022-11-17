using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyButton : MonoBehaviour
{
    
    //private EnemyFactory factory;

    private EditorManager editor;

    TextMeshProUGUI btnText;
    // Start is called before the first frame update
    void Start()
    {
        //factory = GameObject.Find("Game Manager").GetComponent<EnemyFactory>();

        editor = EditorManager.instance;

        btnText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnClickSpawn()
    {
        Debug.Log("testing2");
        // switch (btnText.text)
        // {
        //     case "Enemy":
        //         editor.item = Create(factory.prefab1);
        //         Debug.Log("testing1");
        //         break;
            
        //     default:
        //         break;
        // }
        editor.item = Create(editor.prefab_enemy);
        editor.instantiated = true;
    }

     public GameObject Create(GameObject prefab)
    {
        GameObject enemy = Instantiate(prefab);
        return enemy;
    }

}
