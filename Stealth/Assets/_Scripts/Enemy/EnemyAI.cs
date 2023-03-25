using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Transform target;
    NavMeshAgent NavMeshAgent;
    public float alertness= 5f;


    private float dirX;

    public float moveSpeed;
    private Rigidbody rb;
    private bool facingRight = false;
    private Vector3 localScale;

    bool playerInSight = false;

    public Transform[] waypoints;
    int waypointIndex = 0;
    Vector3 targetMain;

    FieldOfView FOV;

    DetectionMeter detectionMeter;

    public WaitForSeconds delay = new WaitForSeconds(1f);
    public WaitForSeconds distract = new WaitForSeconds(5f);

    public bool opendoor = true;
    public bool hasdoor = false;
    public GameObject _otherdoor;

    public bool distracted = false;

    SoundManager soundInstance;
    bool playSoundOnce = false;
 
    //hearing
    Vector3 lastPosition;

     float hearRadius;
     bool canHear;

    //networking stuff
    private bool connected = false;
    public bool networking = false;

    //ui
    [SerializeField]private Slider slider;

    public enum MovementState
    {
        looking,
        seeking,
        walking
    }
    MovementState currentState;
    
    void SetMovementState(MovementState m)
    {
        currentState = m;
    }
    public MovementState GetState()
    {
        return currentState;
    }

    void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        FOV = GetComponent<FieldOfView>();
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody>();

        if (!networking)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            UpdateDestination();
        }
      
        lastPosition = transform.position;

        //moveSpeed = 2.5f;

       
        dirX = -1f;

       

        //detectionMeter = GameObject.FindGameObjectWithTag("GameController").GetComponent<DetectionMeter>();
        //soundInstance = GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundManager>().instance;

        //make hear radius same as view radius?
        hearRadius = (GetComponent<FieldOfView>().radius);
       
    }
    public void SetTarget()
    {
         target = GameObject.FindGameObjectWithTag("Player").transform;

        UpdateDestination();
        connected = true;
    }
    void UpdateDestination()
    {
        
            targetMain = waypoints[waypointIndex].position;
            NavMeshAgent.SetDestination(targetMain);
            NavMeshAgent.speed = moveSpeed;
            //going to a new place now, should turn
            // //add pause when switching between points
            // NavMeshAgent.isStopped = true;

            // NavMeshAgent.isStopped = false;
        

        if(opendoor == false)
        {
            NavMeshAgent.speed = 0;
        }

        

        SetMovementState(MovementState.looking);
        StartCoroutine(WalkPause());
    }
    void IterateWaypointIndex()
    {
        waypointIndex++;
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
        
    }
   
    private IEnumerator WalkPause()
    {
        NavMeshAgent.isStopped = true;
        //play pause
        SetMovementState(MovementState.looking);
       // Debug.Log("Should be looking");
        //wait
        //Debug.Log("Waiting");
        yield return delay;
        //after waiting
        NavMeshAgent.isStopped = false;    
        //Debug.Log("Done waiting");  
        //move again
        SetMovementState(MovementState.walking);
          
    }
    public IEnumerator DistractionDelay()
    {
        //wait for delay time
        yield return delay;

        //set destination back to normal
        targetMain = waypoints[waypointIndex].position;
        NavMeshAgent.SetDestination(targetMain);
    }
    public IEnumerator Distracted(GameObject distraction)
    {
        Debug.Log("Starting Distraction");
        //stop current coroutines
        StopCoroutine(WalkPause());

        //flag to indicate distraction
        distracted = true;
        
        //stop any coroutine that might be conflicting with this one, we want enemy to immediately walk over
        //StopCoroutine(WalkPause());
        //make sure they can walk
         NavMeshAgent.isStopped=false;
         //give them destination to go to
        NavMeshAgent.SetDestination(distraction.transform.position);
       
        //enemy should not do anything else but just move, rest of AI is turned off
        
        yield return distract;

        //afterwards, destroy the distraction
        //Destroy(distraction); //Disable if objectpooling
        distraction.SetActive(false); //Used for object pooling, a bit buggy. Prefer if we use this and fix. Enable object pooling under Player's script.

        //set destination back to normal
        //targetMain = waypoints[waypointIndex].position;
        //NavMeshAgent.SetDestination(targetMain);

        distracted = false;

        StartCoroutine(DistractionDelay());
    }
  

    // Update is called once per frame
    void Update()
    {
        if (connected || !networking)
       { 
        if (hasdoor)
        {
            if (_otherdoor.GetComponent<Door>().isOpen == true)
            {
                opendoor = true;
                NavMeshAgent.speed = moveSpeed;
            }
        }
        //detection meter
        if (slider != null && !distracted && opendoor)
       {
            DetectionMeterUpdate();
       }
       
       if (!distracted)
       {
            //vision test then hearing test
            playerInSight = inSight();
            HearingCheck();
           // Debug.Log(Vector3.Distance(transform.position, targetMain));
           //need more leniency here
            if (Vector3.Distance(transform.position, targetMain) <1.5f )
            {
                //detectionMeter.Meter();
                IterateWaypointIndex();
                UpdateDestination();
                
                //Debug.Log("Switching path");
            }
            //Debug.Log(NavMeshAgent.pathStatus);
            
            //reset path
            /* if (NavMeshAgent.isStopped)
            {
                NavMeshAgent.enabled = false;
                NavMeshAgent.enabled = true;
                IterateWaypointIndex();
                UpdateDestination();
            }  */
       }
       else //distracted
       {
            SetMovementState(MovementState.looking);
       }
       //check for moving
        if (transform.position != lastPosition && !playerInSight)
        {
            //dont play for now
            //soundInstance.PlaySound(SoundManager.Sound.EnemyMove, transform.position);
            //SetMovementState(MovementState.walking);
        }
        
       /* 
       
            
        lastPosition = transform.position; */
        //get unstuck
       /* if (!NavMeshAgent.hasPath && NavMeshAgent.pathStatus == NavMeshPathStatus.PathComplete) {
             Debug.Log("Character stuck");
             NavMeshAgent.enabled = false;
             NavMeshAgent.enabled = true;
             Debug.Log("navmesh re enabled");
             // navmesh agent will start moving again
        }    */
       }
    }
 
    void CheckWhereToFace(){
        
        if (dirX >0)
            {
                facingRight = true;
            }
            else{
             facingRight = false;
            }

        if (((facingRight) && (localScale.x <0)) || ((!facingRight) && (localScale.x >0)))
        {
            localScale.x *= -1f;
        }
        transform.localScale = localScale;
        

    }

      private void HearingCheck()
    {
        float hearDistance = Vector3.Distance(transform.position, target.position);
        float currentHearRadius;
        //check if player is crouching first; the hearing should be reduced if this is the case
        if (target.GetComponent<PlayerController>().state != PlayerController.MovementState.crouching)
        {
            currentHearRadius = hearRadius;
        }
        else //is crouching
        {
            currentHearRadius = hearRadius/3; //decrease the radius, making enemy "hear less"
        }
      /*   //check if player is moving
        if (target.GetComponent<PlayerController>().state != PlayerController.MovementState.idle && hearDistance <= currentHearRadius)
        {
            Debug.Log("Can hear you");
            //use this for now, can hear
            canHear = true;

        }
        else{
            //dont want to get stuck "hearing"
            canHear = false;
        } */

        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, currentHearRadius, GetComponent<FieldOfView>().targetMask);

        if(rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform; //will always be the player
            Vector3 directionToTarget = (target.position-transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < 360) //hearing is 360 degrees
            {
                //could be heard?
                if (!Physics.Raycast(transform.position, directionToTarget, hearDistance, GetComponent<FieldOfView>().obstructionMask) && ((target.GetComponent<PlayerController>().state != PlayerController.MovementState.idle)))
                {
                     Debug.Log("Can hear you");
                    //use this for now, can hear
                    canHear = true;
                }
                else
                    canHear = false;
            }
            else
                canHear = false;
        }
        else if (canHear)
             canHear = false; //this is so it doesn't get stuck hearing
    }
    bool inSight()
    {
       //if we can see the player or hear the player
        if(FOV.canSeePlayer || canHear)
        {
            //Debug.Log("Enemy AI sees player");
           //stop coroutine, so enemy can move
           StopCoroutine(WalkPause());
           //make sure enemy can move
           NavMeshAgent.isStopped = false; 

            targetMain = target.position;
            NavMeshAgent.SetDestination(target.position);
            Debug.DrawLine(transform.position, target.position, Color.red);

            //play sound
            if (!playSoundOnce)
            {
                //soundInstance.PlaySound(SoundManager.Sound.EnemyDetect, transform.position);
                playSoundOnce = true;
                //seeking animation
                SetMovementState(MovementState.seeking);
            }
            return true;
        }
        else{
            //reset sound
            playSoundOnce = false;
            SetMovementState(MovementState.walking); //walking back
        }
        targetMain = waypoints[waypointIndex].position;
        NavMeshAgent.SetDestination(targetMain);
        return false;
    }

    void DetectionMeterUpdate()
    {
         //distance between player and the AI
         float distanceToTagret = Vector3.Distance(transform.position, target.position);
         //Debug.Log(distanceToTagret);
        
        //this is the perent of how close the player is
        float sliderValue = (hearRadius/distanceToTagret);
         //float sliderValue = ((hearRadius/2)/distanceToTagret);


        //this means it should be full 100%, is seen/heard
        if (distanceToTagret < hearRadius)
        {
            slider.value = 1;
        }
        else //need to fill in some percentage
        {
            slider.value = sliderValue;
        }
        //Debug.Log(sliderValue);
    }

   
}
