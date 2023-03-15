using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    private void Start() {
        GetComponent<Canvas>().enabled = false;
    }
    public void Restart()
    {
        //send in current scene name to scene switcher, so the level restarts
        GameObject.FindGameObjectWithTag("GameController").GetComponent<SwitchScene>().ChangeScene(SceneManager.GetActiveScene().name);
        //set back the time scale just in case
        Time.timeScale = 1;
        //hide screen
        GetComponent<Canvas>().enabled = false;
    }
    public void GoToMenu()
    {
        //send in the menu scene so we go back to the main menu
        GameObject.FindGameObjectWithTag("GameController").GetComponent<SwitchScene>().ChangeScene("Menu");
         //hide screen
        GetComponent<Canvas>().enabled = false;
    }
}
