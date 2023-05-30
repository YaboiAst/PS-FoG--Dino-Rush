using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{  
    [Header ("Movement Parameters")]
    public float moveSpeed = 500f;
    public float jumpForce = 8f;
    public float dashForce = 10f;
    private bool hasDoubleJump = true;
    private Rigidbody2D rb;

    [Header ("Ground Check")]
    public LayerMask whatIsGround;
    public Transform groundCheck1, groundCheck2;
    private bool isGrounded;

    [Header ("Coyotte Time")]
    public float hangTime = 0.2f;
    private float hangCounter;

    [Header ("Jump Buffer")]
    public float jumpBuffer = 0.1f;
    private float jumpBufferCounter;

    [Header ("Dash Cooldown")]
    public float dashCooldown = 0.5f;
    private float dashCooldownCounter;
    private bool canDash;

    [Header ("Orb Position Control")]
    public Transform orbPos;
    public Transform orbPos1, orbPos2;


    [Header ("Visuals")]
    private SpriteRenderer sprite;
    private Animator animator;
    public float isFacing = 1f;
    private bool toRight = true;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, rb.velocity.y);
        if(rb.velocity.x != 0) animator.SetBool("isMoving", true);
        else animator.SetBool("isMoving", false);

        isGrounded = Physics2D.OverlapCircle(groundCheck1.position, .1f, whatIsGround) || Physics2D.OverlapCircle(groundCheck2.position, .1f, whatIsGround);

        if(Input.GetAxisRaw("Horizontal") != 0)
            isFacing = Input.GetAxisRaw("Horizontal");
        if(isGrounded && !hasDoubleJump) hasDoubleJump = true;

        if(isFacing == -1 && toRight){
            sprite.flipX = true;
            toRight = false;
        }
        else if(isFacing == 1 && !toRight){
            sprite.flipX = false;
            toRight = true;
        }

        // Coyotte Time
        if(isGrounded) 
            hangCounter = hangTime;
        else 
            hangCounter -= Time.deltaTime;

        // Jump Buffer
        if(Input.GetButtonDown("Jump") && (hangCounter > 0f || hasDoubleJump))
            jumpBufferCounter = jumpBuffer;
        else 
            jumpBufferCounter -= Time.deltaTime;

        // Jumping
        if(jumpBufferCounter > 0f){
            jumpBufferCounter = 0f;
            animator.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x * 0.5f, jumpForce);
            if(hasDoubleJump && hangCounter <= 0) hasDoubleJump = false;
        }
        
        // // Jump strength control
        // if(Input.GetButtonUp("Jump") && rb.velocity.y > 0 && jumpBufferCounter > 0f){
        //     print("opa");
        //     rb.velocity = new Vector2(rb.velocity.x, 0.5f * jumpForce);
        // }

        // Dash Cooldown
        if(dashCooldownCounter <= 0 && isGrounded)
            canDash = true;

        // Orb control
        if(isFacing > 0 && orbPos.position != orbPos1.position){
            orbPos.position = Vector2.Lerp(orbPos.position, orbPos1.position, 0.2f);
        }
        if(isFacing < 0 && orbPos.position != orbPos2.position){
            orbPos.position = Vector2.Lerp(orbPos.position, orbPos2.position, 0.2f);
        }
        
    }

    private void FixedUpdate() {
        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash){
            rb.velocity = Vector2.zero;
            animator.SetTrigger("Dash");
            rb.velocity = transform.right * dashForce * isFacing;
            dashCooldownCounter = dashCooldown;
            canDash = false;
        }
        else dashCooldownCounter -= Time.deltaTime;
    }
}