using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController2D : MonoBehaviour
{
    // Start is called before the first frame update

    Animator animator;
    Rigidbody2D rd2d;
    SpriteRenderer sprd;
    bool isGrounded;
    public bool canMove = true;

    [SerializeField]
        Transform groundCheck;
    [SerializeField]
        Transform groundCheckR;
    [SerializeField]
        Transform groundCheckL;

    [SerializeField]
        private float runSpeed = 10.0f;

    [SerializeField]
        private float jumpSpeed = 15.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rd2d = GetComponent<Rigidbody2D>();
        sprd = GetComponent<SpriteRenderer>();

        
    }

    // Update is called once per frame
    private void FixedUpdate()

    {
        if (canMove)
        {
            if ((Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))) ||
                     (Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground"))) ||
                      (Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground"))))
            {
                isGrounded = true;

            }
            else
            {
                isGrounded = false;
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack_JumpKick"))
                {

                }
                else
                {
                    animator.Play("attack_Jump");
                }

            }
            if (Input.GetKey("d") || Input.GetKey("right"))
            {
                rd2d.velocity = new Vector2(runSpeed, rd2d.velocity.y);
                if (isGrounded)
                    animator.Play("player_run");
                transform.eulerAngles = new Vector3(0, 0, 0);


            }
            else if (Input.GetKey("a") || Input.GetKey("left"))
            {
                rd2d.velocity = new Vector2(-10, rd2d.velocity.y);
                if (isGrounded)
                    animator.Play("player_run");

                transform.eulerAngles = new Vector3(0, 180, 0);

            }
            else
            {
                if (isGrounded)
                {
                    if ((animator.GetCurrentAnimatorStateInfo(0).IsName("attack_Kick")) || (animator.GetCurrentAnimatorStateInfo(0).IsName("attack_leftPunch"))
                        || (animator.GetCurrentAnimatorStateInfo(0).IsName("attack_rightPunch")))
                    {

                    }
                    else
                    {
                        animator.Play("idle");
                    }
                    rd2d.velocity = new Vector2(0, rd2d.velocity.y);

                }
            }
            if ((Input.GetKey("w") || Input.GetKey("up")) && isGrounded)
            {

                animator.Play("player_jump");
                rd2d.velocity = new Vector2(rd2d.velocity.x, jumpSpeed);

            }
        }
    }
}
