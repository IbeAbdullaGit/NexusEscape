using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySenses1 : MonoBehaviour
{
    public float viewRadius;
    public float viewAngle;

    public LayerMask targetPlayer;
    public LayerMask obstacleMask;

    private GameObject player;

    //hearing
    public float hearRadius;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        //THIS IS ALL FOR VISION, CAN RE-ENABLE IF WE NEED AN ADDITIONAL SOLUTION FOR THAT
        /* Vector3 playerTarget = (player.transform.position - transform.position).normalized;

        if (Vector3.Angle(transform.forward, playerTarget) < viewAngle/2)
        {
            float distanceToTarget = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToTarget < viewRadius)
            {
                if (Physics.Raycast(transform.position, playerTarget, distanceToTarget, obstacleMask) == false)
                {
                    //detected player
                    Debug.Log("I have seen you");
                }
            }
        } */

        float hearDistance = Vector3.Distance(transform.position, player.transform.position);
        //check if player is moving
        if (player.GetComponent<PlayerController>().state != PlayerController.MovementState.idle && hearDistance < hearRadius)
        {
            Debug.Log("Can hear you");
        }
    }
}
