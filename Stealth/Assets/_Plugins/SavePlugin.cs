using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class SavePlugin : MonoBehaviour
{
    [DllImport("Plugin")]
    private static extern int GetID();

    [DllImport("Plugin")]
    private static extern void SetID(int id);

     [DllImport("Plugin")]
     private static extern Vector3 GetPosition();
      [DllImport("Plugin")]
      private static extern void SetPosition(float x, float y, float z);

       [DllImport("Plugin")]
       private static extern void SaveToFile(int id, float x, float y, float z);

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
        if (editor.editorMode)
        {
            StartWriting(fn);
            //for now, only position is important for saving
            //also we only need to save the player for now
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
            {
                SaveToFile(1, obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);
                
            }
            EndWriting();

            Debug.Log("Saved");

        }
    }
    public void LoadFile()
    {
        Debug.Log("Start loading");
        if (editor.editorMode)
        {
            StartWriting(fn);
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
            {
                //obj.transform.position = LoadFromFile(fn);
                LoadFromFile(fn);
                //obj.GetComponent<CharacterController>().enabled = false;
                obj.transform.position = GetPosition();
               // obj.GetComponent<CharacterController>().enabled = true;
                
            }
            EndWriting();
            Debug.Log("Loaded");
        }
        
    }

}
