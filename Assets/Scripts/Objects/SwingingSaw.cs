using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingSaw : MonoBehaviour
{
    public GameObject pivotPoint;
    public GameObject Saw;
    void Update()
    {
        transform.RotateAround(pivotPoint.transform.position, Vector3.back, 200f * Time.deltaTime);
        Saw.transform.Rotate(Vector3.back * 200f * Time.deltaTime);
    }
}
