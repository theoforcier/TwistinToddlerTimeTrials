using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI bestText;
    public static float currentTime;
    public string prefName;

    private void Start()
    {
        bestText.text = PlayerPrefs.GetFloat(prefName).ToString("0.000");
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        timerText.text = currentTime.ToString("0.000");
    }
}
