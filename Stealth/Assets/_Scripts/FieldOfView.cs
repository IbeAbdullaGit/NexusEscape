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

    Light spotlight;

private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)  
   {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees*Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees*Mathf.Deg2Rad));
   }

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
        //get the light
        spotlight = GetComponentInChildren<Light>();
        //set the angle of the light to be the same as this angle
        spotlight.spotAngle = angle;
        //get the viewing angle
        Vector3 viewAngle01 = DirectionFromAngle(transform.eulerAngles.y, -angle / 2);
        //set the range of the spotlight to be the length of how far we can see
        spotlight.range = Vector3.Magnitude((transform.position + viewAngle01 * radius) - (transform.position) );
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
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if(rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position-transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle/2)
            {
                //could be seen
                float distanceToTagret = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTagret, obstructionMask))
                {
                    canSeePlayer = true;
                    //Debug.Log("Can see player!");   
                    //change light color
                    spotlight.color = Color.red;
                }
                else{
                    canSeePlayer = false;
                    //change light color
                    spotlight.color = Color.white;
                }
            }
            else{
                canSeePlayer = false;
                //change light color
                spotlight.color = Color.white;
            }
        }
        else if (canSeePlayer) //if previously in view of enemy, but not anymore
        {
            canSeePlayer = false;
            //change light color
            spotlight.color = Color.white;
        }
    }
  
}
