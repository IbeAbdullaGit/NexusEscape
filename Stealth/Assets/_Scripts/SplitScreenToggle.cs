using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreenToggle : MonoBehaviour
{
    PlayerAction inputAction;
    bool splitscreen = true;
    Camera screen;
    Camera screen2;
    // Start is called before the first frame update
    void Start()
    {
        inputAction = PlayerInputController.controller.inputAction;
        inputAction.Player1.ToggleSplitScreen.performed += cntxt => SplitScreen();

        screen = transform.GetChild(0).GetComponent<Camera>();
        screen2 = GameObject.FindGameObjectWithTag("Player2").transform.GetChild(0).GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //for enabling second player
        if (Input.GetKeyDown(KeyCode.Z))
        {
            splitscreen = !splitscreen;
            if (splitscreen)
            {
                 screen.rect = new Rect(0,0,0.5f,1);
                screen2.rect = new Rect (0.5f, 0, 0.5f, 1);

                screen.orthographicSize =1;
                screen2.orthographicSize = 1;
            }
            else{
                //player 2 is the main
                screen2.rect = new Rect(0,0,1.0f,1);
                screen.rect = new Rect (0.0f, 0, 0.0f, 1);

                screen.orthographicSize =1;
                screen2.orthographicSize = 1;
            }
        }
    }
    void SplitScreen()
    {
        splitscreen = !splitscreen;
        if (splitscreen)
        {
            screen.rect = new Rect(0,0,0.5f,1);
            screen2.rect = new Rect (0.5f, 0, 0.5f, 1);
        }
        else{
            screen.rect = new Rect(0,0,1.0f,1);
            screen2.rect = new Rect (0.0f, 0, 0.0f, 1);
        }
    }
}
