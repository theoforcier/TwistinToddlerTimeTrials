using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerCanvas : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI bestText;
    public string prefName;

    void Start()
    {
        if (GameManager.instance.gameMode == "normal")
            bestText.text = PlayerPrefs.GetFloat(prefName).ToString("0.000");
        else
            bestText.text = PlayerPrefs.GetFloat("marathon").ToString("0.000");
    }

    void Update()
    {
        if (GameManager.instance.gameMode == "normal")
            timerText.text = Timer.instance.currentTime.ToString("0.000");
        else
            timerText.text = (Timer.instance.totalTime + Timer.instance.currentTime).ToString("0.000");
    }
}
