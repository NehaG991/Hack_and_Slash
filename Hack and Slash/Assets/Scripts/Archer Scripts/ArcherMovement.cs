using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMovement : MonoBehaviour
{
    // fields
    // player components
    public Rigidbody2D rb;
    public Animator playerAnim;
    public SpriteRenderer sprite;

    // movement variables
    public float speed;
    float moveBy;
    bool facingRight = true;

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
        // sets animator speed back to normal if grounded
        if (isGrounded)
        {
            playerAnim.speed = 1;
        }

        // setting player animation based on current speed
        playerAnim.SetFloat("Speed", Mathf.Abs(moveBy));

        // calling movement methods
        Move();
        GroundChecker();
        Jump();
        BetterJump();

    }

    // move method
    void Move()
    {

        // getting movement value and applying it to the player
        float x = Input.GetAxisRaw("Horizontal");
        moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);

        // flipping sprite based on direction
        if (moveBy < 0 && facingRight)
        {
            Flip();
        }
        else if (moveBy > 0 && !facingRight)
        {
            Flip();
        }

        // playing roll animation if 's' key is pressed down
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerAnim.SetBool("IsRolling", true);
        }
    }

    // Flip method to flip sprite
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    // animation event: Setting IsRolling bool to false at end of animation
    void RollEnded()
    {
        playerAnim.SetBool("IsRolling", false);
    }

    // jump method
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    // makes jump look more realistic
    void BetterJump()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.W))
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    // Animation Event: Holds falling Sprite until player hits ground
    void StillJumping()
    {
        if (isGrounded == false)
        {
            playerAnim.speed = 0;
        }
    }

    // checks if the player is on the ground to jump
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

    // falling on spikes method
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spikes"))
        {
            playerAnim.SetBool("IsDead", true);
            this.GetComponent<ArcherMovement>().enabled = false;
        }
    }

}
