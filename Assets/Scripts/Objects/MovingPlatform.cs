using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float movementSpeed;
    public int start;
    public Transform[] locationPoints;
    int i;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = locationPoints[start].position;    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Vector2.Distance(transform.position, locationPoints[i].position) < 0.02f)
        {
            i++;
            if (i == locationPoints.Length)
            {
                i = 0; 
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, locationPoints[i].position, movementSpeed *Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision){

        collision.collider.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision){
        collision.collider.transform.SetParent(null);
    }
}
