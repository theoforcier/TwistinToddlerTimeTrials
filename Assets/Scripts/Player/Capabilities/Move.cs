using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 20f;
    [SerializeField, Range(0.05f, 0.5f)] private float wallStickTime = 0.25f;

    private Vector2 direction, desiredVelocity, velocity;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer render;
    private CollisionData collisionData;
    private WallMovement wallMovement;

    private float maxSpeedChange, acceleration, wallStickCounter;
    private bool isGrounded, facingRight=true;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        collisionData = GetComponent<CollisionData>();
        wallMovement = GetComponent<WallMovement>();
    }

    private void Update()
    {
        direction.x = input.RetrieveMoveInput();

        // Flip sprite
        if ((direction.x > 0 && !facingRight) || (direction.x < 0 && facingRight))
        {
            FlipSprite();
        }

        desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - collisionData.Friction, 0f);
        animator.SetFloat("Speed", Mathf.Abs(desiredVelocity.x));
    }

    // Apply movement
    private void FixedUpdate() {
        isGrounded = collisionData.IsGrounded;
        velocity = rb.velocity;

        acceleration = isGrounded ? maxAcceleration : maxAirAcceleration;
        maxSpeedChange = acceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

        // If sliding, give time to perform inputs
        #region Wall Stick
        if (collisionData.OnWall && !collisionData.IsGrounded && !wallMovement.WallJumping)
        {
            if (wallStickCounter > 0)
            {
                velocity.x = 0;

                if (input.RetrieveMoveInput() == collisionData.ContactNormal.x)
                {
                    wallStickCounter -= Time.deltaTime;
                }
                else
                {
                    wallStickCounter = wallStickTime;
                }
            }
            else
            {
                wallStickCounter = wallStickTime;
            }
        }
        #endregion

        rb.velocity = velocity;
    }

    public void FlipSprite()
    {
        render.flipX = !render.flipX;
        facingRight = !facingRight;
    }
}
