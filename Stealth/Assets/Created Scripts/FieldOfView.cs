using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef; 

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;
    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        //StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);
        while(true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        //Debug.Log("Doing Check");
        
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if(rangeChecks.Length != 0)
        {
            Debug.Log("Range checks: " + rangeChecks.Length);
            
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position-transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle/2)
            {
                //could be seen
                float distanceToTagret = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTagret, obstructionMask))
                {
                    canSeePlayer = true;
                    Debug.Log("Can see player!");
                }
                else{
                    canSeePlayer = false;
                }
            }
            else{
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer) //if previously in view of enemy, but not anymore
        {
            canSeePlayer = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //tryi doing this in update instead
        FieldOfViewCheck();
    }
}
