using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    public GameObject linePrefab;
    Line activeLine;
    public Jump character;
    public 
    bool haveJumped = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (character.isJumping){
            activeLine = null;
            haveJumped = true;
        }

        if (!character.isJumping  && haveJumped){
            GameObject newLine = Instantiate(linePrefab);
            activeLine = newLine.GetComponent<Line>();

            // Vector2 mousePos = character;
            // activeLine.Upd

            haveJumped = false;
        }
    }
}
