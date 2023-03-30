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
    float oldPos = 0.0f; 
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
           // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
           // ridge.rotation = angle;
            direction = playerDirection;     
        }

        if(baby.transform.position.y == ballon.position.y)
        {
            Debug.Log("Over");
            FlipSprite();
        }


     
    }

    void FixedUpdate()
    {
        if(player)
        {
            
            ridge.velocity = new Vector2(direction.x, direction.y) * movementSpeed;
        }


    }

    public void FlipSprite()
    {
        render.flipX = !render.flipX;
        
    }
}
