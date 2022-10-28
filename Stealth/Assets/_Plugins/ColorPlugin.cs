using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class ColorPlugin : MonoBehaviour
{
    [DllImport("SaveColour")]
    private static extern int GetID();

    [DllImport("SaveColour")]
    private static extern void SetID(int id);

    [DllImport("SaveColour")]
    private static extern Color GetColor();

    [DllImport("SaveColour")]
    private static extern void SetColor(float R, float G, float B, float A);

    [DllImport("SaveColour")]
    private static extern void SaveColor(int id, float R, float G, float B, float A);

    [DllImport("SaveColour")]
    private static extern void startWriting(string fileName);

    [DllImport("SaveColour")]
    private static extern void EndWriting();

    PlayerAction inputAction;
    string m_Path;
    string fn;

    EditorManager editor;

    void Start()
    {
        inputAction = PlayerInputController.controller.inputAction;

        inputAction.Editor.SaveColor.performed += cntext => SaveColorData();

        m_Path = Application.dataPath;
        fn = m_Path + "/colour_data.txt";
        Debug.Log(fn);

        editor = GetComponent<EditorManager>();
    }

    void SaveColorData()
    {

        Debug.Log("Start Saving Color");

        if (editor.editorMode)
        {
            startWriting(fn);

            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("pickup"))
            {
                var objectRenderer = obj.GetComponent<Renderer>();
                Color32 objectColor = objectRenderer.material.color;
                SaveColor(1, objectColor.r, objectColor.g, objectColor.b, objectColor.a);
                Debug.Log("Saving " + objectColor.r + "," + objectColor.g + "," + objectColor.b);
                objectRenderer.material.SetColor("_Color", Color.red); //Demo purposes

            }
            EndWriting();
            Debug.Log("Color Saved");

        }
    }
}
