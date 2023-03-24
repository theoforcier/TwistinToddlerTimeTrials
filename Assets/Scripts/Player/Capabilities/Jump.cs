using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f, 20f)] private float jumpHeight = 3f;
    [SerializeField, Range(0, 5)] private int maxAirJumps = 1;
    [SerializeField, Range(0f, 10f)] private float downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 10f)] private float upwardMovementMultiplier = 1.7f;
    [SerializeField, Range(0f, 0.5f)] private float coyoteTime = 0.2f;
    [SerializeField, Range(0f, 0.5f)] private float jumpBufferTime = 0.2f;

    private Rigidbody2D rb;
    private Animator animator;
    private CollisionData collisionData;
    private Vector2 velocity;

    private int jumpPhase;
    private float defaultGravityScale, jumpSpeed, coyoteCounter, jumpBufferCounter;

    private bool desiredJump, isGrounded;
    public bool isJumping;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collisionData = GetComponent<CollisionData>();

        defaultGravityScale = 1f;
    }

    private void Update()
    {
        desiredJump |= input.RetrieveJumpInput();
    }

    private void FixedUpdate() {
        isGrounded = collisionData.IsGrounded;
        velocity = rb.velocity;

        // Reset jump phase
        if (isGrounded && rb.velocity.y == 0)
        {
            jumpPhase = 0;
            coyoteCounter = coyoteTime;
            isJumping = false;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        // Process jump input
        if (desiredJump)
        {
            desiredJump = false;
            jumpBufferCounter = jumpBufferTime;
        }
        else if (!desiredJump && jumpBufferCounter > 0)
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0)
        {
            JumpAction();
        }

        // Apply appropriate multiplier depending on upwards or downwards velocity and buttom hold
        if (input.RetrieveJumpHoldInput() && rb.velocity.y > 0)
        {
            rb.gravityScale = upwardMovementMultiplier;
        }
        else if (!input.RetrieveJumpHoldInput() || rb.velocity.y < 0)
        {
            rb.gravityScale = downwardMovementMultiplier;
        }
        else if (rb.velocity.y == 0)
        {
            rb.gravityScale = defaultGravityScale;
        }

        // Finally, set velocity
        rb.velocity = velocity;
    }

    private void JumpAction()
    {
        // If jump is available, calculate jump speed and apply to velocity
        if (coyoteCounter > 0 || (jumpPhase < maxAirJumps && isJumping))
        {
            if (isJumping)
            {
                jumpPhase += 1;
            }

            jumpBufferCounter = 0;
            coyoteCounter = 0;
            jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);
            isJumping = true;

            if (velocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
            }

            velocity.y += jumpSpeed;
        }
    }
}
