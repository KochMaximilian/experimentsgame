using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FireMovement : MonoBehaviour
{
    public Rigidbody rb;

    public float speed;

    public bool allowSmoothJump = true;
    public float fallMultiplier = 2.5f;
    // when Jumpbutton is released and more gravity is applied, Player shall not be able to jump as high as earlier:
    public float lowJumpMultiplier = 2f;

    public float jumpHeight;
    public bool isGrounded;
    public bool canJump;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal_fire");
        float moveVertical = Input.GetAxis("Vertical_fire");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        if (Input.GetKey(KeyCode.UpArrow) && isGrounded && canJump)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            // vertical motion less then zero (falling):
            if (rb.velocity.y < 0 && allowSmoothJump)
            {
                // apply our fallmultiplier to our gravity
                rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
            isGrounded = false;
            canJump = false;
        }

    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Ground" && isGrounded == false)
        {
            isGrounded = true;
            canJump = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground" && isGrounded == true)
        {
            isGrounded = false;
            canJump = false;
        }
    }
}
