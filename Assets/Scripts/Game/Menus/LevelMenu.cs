using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public GameObject levelsPanel;
    public TextMeshProUGUI record1, record2, record3, record4, record5, record6, record7, record8, record9;
    public Button[] buttons;
    public Button marathonButton;
    public Button tutorialButton;
    private int currentLevel;

    private void Start()
    {
        record1.text = PlayerPrefs.GetFloat("Level1").ToString("0.000");
        record2.text = PlayerPrefs.GetFloat("Level2").ToString("0.000");
        record3.text = PlayerPrefs.GetFloat("Level3").ToString("0.000");
        record4.text = PlayerPrefs.GetFloat("Level4").ToString("0.000");
        record5.text = PlayerPrefs.GetFloat("Level5").ToString("0.000");
        record6.text = PlayerPrefs.GetFloat("Level6").ToString("0.000");
        record7.text = PlayerPrefs.GetFloat("Level7").ToString("0.000");
        record8.text = PlayerPrefs.GetFloat("Level8").ToString("0.000");
        record9.text = PlayerPrefs.GetFloat("Level9").ToString("0.000");

        // Sets up the respective event listeners for each button
        foreach (Button b in buttons)
        {
            InitalizeButton(b);
        }

        marathonButton.onClick.AddListener(Marathon);
        tutorialButton.onClick.AddListener(Tutorial);
    }

    public void Back()
    {
        levelsPanel.SetActive(false);
    }

    public void Marathon()
    {
        GameManager.instance.gameMode = "marathon";
        GameManager.instance.level = 1;
        Timer.instance.totalTime = 0;
        LoadLevel(GameManager.instance.level);
    }

    public void LoadLevel(int level)
    {
        Timer.instance.timing = true;
        SceneManager.LoadScene("Level " + level.ToString());
        GameManager.instance.level = level;
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
