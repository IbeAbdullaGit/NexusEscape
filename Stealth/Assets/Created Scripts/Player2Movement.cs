using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;


#pragma warning disable 618, 649
namespace UnityStandardAssets.Characters.FirstPerson
{

public class Player2Movement : MonoBehaviour
{
    [SerializeField] private MouseLook m_MouseLook;
     private Camera m_Camera;

      public PlayerAction inputAction;
      //public static PlayerInputController instance;

    Vector2 move;
    Vector2 rotate;

    public float walkSpeed = 5.0f;

     //player jump
    Rigidbody rb;
    private float distanceToGround;
    private bool isGrounded = true;
    public float jump = 5f;

    // Start is called before the first frame update
    void Start()
    {
        m_Camera = GetComponentInChildren<Camera>();
        m_MouseLook.Init(transform , m_Camera.transform);

        //using controller from playerinputcontroller
        inputAction = PlayerInputController.controller.inputAction;

        inputAction.Player2.Move.performed += cntxt => move = cntxt.ReadValue<Vector2>();
        inputAction.Player2.Move.canceled += cntxt => move = Vector2.zero;

         inputAction.Player2.Jump.performed += cntxt => Jump();

        rb = GetComponent<Rigidbody>();
        distanceToGround = GetComponent<Collider>().bounds.extents.y;
        
    }
    private void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            isGrounded = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        RotateView();
        transform.Translate(transform.right * move.x * Time.deltaTime * walkSpeed, Space.Self);
        transform.Translate(transform.forward * move.y * Time.deltaTime * walkSpeed, Space.Self);

         isGrounded = Physics.Raycast(transform.position, -Vector3.up, distanceToGround);


         m_MouseLook.UpdateCursorLock();
    }
  

    private void RotateView()
        {
            m_MouseLook.LookRotation(transform, m_Camera.transform);
        }


        public void SetRotateZ(float value)
        {
            m_MouseLook.SetRotateZ(value);
        }
}
}
