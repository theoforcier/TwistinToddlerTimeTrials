using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRecorder : MonoBehaviour
{
    public Ghost ghost;
    public float timer;
    private float timeValue;

    private void Awake()
    {
            ghost.resetData();
            timeValue = 0;
            timer = 0;
            ghost.isRecording = true;
    }

    void Update()
    {
        timer += Time.deltaTime;
        timeValue += Time.deltaTime;

        if (GameManager.instance.gameMode == "normal" && ghost.isRecording && timer >= 1/ghost.recordFrequency)
        {
            ghost.timeStamp.Add(Timer.instance.currentTime);
            ghost.position.Add(this.transform.localPosition);

            // animations
            ghost.speed.Add(GetComponent<Move>().desiredVelocity.x);
            ghost.isJumping.Add(GetComponent<Jump>().isJumping);
            ghost.onWall.Add(GetComponent<WallMovement>().onWall);
            ghost.spriteFliped.Add(GetComponent<SpriteRenderer>().flipX);

            timer = 0;
        }
    }
}
