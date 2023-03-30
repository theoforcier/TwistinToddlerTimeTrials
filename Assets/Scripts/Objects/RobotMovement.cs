using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    public float movementSpeed;
    public int start;
    public Transform[] movementPoints;
    int i;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = movementPoints[start].position;    
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, movementPoints[i].position) < 0.02f)
        {
            i++;
            if (i == movementPoints.Length)
            {
                i = 0; 
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, movementPoints[i].position, movementSpeed *Time.deltaTime);
    }
}