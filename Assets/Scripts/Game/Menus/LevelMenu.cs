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
    public Button test;

    private void Start()
    {
        record1.text = PlayerPrefs.GetFloat("Level1").ToString("0.000");
        record2.text = PlayerPrefs.GetFloat("Level2").ToString("0.000");
        record3.text = PlayerPrefs.GetFloat("Level3").ToString("0.000");

        // Sets up the respective event listeners for each button
        InitalizeButton(test);
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
        button.onClick.AddListener(delegate { LoadLevel(GameManager.instance.level) ;});
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void Level3()
    {
        SceneManager.LoadScene("Level 3");
    }
}
