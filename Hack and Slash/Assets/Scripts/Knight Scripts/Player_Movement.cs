using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
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

    // roll variables
    public float rememberRollFor;

    // shield variables
    bool isShieldUp = false;

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
        Shield();

        // releases shield after mouse is released
        if (Input.GetMouseButtonUp(1))
        {
            playerAnim.speed = 1;
        }
    }

    // movement method: Rolling and Running
    void Move()
    {

        // player can't move if shield is up
        if (isShieldUp == false)
        {
            float x = Input.GetAxisRaw("Horizontal");
            moveBy = x * speed;
            rb.velocity = new Vector2(moveBy, rb.velocity.y);
        }

        if (moveBy < 0 && facingRight)
        {
            Flip();
            
        }
        else if (moveBy > 0 && !facingRight)
        {
            Flip();
        }

        // checking if roll is done and changing the animator condition based on the time
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerAnim.SetBool("IsRolling", true);
        }
    }

    // method that flips sprite
    void Flip()
    {
        facingRight = !facingRight;
        /*Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;*/

        transform.Rotate(0f, 180f, 0f);
    }

    // changes the animation bool at the end from the event 
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
        if (rb.velocity.y < 0 )
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.W))
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
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

    // Starts shield animation 
    void Shield()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            playerAnim.SetBool("ShieldUp", true);
            isShieldUp = true;
        }
    }

    // keeps the shield up if the button is still pressed
    void KeepShieldUp()
    {

        if (Input.GetKey(KeyCode.Mouse1))
        {
            playerAnim.speed = 0.0f;
        }
    }

    // puts shield away at the end of the animation
    void ShieldDown()
    {
        playerAnim.SetBool("ShieldUp", false);
        isShieldUp = false;
    }



    // Falling on Spikes
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spikes"))
        {
            playerAnim.SetBool("IsDead", true);

            // makes shield up true so player can't move
            isShieldUp = true;
        }
    }
}
