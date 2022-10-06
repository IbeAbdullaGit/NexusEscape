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
    private float dirZ;
    public float moveSpeed = 2.5f;
    private Rigidbody rb;
    private bool facingRight = false;
    private bool facingForward = false;
    private Vector3 localScale;

    bool playerInSight = false;

    public string movementPattern;

    bool leftToRight = false;
    bool forwardBack = false;

    bool dirSwitch = false;
    float storeX = -1f;
    float storeZ = -1f;

    void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody>();
        //moveSpeed = 2.5f;

        if (movementPattern == "LeftToRight") //simple back and forth
        {
            leftToRight = true;
            dirX = -1f;
        }
        else if (movementPattern =="Square"){ //square pattern
            forwardBack = true;
            dirX = -1f;
            dirZ = 0f;
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag =="Wall")
        {
            dirX *= -1f;
            if(forwardBack)
            {
                if (!dirSwitch) //moving up and down
                {
                    storeX = dirX;
                    dirX =0;
                    dirZ = -storeZ;
                    
                }
                else //moving left and right
                {
                    storeZ = dirZ;
                    dirZ =0;
                    dirX = -storeX;
                }
                dirSwitch = !dirSwitch;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerInSight = inSight();
        
    }
     void FixedUpdate()
    {
        if (!playerInSight) //not in sight
        {
            if (leftToRight)
            {
                rb.velocity = new Vector3(dirX*moveSpeed, rb.velocity.y, rb.velocity.z); 
            }
            else if (forwardBack)
            {
                rb.velocity = new Vector3(dirX*moveSpeed, rb.velocity.y, dirZ*moveSpeed); 
            }
        }
    }
    void LateUpdate() 
    {
        if (!playerInSight)
        {
            CheckWhereToFace();   
        }
    }
    void CheckWhereToFace(){

        if (leftToRight)
        {
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
        //IF THE SQUARE MOVEMENT PATTERN
        else if (forwardBack)
        {
            if (dirX >0)
            {
                facingRight = true;
            }
            else{
                facingRight = false;
            }
            if (dirZ >0)
            {
                facingForward = true;
            }
            else{
                facingForward = false;
            }

            if (!dirSwitch)
            {if (((facingRight) && (localScale.x <0)) || ((!facingRight) && (localScale.x >0)))
            {
                localScale.x *= -1f;
            }
            }
            else
            {if (((facingForward) && (localScale.z <0)) || ((!facingForward) && (localScale.z >0)))
            {
                localScale.z *= -1f;
            }
            }
            transform.localScale = localScale;
        }

    }

    bool inSight()
    {
        Vector3 PlayerSight = transform.position - target.position;
        float angle = Vector3.Angle(transform.forward, PlayerSight);
        if(Mathf.Abs(angle) > 70 && Mathf.Abs(angle) < 290)
        {
            NavMeshAgent.SetDestination(target.position);
            Debug.DrawLine(transform.position, target.position, Color.red);
            return true;
        }

        return false;
    }

   
}
