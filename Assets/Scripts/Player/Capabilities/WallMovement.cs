using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour
{
    public bool WallJumping { get; private set; }

    [SerializeField] private InputController input = null;
    [Header("Wall Slide")]
    [SerializeField][Range(0.1f,5f)] private float wallSlideMaxSpeed = 2f;
    [Header("Wall Jump")]
    [SerializeField] private Vector2 wallJumpClimb = new Vector2(4f, 12f);
    [SerializeField] private Vector2 wallJumpBounce = new Vector2(10.7f, 10f);
    [SerializeField] private Vector2 wallJumpLeap = new Vector2(14f, 12f);

    private CollisionData collisionData;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer render;

    private Vector2 velocity;
    private bool onGround, desiredJump, wallAnimation;
    private float wallDirectionX;

    public bool onWall;

    // Start is called before the first frame update
    private void Start()
    {
        collisionData = GetComponent<CollisionData>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (onWall && !onGround)
        {
            desiredJump |= input.RetrieveJumpInput();
        }

        // Animations
        if (onGround)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("onWall", false);
        }
        else if (wallAnimation) 
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("onWall", true);
        }
        else
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("onWall", false);
        }
    }

    private void FixedUpdate()
    {
        velocity = rb.velocity;
        onWall = collisionData.OnWall;
        onGround = collisionData.IsGrounded;
        wallDirectionX = collisionData.ContactNormal.x;

        #region Wall Slide
        if (onWall && -wallDirectionX == input.RetrieveMoveInput())
        {
            wallAnimation = true;
            if (velocity.y < -wallSlideMaxSpeed)
            {
                velocity.y = -wallSlideMaxSpeed;
            }
        }
        else
        {
            wallAnimation = false;
        }
        #endregion

        #region Wall Jump
        if (onWall && velocity.x == 0 || onGround)
        {
            WallJumping = false;
        }

        if (desiredJump)
        {
            // If moving towards wall
            if (-wallDirectionX == input.RetrieveMoveInput())
            {
                velocity = new Vector2(wallJumpClimb.x * wallDirectionX, wallJumpClimb.y);
                WallJumping = true;
                desiredJump = false;
            }
            // If no movement
            else if (input.RetrieveMoveInput() == 0)
            {
                velocity = new Vector2(wallJumpBounce.x * wallDirectionX, wallJumpBounce.y);
                WallJumping = true;
                desiredJump = false;
                GetComponent<Move>().FlipSprite();
            }
            // If moving away from wall
            else
            {
                velocity = new Vector2(wallJumpLeap.x * wallDirectionX, wallJumpLeap.y);
                WallJumping = true;
                desiredJump = false;
            }

            animator.Play("baby_jump", -1, 0f);
        }
        #endregion

        // Setting player velocity
        rb.velocity = velocity;
    }

    // Avoid sliding up walls
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionData.EvaluateCollision(collision);

        if (collisionData.OnWall && !collisionData.IsGrounded && WallJumping)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
