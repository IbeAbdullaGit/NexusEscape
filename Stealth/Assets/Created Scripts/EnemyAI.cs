using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform target;
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
       
    }
    void UpdateDestination()
    {
        targetMain = waypoints[waypointIndex].position;
        NavMeshAgent.SetDestination(targetMain);
        //going to a new place now, should turn
    }
    void IterateWaypointIndex()
    {
        waypointIndex++;
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }
  

    // Update is called once per frame
    void Update()
    {
       playerInSight = inSight();
      
        if (Vector3.Distance(transform.position, targetMain) <1 )
        {
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
        //DetectionMeter.instance.Meter();
        if(FOV.canSeePlayer)
        {
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
