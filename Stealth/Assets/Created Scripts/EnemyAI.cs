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

    void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        inSight();
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
