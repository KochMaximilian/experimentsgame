using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    Rigidbody rigidbody;
    private Vector3 normal;
    public float jumpAnimationThreshold = 1.0f;
    private float passedTime;
    private float timeNeededToPass = 0.5f;
    private Vector3 startPosAnim;
    private Vector3 endPosAnim;
    public int jumps = 2;
    private int jumpsVar;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        jumpsVar = jumps;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && ((animator.GetBool("IsOnGround") == true || animator.GetBool("IsOnGroundWhileMoving")) || animator.GetBool("ExtraJumpsLeft") == true))
        {
            animator.SetTrigger("JumpTrigger");
            animator.SetBool("IsOnGround", false);
            animator.SetBool("IsOnGroundWhileMoving", false);
            jumpsVar -= 1;
            if (jumpsVar <= 0)
            {
                animator.SetBool("ExtraJumpsLeft", false);
            }
        }
        startPosAnim = rigidbody.transform.position;
        passedTime -= Time.deltaTime;
        if (passedTime < -timeNeededToPass)
        {
            endPosAnim = rigidbody.transform.position;
            passedTime = 0;
        }
        if ((startPosAnim.y - endPosAnim.y) > jumpAnimationThreshold || (startPosAnim.y - endPosAnim.y) < -jumpAnimationThreshold)
        {
            animator.SetBool("verticalMovementStopped", false);
        }
        else
        {
            animator.SetBool("verticalMovementStopped", true);
        }
        animator.SetFloat("VelocityY", rigidbody.velocity.y);
    }
    private void OnCollisionEnter(Collision collision)
    {
        normal = collision.GetContact(0).normal;
        if (normal.y > 0)
        {
            animator.ResetTrigger("JumpTrigger");
            animator.SetBool("IsOnGround", true);
            if (animator.GetBool("Moving") == true)
            {
                animator.SetBool("IsOnGroundWhileMoving", true);
            }
            animator.SetBool("ExtraJumpsLeft", true);
            jumpsVar = jumps;
        }
        #region Collisions
        /*
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.ResetTrigger("JumpTrigger");
            animator.SetBool("IsOnGround", true);
            if (animator.GetBool("Moving") == true)
            {
                animator.SetBool("IsOnGroundWhileMoving", true);
            }
        }
        */
        #endregion
    }



}
