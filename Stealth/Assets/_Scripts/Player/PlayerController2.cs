using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController2 : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject cam;
    public float walkSpeed, sensitivity, maxForce, jumpForce, snappiness;
    public float runSpeed;
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;
    bool crawl;
    float speed;
    
    public Vector2 move; 
    public Vector2 look;
     private Vector3 targetVelocityLerp;
    private float lookRotation;

    public bool grounded;
    
    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        crouching
    }
    [Header("Interaction")]
    [SerializeField]private float interactionDistance = default;
    [SerializeField]private LayerMask interactionLayer = default;
    private Interactable currentInteractable;
    [SerializeField] private bool canInteract = true;
    [SerializeField] private KeyCode interactKey = KeyCode.Mouse0;
    

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;

    float distanceToGround;
     SoundManager soundInstance;

    Vector3 lastPosition;   

     public void OnMove(InputAction.CallbackContext context)
    {
        
        {
            move = context.ReadValue<Vector2>();
        }
    }
    public void OnLook(InputAction.CallbackContext context)
    {
       
        {look = context.ReadValue<Vector2>();
        }
    }
     public void OnJump(InputAction.CallbackContext context)
    {
      
       {Jump();
       }
    }
     void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
         distanceToGround = GetComponent<Collider>().bounds.extents.y;
         startYScale = transform.localScale.y;
         soundInstance = GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundManager>().instance;
         lastPosition = transform.position;
         //if (!view.IsMine)
         {
            //destroy camera
         }
         
    }
    private void Update() {

        
       { 
        StateHandler();
        //NEW, CHECK FOR CROUCHING
        if (crawl)
        {
            speed = crouchSpeed;
        }
        //REDO HOW WE DO GROUNDING
        grounded = Physics.Raycast(transform.position, -Vector3.up, distanceToGround);
       }

       if (canInteract)
         {
            HandleInteractionCheck();
            HandleInteractionInput();
         }
    }
    private void HandleInteractionCheck()
    {
        //adjust for not main camera
        Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            //Debug.Log("Succesful raycast");
            //on the right layer for interactables, and theres not currently anything we're interacting with, OR we're looking at a new object that's not currently looked at
            if (hit.collider.gameObject.layer ==10 && (currentInteractable == null || hit.collider.gameObject.GetInstanceID() != currentInteractable.gameObject.GetInstanceID()))
            {
                //will infer we want interactable
                hit.collider.TryGetComponent<Interactable>(out currentInteractable);

                

                //if we've got an interactable, give it focus
                if (currentInteractable)
                    currentInteractable.OnFocus();
            }
        }
        //raycast doesnt have anything but we have something interacted
        else if (currentInteractable)
        {
            //lose the interactable
            currentInteractable.OnLoseFocus();
            currentInteractable = null;
        }
        
    }
    private void HandleInteractionInput()
    {
        Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetKeyDown(interactKey) && currentInteractable != null && Physics.Raycast(ray, out hit, interactionDistance, interactionLayer))
        {
            //Debug.Log("trying to do interaction");
            //do the interaction
            currentInteractable.OnInteract();
        }
        
    }
     private void FixedUpdate() {
        
        {
            Move();
            if (lastPosition != transform.position)
            {
            //means we've moved
                soundInstance.PlaySound(SoundManager.Sound.PlayerMove, transform.position);
            }
            lastPosition = transform.position;
        }
    }
    private void StateHandler()
    {
        //mode - sprinting
        if (grounded && Input.GetKey(KeyCode.LeftShift))
        {
            state = MovementState.sprinting;
            speed = runSpeed;
        }
        else if (grounded)
        {
            state = MovementState.walking;
            speed = walkSpeed;
        }
        //crouching
        if (Input.GetKeyDown(KeyCode.C))
        {
           state = MovementState.crouching;
           speed = crouchSpeed;
           
           transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
           rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
                
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
           transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);

        }
        //for our network
        if (state == MovementState.sprinting)
            speed = runSpeed;
    }
    public void CrawlChange()
        {
            crawl = !crawl;
        }
    public void Jump()
    {
        //Vector3 jumpForces = Vector3.zero;
        if (grounded)
        {
            //jumpForces = Vector3.up * jumpForce;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            grounded = false;
        }
        //rb.AddForce(jumpForces, ForceMode.VelocityChange);
    }
    void Move()
    {
        //find target velocity
        Vector3 currentVelocity = rb.velocity;
        Vector3 targetVelocity = new Vector3(move.x, 0, move.y);
        targetVelocity *=speed;

        //align direction
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocityLerp = Vector3.Lerp(targetVelocityLerp, targetVelocity, snappiness); 

        //calculate forces
         Vector3 velocityChange = (targetVelocityLerp - currentVelocity); 
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);

        //limit force
        Vector3.ClampMagnitude(velocityChange, maxForce);

        rb.AddForce(velocityChange, ForceMode.VelocityChange);

       /*  if (id != NetworkManager.Singleton.Client.Id) // Don't overwrite local player's forward direction to avoid noticeable rotational snapping
            transform.forward = forward; */
    }
    void Look()
    {
        //turn
        transform.Rotate(Vector3.up * look.x * sensitivity);

        //look
        lookRotation += (-look.y*sensitivity);
        lookRotation = Mathf.Clamp(lookRotation, -90f, 90f);
        cam.transform.eulerAngles = new Vector3(lookRotation, cam.transform.eulerAngles.y,cam.transform.eulerAngles.z);
    }
    // Start is called before the first frame update
   
    private void LateUpdate() {
       
        {Look();
        }
    }
    public void SetGrounded(bool state)
    {
        grounded = state;
    }
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, transform.localScale.y * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            //true if the angle is smaller than our maximum slope angle it's not 0
            return angle < maxSlopeAngle && angle!=0;
        }
        return false;
    }
    /* private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(transform.)
    }
 */
}
