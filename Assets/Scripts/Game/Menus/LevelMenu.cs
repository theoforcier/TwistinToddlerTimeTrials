using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public GameObject levelsPanel;
    public TextMeshProUGUI record1, record2, record3;
    public Button[] buttons;
    private int currentLevel;

    private void Start()
    {
        record1.text = PlayerPrefs.GetFloat("Level1").ToString("0.000");
        record2.text = PlayerPrefs.GetFloat("Level2").ToString("0.000");
        record3.text = PlayerPrefs.GetFloat("Level3").ToString("0.000");

        // Sets up the respective event listeners for each button
        foreach (Button b in buttons)
        {
            InitalizeButton(b);
        }
    }

    public void Back()
    {
        levelsPanel.SetActive(false);
    }

    public void Marathon()
    {
        GameManager.instance.gameMode = "marathon";
        GameManager.instance.level = 1;
        LoadLevel(GameManager.instance.level);
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene("Level " + level.ToString());
    }

    public void InitalizeButton(Button button)
    {
        GameManager.instance.gameMode = "normal";
        GameManager.instance.level = int.Parse(button.GetComponentInChildren<TMP_Text>().text.Split()[1]);
        var localLevel = GameManager.instance.level;
        button.onClick.AddListener(delegate { LoadLevel(localLevel) ;});
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
