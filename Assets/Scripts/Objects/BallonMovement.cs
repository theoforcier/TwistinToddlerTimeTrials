using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonMovement : MonoBehaviour
{
    public float movementSpeed;
    Rigidbody2D ridge;
    Transform player;
    Vector2 direction;
    bool check = true;
    float oldPos = 0.0f; 

    void Awake()
    {
        ridge = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        oldPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(player)
        {
            Vector3 playerDirection = (player.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            ridge.rotation = angle;
            direction = playerDirection;

            transform.right = -direction;    
            if(transform.position.y < oldPos)
            {
                transform.Rotate(new Vector3(180.0f, 0.0f, 0.0f));
               
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
