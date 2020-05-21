using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // fields
    // player components
    public Rigidbody2D rb;
    public Animator playerAnim;

    // movement speed
    public float speed;
    float moveBy;

    // jump variables
    public float jumpForce;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float rememberGroundedFor;
    float lastTimeGrounded;

    // ground check variables
    bool isGrounded = false;
    public Transform isGroundCheckered;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        // running run animation if player is moving and on the ground
        if (isGrounded)
        {
            playerAnim.SetFloat("Speed", Mathf.Abs(moveBy));
        }

        Move();
        GroundChecker();
        Jump();
        BetterJump();
    }

    // movement method
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);


        
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void BetterJump()
    {
        if (rb.velocity.y < 0 )
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.W))
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void GroundChecker()
    {
        Collider2D collider = Physics2D.OverlapCircle(isGroundCheckered.position, checkGroundRadius, groundLayer);

        if (collider != null)
        {
            isGrounded = true;
            playerAnim.SetBool("IsJumping", false);
        }
        else
        {
            if (isGrounded)
            {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
            playerAnim.SetBool("IsJumping", true);
        }
    }
}
