using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class SavePlugin : MonoBehaviour
{
    [DllImport("Plugin")]
    private static extern int GetID();

    [DllImport("Plugin")]
    private static extern void SetID(int id);

     [DllImport("Plugin")]
     private static extern string GetPosition();
      [DllImport("Plugin")]
      private static extern void SetPosition(float x, float y, float z);

       [DllImport("Plugin")]
       private static extern void SaveToFile(string s);

        [DllImport("Plugin")]
        private static extern void StartWriting(string fileName);

         [DllImport("Plugin")]
        private static extern void LoadFromFile(string fileName);

        [DllImport("Plugin")]
        private static extern void EndWriting();

        //[DllImport("Plugin.dll", EntryPoint = "?LoadFromFile@GameObject@@QEAA?AUVector3@@PEBD@Z")]
       

    PlayerAction inputAction;
    string m_Path;
    string fn;

    EditorManager editor;

    void Start() {
        inputAction = PlayerInputController.controller.inputAction;

        inputAction.Editor.Save.performed += ContextMenu =>SaveItems();
        inputAction.Editor.Load.performed += ContextMenu =>LoadFile();

        m_Path = Application.dataPath;
        fn = m_Path +"/save_data.json";
        Debug.Log(fn);

        editor = GetComponent<EditorManager>();
    }

    public void SaveItems()
    {
        Debug.Log("Start Saving");
        //if (editor.editorMode)
        {
            StartWriting(fn);
            //we are only saving the last level
            var player = GameObject.FindGameObjectWithTag("Player");
            
            SaveToFile(SceneManager.GetActiveScene().name);
            EndWriting();

            Debug.Log("Saved");

        }
    }
    public void LoadFile()
    {
        Debug.Log("Start loading");
        //if (editor.editorMode)
        {
            StartWriting(fn);
            //we are only saving the last level
            var player = GameObject.FindGameObjectWithTag("Player");
            {
                //obj.transform.position = LoadFromFile(fn);
                LoadFromFile(fn);
                //obj.GetComponent<CharacterController>().enabled = false;
                //player.transform.position = GetPosition();

                //access game manaer
                GameObject.FindGameObjectWithTag("GameController").GetComponent<SwitchScene>().ChangeScene(GetPosition());
                //SceneManager.LoadScene(GetPosition());
               // obj.GetComponent<CharacterController>().enabled = true;
                
            }
            EndWriting();
            Debug.Log("Loaded");
        }
        
    }

}
