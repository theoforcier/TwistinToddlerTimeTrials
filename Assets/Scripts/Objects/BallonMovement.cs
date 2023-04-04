using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonMovement : MonoBehaviour
{
    public float movementSpeed;
    public GameObject baby;
    Rigidbody2D ridge;
    
    Transform player;
    Transform ballon;
    Vector2 direction;
    private SpriteRenderer render;

    void Awake()
    {
        ridge = GetComponent<Rigidbody2D>();
        ballon = GetComponent<Transform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player)
        {
            Vector3 playerDirection = (player.position - transform.position).normalized;
            direction = playerDirection;
            if (direction.x < 0) {
                render.flipX = false;
            }
            else
            {
                render.flipX = true;
            }
        }
    }

    void FixedUpdate()
    {
        if(player)
        {
            ridge.velocity = new Vector2(direction.x, direction.y) * movementSpeed;
        }
    }
}
