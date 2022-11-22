using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
   private void OnTriggerEnter(Collider other) {
    
    if (other.tag == "Player") //if the player comes in
    {
        //unload this scene
        SceneManager.UnloadSceneAsync("Tutorial");
        //load the next scene, or loading scene
        SceneManager.LoadScene("Level1");
    }
   }
}
