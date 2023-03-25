using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    PlayerController player;

    Animator animator;
    bool isCrouching = false;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //check states
        //first check if crouching, set bool
        if (player.state == PlayerController.MovementState.crouching)
        {
            animator.SetBool("crouch", true);
            isCrouching = true;
        }
        else
        {
            animator.SetBool("crouch", false);
            isCrouching = false;
        }
        if (!isCrouching)
       {
        switch (player.state)
        {
            case PlayerController.MovementState.idle:
                animator.SetTrigger("walkingIdle");
                break;
            case PlayerController.MovementState.walking:
                //nested switch, check which direction
                switch (player.direction)
                {
                    case PlayerController.MovementDirection.forward:
                        animator.SetTrigger("walkingForward");
                        break;
                    case PlayerController.MovementDirection.backward:
                        animator.SetTrigger("walkingBackward");
                        break;
                    case PlayerController.MovementDirection.left:
                        animator.SetTrigger("walkingLeft");
                        break;
                    case PlayerController.MovementDirection.right:
                        animator.SetTrigger("walkingRight");
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
       }
       else //is crouching
       {
        switch (player.state)
        {
            case PlayerController.MovementState.idle:
                animator.SetTrigger("crouchIdle");
                break;
            case PlayerController.MovementState.walking:
                //nested switch, check which direction
                switch (player.direction)
                {
                    case PlayerController.MovementDirection.forward:
                        animator.SetTrigger("crouchForward");
                        break;
                    case PlayerController.MovementDirection.backward:
                        animator.SetTrigger("crouchBackward");
                        break;
                    case PlayerController.MovementDirection.left:
                        animator.SetTrigger("crouchLeft");
                        break;
                    case PlayerController.MovementDirection.right:
                        animator.SetTrigger("crouchRight");
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
       }
    }
}
