using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    public GameObject linePrefab;
    Line activeLine;
    public GameObject character;
    Jump jumpController;
    bool haveJumped = false;

    void Start(){
        jumpController = character.GetComponent<Jump>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpController.isJumping){
            activeLine = null;
            haveJumped = true;
        }

        if (!jumpController.isJumping  && haveJumped){
            for (int i = 0; i < 5; i++){
                GameObject newLine = Instantiate(linePrefab);
                activeLine = newLine.GetComponent<Line>();

                Vector2 linePositions = character.transform.position;
                linePositions.y--;
                activeLine.UpdateLine(linePositions);


                linePositions.y += Random.Range(0f, .5f);
                linePositions.x += Random.Range(-.5f, .5f);
                activeLine.UpdateLine(linePositions);

                if (Random.Range(0, 2) == 0){
                    Vector2 temp = linePositions;
                    temp.y += Random.Range(0f, .5f);
                    temp.x += Random.Range(-.5f, .5f);
                    activeLine.UpdateLine(temp);
                }

                linePositions = character.transform.position;
                linePositions.y--;
            }

            haveJumped = false;
        }
    }
}
