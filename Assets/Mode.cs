using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mode : MonoBehaviour
{
    public TMP_Text modeText;

    void Awake()
    {
        if (GameManager.instance.gameMode == "marathon")
            modeText.text = GameManager.instance.gameMode.ToUpper();

        else if (GameManager.instance.gameMode == "normal")
            modeText.text = "Level " + GameManager.instance.level.ToString();
    }
}
