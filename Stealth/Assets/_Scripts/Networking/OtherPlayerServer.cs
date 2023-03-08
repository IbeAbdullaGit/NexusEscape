using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Riptide;

public class OtherPlayerServer : MonoBehaviour
{
    private bool[] inputs;
    //[SerializeField] private Transform camProxy;

    Vector2 facing;

    private Vector2 movement;
    

    private void Start() {
        
        inputs = new bool[2];
    }
    private void Update() {
        
        //first face the right direction
        //transform.forward = facing;
        //this is actually the look
        GetComponent<PlayerController>().look = facing;
        
        //send over the inputs
        GetComponent<PlayerController>().move = movement;
        //where they are facing
        //transform.forward = camProxy.forward;
        if (inputs[0]) //jump
        {
            GetComponent<PlayerController>().Jump();
        }
        if (inputs[1]) //run
        {
            GetComponent<PlayerController>().state = PlayerController.MovementState.sprinting;
        }

    }

    public void SetInput(bool[] inputs, Vector2 move, Vector2 look)
    {
        this.inputs = inputs;
        movement = move;
        facing = look;
        
    }
}

