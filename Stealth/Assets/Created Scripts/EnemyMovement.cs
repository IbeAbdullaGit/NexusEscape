using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    private float dirX;
    private float dirY;
    private float moveSpeed;
    private Rigidbody rb;
    private bool facingRight = false;
    private Vector3 localScale;

    // Start is called before the first frame update
    void Start()
    {
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
    void FixedUpdate()
    {
        rb.velocity = new Vector3(dirX*moveSpeed, rb.velocity.y, rb.velocity.z);
    }

    void LateUpdate() 
    {
        CheckWhereToFace();   
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
}
