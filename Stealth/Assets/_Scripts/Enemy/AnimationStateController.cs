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
        if (movement.GetState() == EnemyAI.MovementState.walking)
        {
            animator.SetTrigger("is_walking");
        }
        else if (movement.GetState() == EnemyAI.MovementState.looking)
        {
            animator.SetTrigger("is_looking");

        }
        else
        {
            animator.SetTrigger("is_seeking");
        }
    }
}
