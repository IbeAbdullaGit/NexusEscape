using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayerMovement : MonoBehaviour
{
    private bool[] inputs;
    //[SerializeField] private Transform camProxy;

    Vector2 facing;

    private Vector2 movement;
    

    private void Start() {
        
        inputs = new bool[2];
    }
    private void FixedUpdate() {
        
        //first face the right direction
        //transform.forward = facing;
        //this is actually the look
        GetComponent<PlayerController2>().look = facing;
        
        //send over the inputs
        GetComponent<PlayerController2>().move = movement;
        //where they are facing
        //transform.forward = camProxy.forward;
        if (inputs[0]) //jump
        {
            GetComponent<PlayerController2>().Jump();
        }
        if (inputs[1]) //run
        {
            GetComponent<PlayerController2>().state = PlayerController2.MovementState.sprinting;
        }

    }

    public void SetInput(bool[] inputs, Vector2 move, Vector2 look)
    {
        this.inputs = inputs;
        movement = move;
        facing = look;
        
    }

   /*  private void SendMovement()
    {
        Message message = Message.Create(MessageSendMode.unreliable, ServerToClientId.playerMovement);
        message.AddUShort(player.Id);
        message.AddVector3(transform.position);
        message.AddVector3(camProxy.forward);
        NetworkManager.Singleton.Server.SendToAll(message);
    } */

}
