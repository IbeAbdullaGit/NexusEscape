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

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private MouseLook m_MouseLook;
     private Camera m_Camera;

      public PlayerAction inputAction;
      //public static PlayerInputController instance;

    Vector2 move;
    Vector2 rotate;

    public float walkSpeed = 5.0f;
    public float speed;
    public float runSpeed = 8.0f;

     //player jump
    Rigidbody rb;
    private float distanceToGround;
    private bool isGrounded = true;
    public float jump = 5f;

     private CharacterController m_CharacterController;

    //private CollisionFlags m_CollisionFlags;

    //NEW
    public bool crawl = false;
    public float crouchSpeed;
    private float initialHeight;

    bool m_IsWalking = true;

    public void CrawlChange()
        {
            crawl = !crawl;
            if (crawl)
            {
                m_CharacterController.height = 0.3f;
               // m_WalkSpeed = crouchSpeed;
            }
            else
            {
                m_CharacterController.height = initialHeight;
                //m_WalkSpeed
            }
        }


    // Start is called before the first frame update
    void Start()
    {
         m_CharacterController = GetComponent<CharacterController>();
        m_Camera = GetComponentInChildren<Camera>();
        m_MouseLook.Init(transform , m_Camera.transform);

        //using controller from playerinputcontroller
        inputAction = PlayerInputController.controller.inputAction;

        inputAction.Player1.Move.performed += cntxt => move = cntxt.ReadValue<Vector2>();
        inputAction.Player1.Move.canceled += cntxt => move = Vector2.zero;

         inputAction.Player1.Jump.performed += cntxt => Jump();

        rb = GetComponent<Rigidbody>();
        distanceToGround = GetComponent<Collider>().bounds.extents.y;

         initialHeight = m_CharacterController.height;

        
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
         //CHECK FOR CROUCH
            if (Input.GetKeyDown(KeyCode.C))
            {
                CrawlChange();
                
            }
        RotateView();
         m_IsWalking = !Input.GetKey(KeyCode.LeftShift); //RUN KEY

        //NEW, CHECK FOR CROUCHING
            if (crawl)
            {
                speed = crouchSpeed;
            }
            else{
                speed = m_IsWalking ? walkSpeed : runSpeed;
            }
        transform.Translate(transform.forward * move.x * Time.deltaTime * speed, Space.Self);
        transform.Translate(-transform.right * move.y * Time.deltaTime * speed, Space.Self);
        //m_CollisionFlags = transform.position;

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

    /*  private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        } */
}
}
