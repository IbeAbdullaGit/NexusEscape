using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchScene : MonoBehaviour
{
    public GameObject LoadingScreen;
   public Image LoadingBarFill;

   public void ChangeScene(string title)
   {
        //SceneManager.LoadScene(title);
        StartCoroutine(LoadSceneAsynch(title));
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
         float progressValue = Mathf.Clamp01(operation.progress/0.9f);
         LoadingBarFill.fillAmount = progressValue;
         yield return null;
      }
   }
}
