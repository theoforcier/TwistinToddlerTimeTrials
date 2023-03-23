using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject levelsPanel;

    public void PlayGame()
    {
        levelsPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
