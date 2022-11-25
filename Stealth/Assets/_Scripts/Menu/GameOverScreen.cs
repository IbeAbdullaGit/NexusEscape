using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void Restart()
    {
        //send in current scene name to scene switcher, so the level restarts
        GameObject.FindGameObjectWithTag("GameController").GetComponent<SwitchScene>().ChangeScene(SceneManager.GetActiveScene().name);
        //set back the time scale just in case
        Time.timeScale = 1;
    }
    public void GoToMenu()
    {
        //send in the menu scene so we go back to the main menu
        GameObject.FindGameObjectWithTag("GameController").GetComponent<SwitchScene>().ChangeScene("Menu");
    }
}
