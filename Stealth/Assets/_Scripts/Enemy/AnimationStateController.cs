using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    
    Animator animator;
    EnemyAI movement;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (movement.GetState())
        {
            case EnemyAI.MovementState.looking:
                animator.SetTrigger("is_looking");
                Debug.Log("Is looking");
                break;
            case EnemyAI.MovementState.seeking:
                animator.SetTrigger("is_seeking");
                break;
            case EnemyAI.MovementState.walking:
                animator.SetTrigger("is_walking");
                break;
            default:
                break;
        }
        
    }
}
