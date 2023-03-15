using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Riptide;

public class PlayerControllerServer : MonoBehaviour
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
        /* //get inputs from player controller class
        move = GetComponent<PlayerController>().move; //describes normal moving
        look = GetComponent<PlayerController>().look;
        //for jump we can simply see if they pressed the jump key
        if (Input.GetKey(KeyCode.Space))
            jump = true;
        //detect run
         if (GetComponent<PlayerController>().state == PlayerController.MovementState.sprinting)
            run = true;


        //do this every frame for smoother results
        //SendUpdate();
 */
        SendSimpleInput();
    }

    private void SendUpdate()
    {
        SendInput();

        //reset inputs
        jump = false;
        run = false;
        move.Set(0, 0);
        look.Set(0,0);
    }

    #region Messages
    private void SendSimpleInput()
    {
        Message message = Message.Create(MessageSendMode.Reliable, ServerToClientId.inputSimple);
        message.AddVector3(transform.position);
        NetworkManagerServer.Singleton.Server.SendToAll(message);
    }
    private void SendInput()
    {
        Message message = Message.Create(MessageSendMode.Reliable, ServerToClientId.input);

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
       
        NetworkManagerServer.Singleton.Server.SendToAll(message);
    }
    #endregion
}
