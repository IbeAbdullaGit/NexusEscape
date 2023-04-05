using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject cam;

    [SerializeField]
    private GameObject head;

    public float walkSpeed, sensitivity, maxForce, jumpForce, snappiness;
    public float runSpeed;
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;
    bool crawl;
    float speed;
    
    public Vector2 move, look;
     private Vector3 targetVelocityLerp;
    private float lookRotation;

    public bool grounded;

    bool runOnce = false;
    
    public MovementState state;
    bool paused = false;
    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        idle
    }
    public MovementDirection direction;
    public enum MovementDirection
    {
        forward,
        backward,
        left,
        right,
        none
    }
    [Header("Interaction")]
    [SerializeField]private float interactionDistance = default;
    [SerializeField]private LayerMask interactionLayer = default;
    private Interactable currentInteractable;
    [SerializeField] private bool canInteract = true;
    [SerializeField] private KeyCode interactKey = KeyCode.Mouse0;
    private Camera playerCam;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;

    public Canvas GameOverScreen;

    //SoundManager soundInstance;
    [SerializeField]
    private OperatorPlayerSounds playerSounds;

    Vector3 lastPosition;   

     public void OnMove(InputAction.CallbackContext context)
    {
        
        {
            move = context.ReadValue<Vector2>();
            //set default direction
            direction = MovementDirection.none;
           
            //determine direction
            if (move == Vector2.up) //forward
            {
                direction = MovementDirection.forward;
            }
            if (move == Vector2.down) //backward
            {
                direction = MovementDirection.backward;
            }
            if (move == Vector2.left) //left
            {
                direction = MovementDirection.left;
            }
            if (move == Vector2.right)//right
            {
                direction = MovementDirection.right;
            }
            //play moving sound on input
            //soundInstance.PlaySound(SoundManager.Sound.PlayerMove, transform.position);
        }
    }
    public void OnLook(InputAction.CallbackContext context)
    {
       
        {
            look = context.ReadValue<Vector2>();
        }
    }
     public void OnJump(InputAction.CallbackContext context)
    {
      
       {
            Jump();
            //play moving sound on input
            //soundInstance.PlaySound(SoundManager.Sound.PlayerMove, transform.position);
       }
    }
     void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
         //distanceToGround = GetComponent<Collider>().bounds.extents.y;
         startYScale = transform.localScale.y;
         playerCam = GetComponentInChildren<Camera>();

         //ASSIGN LATER
         //GameOverScreen.enabled = false;

         //soundInstance = GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundManager>().instance;
         lastPosition = transform.position;
         
    }
    private void Update() {

        if (!runOnce)
        {
            runOnce = true;
             //save at the start of the level
            //GameObject.FindGameObjectWithTag("GameController").GetComponent<SavePlugin>().SaveItems();
        }
       { 
        StateHandler();
        //NEW, CHECK FOR CROUCHING
        if (crawl)
        {
            speed = crouchSpeed;
        }
        //REDO HOW WE DO GROUNDING
        //grounded = Physics.Raycast(transform.position, -Vector3.up, distanceToGround);
       }

       if (canInteract)
         {
            HandleInteractionCheck();
            HandleInteractionInput();
         }

        if (paused)
        {
            //enabled cursor
           Cursor.lockState = CursorLockMode.None;
           Cursor.visible = true;
        }
        
    }
    private void HandleInteractionCheck()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
            if (lastPosition != transform.position && grounded)
            {
            //means we've moved
                //soundInstance.PlaySound(SoundManager.Sound.PlayerMove, transform.position);
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

            //play running sound
        }
        //moving
        else if (grounded && lastPosition != transform.position && state != MovementState.crouching)
        {
            state = MovementState.walking;
            speed = walkSpeed;

            //play walking sound, pass in position
        }
        else //not moving
        {
            state = MovementState.idle;
        }

        //crouching
        if (Input.GetKey(KeyCode.LeftControl))
        {
            state = MovementState.crouching;
            speed = crouchSpeed;
            Debug.Log("crouching");

            //transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            //rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

            //play crouching sound

        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            //transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            speed = walkSpeed;
            state = MovementState.walking;
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
    }
    void Look()
    {
        //turn
        //transform.Rotate(Vector3.up * look.x * sensitivity);

        //look
        //lookRotation += (-look.y*sensitivity);
        //lookRotation = Mathf.Clamp(lookRotation, -90f, 90f);
        //cam.transform.eulerAngles = new Vector3(lookRotation, cam.transform.eulerAngles.y,cam.transform.eulerAngles.z);

        //"look.x and y" were really buggy

        lookRotation -= Input.GetAxis("Mouse Y") * sensitivity;
        lookRotation = Mathf.Clamp(lookRotation, -90f, 90f);

        cam.transform.localRotation = Quaternion.Euler(lookRotation, 0f, 0f);
        if (head != null)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, head.transform.position.y, cam.transform.position.z); //Move Y axis to head
        }
        transform.Rotate(Vector3.up * (Input.GetAxis("Mouse X") * sensitivity));

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
    private void OnCollisionEnter(Collision other) {

        //Debug.Log("Colliding");
        if (other.collider.tag =="Enemy")
        {
            //this is the end
           GameOverScreen.enabled = true;
           Debug.Log("Game over!");
           //enabled cursor
           Cursor.lockState = CursorLockMode.None;
           Cursor.visible = true;
           //disable any other menus
           //GameObject.FindGameObjectWithTag("GameController").GetComponent<CreateDialog>().enabled = false;
           //pause game
           Time.timeScale =0;
           paused = true;
           //soundInstance.PlaySound(SoundManager.Sound.PlayerDie, transform.position);
        
        }
    }

    private void PlayFootstep()
    {
        playerSounds.PlayFootsteps();
        //Debug.Log("playing footstep");
    }

    private void PlaySilentFootstep()
    {
        playerSounds.PlaySilentFootstep();
        //Debug.Log("playing footstep");
    }
}
