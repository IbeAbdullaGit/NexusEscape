using Riptide;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerClient : MonoBehaviour
{
    [SerializeField] private Transform camTransform;

    private bool[] inputs;

    private Vector2 move;
    private Vector2 look;
    private bool jump, run = false;

    private void Start()
    {
        inputs = new bool[6];
    }

    private void Update()
    {
        //possibly adjust this
        
       /*  if (Input.GetKey(KeyCode.W))
            inputs[0] = true;

        if (Input.GetKey(KeyCode.S))
            inputs[1] = true;

        if (Input.GetKey(KeyCode.A))
            inputs[2] = true;

        if (Input.GetKey(KeyCode.D))
            inputs[3] = true;

        if (Input.GetKey(KeyCode.Space))
            inputs[4] = true;

        if (Input.GetKey(KeyCode.LeftShift))
            inputs[5] = true; */

        //get inputs from player controller class
        move = GetComponent<PlayerController2>().move; //describes normal moving
        look = GetComponent<PlayerController2>().look;
        //for jump we can simply see if they pressed the jump key
        if (Input.GetKey(KeyCode.Space))
            jump = true;
        //detect run
         if (Input.GetKey(KeyCode.LeftShift))
            run = true;


        //do this every frame for smoother results
        SendUpdate();


    }

    private void SendUpdate()
    {
        SendInput();

        //reset inputs
        jump = false;
        run = false;
        move.Set(0, 0);
    }

    #region Messages
    private void SendInput()
    {
        Message message = Message.Create(MessageSendMode.Reliable, ClientToServerId.input);

        //we need to send position and forward, when we need it. For now more important to send when we've moved
        //put bools into an array, send jump and run state
        bool[] input = {jump, run};
        message.AddBools(input, false);
        //send movement state
        message.AddVector2(move);
        //send where the player is facing
        //use "look" value
        message.AddVector2(look);
        //send message
        NetworkManagerClient.Singleton.Client.Send(message);
    }
    #endregion
}