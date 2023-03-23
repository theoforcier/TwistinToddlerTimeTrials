using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelMenu : MonoBehaviour
{
    public GameObject levelsPanel;
    public TextMeshProUGUI record1, record2, record3;

    private void Start()
    {
        record1.text = PlayerPrefs.GetFloat("Level1").ToString("0.000");
        record2.text = PlayerPrefs.GetFloat("Level2").ToString("0.000");
        record3.text = PlayerPrefs.GetFloat("Level3").ToString("0.000");
    }

    public void Back()
    {
        levelsPanel.SetActive(false);
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
