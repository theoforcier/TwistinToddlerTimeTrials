using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    public static Timer instance;

    public float totalTime;
    public float currentTime;
    public bool timing;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
        totalTime = 0;
        currentTime = 0;
        timing = false;
    }

    private void Update()
    {
        if (timing)
            currentTime += Time.deltaTime;
    }
}
