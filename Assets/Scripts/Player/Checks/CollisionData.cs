using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionData : MonoBehaviour
{
    public bool IsGrounded { get; private set; }
    public bool OnWall { get; private set; }
    public float Friction { get; private set; }
    public Vector2 ContactNormal { get; private set; }

    private PhysicsMaterial2D material;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        IsGrounded = false;
        OnWall = false;
        Friction = 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        EvaluateCollision(other);
        RetrieveFriction(other);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        EvaluateCollision(other);
        RetrieveFriction(other);
    }

    public void EvaluateCollision(Collision2D collision)
    {
        for (int i=0; i < collision.contactCount; i++)
        {
            ContactNormal = collision.GetContact(i).normal;
            IsGrounded |= ContactNormal.y >= 0.9f;
            OnWall = Mathf.Abs(ContactNormal.x) >= 0.9f;
        }
    }

    private void RetrieveFriction(Collision2D collision)
    {
        material = collision.rigidbody.sharedMaterial;
        Friction = 0;

        if (material != null)
        {
            Friction = material.friction;
        }
    }
}
