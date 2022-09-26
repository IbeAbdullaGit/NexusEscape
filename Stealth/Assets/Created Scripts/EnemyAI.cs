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
    private float dirY;
    private float moveSpeed;
    private Rigidbody rb;
    private bool facingRight = false;
    private Vector3 localScale;

    bool playerInSight = false;

    void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody>();
        moveSpeed = 3f;

        dirX = -1f;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag =="Wall")
        {
            dirX *= -1f;
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
            rb.velocity = new Vector3(dirX*moveSpeed, rb.velocity.y, rb.velocity.z); 
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
        Vector3 PlayerSight = transform.position - target.position;
        float angle = Vector3.Angle(transform.forward, PlayerSight);
        if(Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
        {
            NavMeshAgent.SetDestination(target.position);
            Debug.DrawLine(transform.position, target.position, Color.red);
            return true;
        }

        return false;
    }
}
