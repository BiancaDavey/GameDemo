using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMoveTowardsAway : EnemyBase
{
    /*
        Functions to:
            *   Check whether a target is in range.
            *   If target is in range, initiate movement away from or towards target.
            *   Halt movement when target is in close range.
            *   Update animation based on target being out of range, in close range, or in range with movement.
    */
    public bool moveAway;
    // public float movementSpeed;  // Note: use if detach from EnemyBase class.
    public float inRangeRadius;
    public float closeRangeRadius;
    public Transform target;
    private Rigidbody2D myRigidBody;
    public Animator animator;

    void Start(){
        currentState = EnemyState.Idle;
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //  Set player as target.
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate(){
        CheckDistanceTarget();
    }

    void CheckDistanceTarget(){
        //  If player within range for chaseRun but outside attackInteract range, update animation, position and state.
        if ((Vector3.Distance(target.position, transform.position) <= inRangeRadius) && (Vector3.Distance(target.position, transform.position) > closeRangeRadius)){ 
                ChangeState(EnemyState.Move);
                animator.SetBool("Show", true);
                animator.SetBool("inRange", false);
                //  Move towards target if in range.
                if (!moveAway){
                    Vector3 temp = Vector3.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
                    UpdateAnimation(temp - transform.position);
                    myRigidBody.MovePosition(temp);
                }
                //  Move away from target if in range.
                else if (moveAway){
                    Vector3 temp = Vector3.MoveTowards(transform.position, target.position, -1 * movementSpeed * Time.deltaTime);
                    UpdateAnimation(temp - transform.position);
                    myRigidBody.MovePosition(temp);
                }
        }
        //  If player in range, update state.
        else if (Vector3.Distance(target.position, transform.position) <= closeRangeRadius){
            ChangeState(EnemyState.PlayerInRange);
            animator.SetBool("inRange", true);
        }
        //  If player outside attackInteract and chaseRun range, update state and animation.
        else {
            ChangeState(EnemyState.Idle);
            animator.SetBool("Show", false);
            animator.SetBool("inRange", false);
        }
        // }
    }

    //  Update animation parameters.
    private void UpdateAnimFloat(Vector2 setVector){
        animator.SetFloat("MovementX", setVector.x);
        animator.SetFloat("MovementY", setVector.y);
    }

    //  Update animation for movement according to direction.
    private void UpdateAnimation(Vector2 direction){
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)){
            if (direction.x > 0){
                UpdateAnimFloat(Vector2.right);
            }
            else if (direction.x < 0){
                UpdateAnimFloat(Vector2.left);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y)){
            if (direction.y > 0){
                UpdateAnimFloat(Vector2.up);
            }
            else if (direction.y < 0){
                UpdateAnimFloat(Vector2.down);
            }
        }
    }

    //  Update state.
    private void ChangeState(EnemyState newState){
        if (currentState != newState){
            currentState = newState;
        }
    }
}
