using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distraction : MonoBehaviour
{
    public float radius;

    public float angle = 360; //hazard will see all around itself

     public GameObject enemyRef; 

    //this will be the enemy
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    
    public bool canSeeEnemy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);
        while(true)
        {
            yield return wait;
            FieldOfViewCheck();
            if (canSeeEnemy)
            {
                //should only run this once per hazard, since after this will be destroyed
                //sending in this game object to run the enemy ref's distracted function
                //stop any coroutines that might be conflicting
                //StopCoroutine(enemyRef.GetComponent<EnemyAI>().WalkPause());
                StartCoroutine(enemyRef.GetComponent<EnemyAI>().Distracted(gameObject));
                //enemyRef.GetComponent<EnemyAI>().StartCoroutine(enemyRef.GetComponent<EnemyAI>().Distracted(gameObject));
            }
            
        }
    }
    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if(rangeChecks.Length != 0)
        {
            //for now this just takes the first enemy in range, may need to change later to fit many enemies
            Transform target = rangeChecks[0].transform;
            //get enemy gameobject?
            enemyRef = rangeChecks[0].gameObject;


            Vector3 directionToTarget = (target.position-transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle/2)
            {
                //could be seen
                float distanceToTagret = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTagret, obstructionMask))
                {
                    canSeeEnemy = true;
                    Debug.Log("Can see enemy!");
                }
                else{
                    canSeeEnemy = false;
                }
            }
            else{
                canSeeEnemy = false;
            }
        }
        /* else if (canSeeEnemy) //if previously in view of enemy, but not anymore
        {
            canSeeEnemy = false;
        } */

       
    }
}