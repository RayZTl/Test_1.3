using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float JumpHeight;

    Rigidbody2D rb;
    private bool isGrounded;

    private int airCount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
       
    }

    private void Move()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), 0);
        transform.Translate(direction * Time.deltaTime * speed);

    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Vector2 direction = new Vector2(0, 1);
            rb.velocity = direction * JumpHeight;
            
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Ground")
        {
            //airCount = 0;
            isGrounded = true;
            Debug.Log("isGrounded");

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            isGrounded = false;
            Debug.Log("no");
        }
    }
}
