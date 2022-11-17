using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingTest : MonoBehaviour
{

    public bool UseObjectPooling = false;
    public GameObject prefab;
    GameObject currentObject;

    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (UseObjectPooling)
            {
                ObjectPooler.instance.SpawnFromPool("Particles", new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f));
            }
            else
            {
                currentObject = Instantiate(prefab);
            }
        }
        
    }
}
