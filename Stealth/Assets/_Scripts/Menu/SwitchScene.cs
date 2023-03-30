using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Riptide;

public class SwitchScene : MonoBehaviour
{
    public GameObject LoadingScreen;
   public Image LoadingBarFill;

   private void Start() {
      Application.runInBackground = true;
   }

   public void LoadScene()
   {
      GetComponent<SavePlugin>().LoadFile();
   }
   public void LoadNextScene()
   {
      //plus 1 for next scene
      //WE ALREADY SAVED PLUS 1 PREVIOUSLY
      /* int id = GetComponent<SavePlugin>().GetCurrentID();
      //dont load what we dont have
      //scene count starts at 1, id starts at 0, scene count should always be bigger
      if (SceneManager.sceneCountInBuildSettings > id)
         ChangeScene(id); */

      //change for networking
      //realistically we are only going to go to nexus 2 because theres no other inbetweens
      if (NetworkManagerServer.Singleton != null) //only if this is the server
      {
         Message message = Message.Create(MessageSendMode.Reliable, ServerToClientId.puzzleInteraction);
         message.AddInt(5); //button meter puzzle
         NetworkManagerServer.Singleton.Server.SendToAll(message);

         ChangeScene("Nexus2Server");
      }
      else //not the server, so the client
      {
         ChangeScene("Nexus2Client");
      }
   }

   public void ChangeScene(string title)
   {
        //SceneManager.LoadScene(title);
        StartCoroutine(LoadSceneAsynch(title));
        Time.timeScale =1;
   }
    public void ChangeScene(int id)
   {
        //SceneManager.LoadScene(title);
        StartCoroutine(LoadSceneAsynch(id));
        Time.timeScale =1;

   }
   public void CloseProgram()
   {
      Application.Quit();
   }
   IEnumerator LoadSceneAsynch(string title)
   {
      AsyncOperation operation = SceneManager.LoadSceneAsync(title);
      LoadingScreen.SetActive(true);
      while (!operation.isDone)
      {
         float progressValue = Mathf.Clamp01(operation.progress/0.1f);
         LoadingBarFill.fillAmount = progressValue;
         yield return null;
      }
   }
   IEnumerator LoadSceneAsynch(int id)
   {
      AsyncOperation operation = SceneManager.LoadSceneAsync(id);
      LoadingScreen.SetActive(true);
      while (!operation.isDone)
      {
         float progressValue = Mathf.Clamp01(operation.progress/0.1f);
         LoadingBarFill.fillAmount = progressValue;
         yield return null;
      }
   }
}
