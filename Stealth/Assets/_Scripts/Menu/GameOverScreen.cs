using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Riptide;

public class GameOverScreen : MonoBehaviour
{
    private void Start() {
        GetComponent<Canvas>().enabled = false;
    }
    public void Restart()
    {
        //send in current scene name to scene switcher, so the level restarts
        GameObject.FindGameObjectWithTag("GameController").GetComponent<SwitchScene>().ChangeScene(SceneManager.GetActiveScene().name);
        //send message
        if (NetworkManagerServer.Singleton !=null)
        {
            Message message = Message.Create(MessageSendMode.Reliable, ServerToClientId.resetGame);
            message.AddInt(1); //same scene

            NetworkManagerServer.Singleton.Server.SendToAll(message);
        }
        //set back the time scale just in case
        Time.timeScale = 1;
        //hide screen
        GetComponent<Canvas>().enabled = false;
    }
    public void GoToMenu()
    {
        //send in the menu scene so we go back to the main menu
        GameObject.FindGameObjectWithTag("GameController").GetComponent<SwitchScene>().ChangeScene("Menu");
        if (NetworkManagerServer.Singleton != null)
        {
            Message message = Message.Create(MessageSendMode.Reliable, ServerToClientId.resetGame);
            message.AddInt(2); //menu scene

            NetworkManagerServer.Singleton.Server.SendToAll(message);

            //now destroy server so we dont need it
            Destroy(GameObject.FindGameObjectWithTag("NetworkServer"));
            
        }
        //hide screen
        GetComponent<Canvas>().enabled = false;
    }
}
