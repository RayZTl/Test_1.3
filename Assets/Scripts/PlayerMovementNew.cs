using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
public class PlayerMovementNew : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float JumpHeight = 16f;

    [SerializeField] private float wallSlideSpeed = 2f;//ray
    [SerializeField] private float wallJumpForce = 20f;//ray

    private float horizontal;
    private bool isJumping;

    private bool isWallSliding;//ray

    private bool isFacingRight = true;
    public Animator Anim;
    private bool isGrounded;
    private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;

    [SerializeField] private Transform wallCheck;//ray

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private LayerMask wallLayer;//ray

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        isJumping = Input.GetButtonDown("Jump"); //ray

        AnimateMove(); 

        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);//ray

        bool isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);

        

        isWallSliding = !isGrounded && isTouchingWall && horizontal != 0f;//ray 1
        if (isJumping)
        {
            if (isGrounded)
            {
                Jump(JumpHeight);
                
            }
            else if (isWallSliding)
            {
                WallJump();
            }
        }
    }//end  1 ray

    void AnimateMove()
    {
        if (horizontal >= 0.1f || horizontal <= -0.1f)
        {
            Anim.SetBool("isRunning", true);
        }
        else
        {
            Anim.SetBool("isRunning", false);
        }
    }


    private void FixedUpdate()
    {
        float moveSpeed = isWallSliding ? speed * 0.5f : speed; // ray 2 
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }

        Flip(horizontal);
    } // end 2 ray


    private void Jump(float JumpHeight )
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Vector2 direction = new(0, 1);
            rb.velocity = direction * JumpHeight;
        }

    }


    private void WallJump() // ray 3
    {
        float wallDirection = isFacingRight ? 1f : -1f;
        rb.velocity = new Vector2(-wallDirection * wallJumpForce, JumpHeight);
        Flip(-wallDirection);
    } // end 3 ray



    private void Flip(float direction) 
    {
        if (direction < 0 && isFacingRight || direction > 0 && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {

            isGrounded = true;
            Debug.Log("isGrounded");
            Anim.SetBool("isJumping", false);

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            Debug.Log("no");
            Anim.SetBool("isJumping", true);
        }
    }
}

/*{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float JumpHeight = 16f;
    [SerializeField] private float wallSlideSpeed = 2f;//1
    [SerializeField] private float wallJumpForce = 20f;//2  
    [SerializeField] private Transform wallCheck;//4
    [SerializeField] private LayerMask wallLayer;//5
    private bool isWallSliding;//3
    private Rigidbody2D rb;
    private float horizontal;
    private bool isFacingRight = true;
    private bool isGrounded;
    public Animator Anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        Movement();//fungsi Berjalan
        Jump();//fungsi loncat
        Flip();//fungsi Berputar
        AnimateMove(); // fungsi animasi berjalan
        bool isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);//6

    }

    private void Movement()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    void AnimateMove()
    {
        if (horizontal >= 0.1f || horizontal <= -0.1f)
        {
            Anim.SetBool("isRunning", true);
        }
        else
        {
            Anim.SetBool("isRunning", false);
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Vector2 direction = new Vector2(0, 1);
            rb.velocity = direction * JumpHeight;
        }

    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {

            isGrounded = true;
            Debug.Log("isGrounded");
            Anim.SetBool("isJumping", false);

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            Debug.Log("no");
            Anim.SetBool("isJumping", true);
        }
    }

}*/

