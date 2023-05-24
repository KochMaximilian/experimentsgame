using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAnimationState : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    void Start() {
        animator = GetComponent<Animator>();
    }
    bool jump = false;
    bool move = false;
    public void Move() {
        move = true;
        UpdateAnimation();

    }
    public void Jump() {
        jump = true;
        UpdateAnimation();

    }
    public void Stand() {
        move = false;
        UpdateAnimation();

    }
    public void Land() {
        jump = false;
        UpdateAnimation();
    }

    void UpdateAnimation() {
        animator.SetBool("move", move);
        animator.SetBool("jump", jump);
    }
 
}
