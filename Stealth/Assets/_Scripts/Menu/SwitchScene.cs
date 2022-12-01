using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchScene : MonoBehaviour
{
    public GameObject LoadingScreen;
   public Image LoadingBarFill;

   public void LoadScene()
   {
      GetComponent<SavePlugin>().LoadFile();
   }
   public void LoadNextScene()
   {
      //plus 1 for next scene
      //WE ALREADY SAVED PLUS 1 PREVIOUSLY
      int id = GetComponent<SavePlugin>().GetCurrentID();
      //dont load what we dont have
      //scene count starts at 1, id starts at 0, scene count should always be bigger
      if (SceneManager.sceneCountInBuildSettings > id)
         ChangeScene(id);
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
