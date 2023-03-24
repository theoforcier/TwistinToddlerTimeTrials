using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Ghost : ScriptableObject
{
    public bool isRecording;
    public float recordFrequency;

    public List<float> timeStamp;
    public List<Vector2> position;
    public List<float> speed;
    public List<bool> isJumping;
    public List<bool> onWall;
    public List<bool> spriteFliped;

    public void resetData()
    {
        timeStamp.Clear();
        position.Clear();
        speed.Clear();
        isJumping.Clear();
        onWall.Clear();
        spriteFliped.Clear();
    }
      


}
