using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Transform target;
    NavMeshAgent NavMeshAgent;
    public float alertness= 5f;


    private float dirX;
   
    public float moveSpeed = 2.5f;
    private Rigidbody rb;
    private bool facingRight = false;
    private Vector3 localScale;

    bool playerInSight = false;

    public Transform[] waypoints;
    int waypointIndex;
    Vector3 targetMain;

    FieldOfView FOV;

    DetectionMeter detectionMeter;

    WaitForSeconds delay = new WaitForSeconds(2f);

    void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        FOV = GetComponent<FieldOfView>();
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody>();

        target = GameObject.FindGameObjectWithTag("Player").transform;

        //moveSpeed = 2.5f;

       
        dirX = -1f;

        UpdateDestination();

        detectionMeter = GameObject.FindGameObjectWithTag("GameController").GetComponent<DetectionMeter>();
       
    }
    void UpdateDestination()
    {
        targetMain = waypoints[waypointIndex].position;
        NavMeshAgent.SetDestination(targetMain);
        //going to a new place now, should turn
        // //add pause when switching between points
        // NavMeshAgent.isStopped = true;
       
        // NavMeshAgent.isStopped = false;
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
        //wait
        yield return delay;
        //after waiting
        NavMeshAgent.isStopped = false;      
          
    }
  

    // Update is called once per frame
    void Update()
    {
       playerInSight = inSight();
      
        if (Vector3.Distance(transform.position, targetMain) <1 )
        {
            detectionMeter.Meter();
            IterateWaypointIndex();
            UpdateDestination();
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

    bool inSight()
    {
       
        if(FOV.canSeePlayer)
        {
           //stop coroutine, so enemy can move
           StopCoroutine(WalkPause());
           //make sure enemy can move
           NavMeshAgent.isStopped = false; 

            //targetMain = target.position;
            NavMeshAgent.SetDestination(target.position);
            Debug.DrawLine(transform.position, target.position, Color.red);
            return true;
        }
        targetMain = waypoints[waypointIndex].position;
        NavMeshAgent.SetDestination(targetMain);
        return false;
    }

   
}
