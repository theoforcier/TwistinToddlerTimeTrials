using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class WinMenu : MonoBehaviour
{
    public static bool isWin = false;
    public GameObject winMenuUI;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI deathCount;    
    public GameObject trophy;
    public string prefName;
    public GameObject Player;

    public void Update()
    {
        if (isWin)
        {
            Time.timeScale = 0f;
            timeText.text = Timer.currentTime.ToString("0.000");
            deathCount.text = PlayerPrefs.GetInt("death").ToString();

            // Setting high scores
            if (Timer.currentTime < PlayerPrefs.GetFloat(prefName) || PlayerPrefs.GetFloat(prefName) == 0)
            {
                PlayerPrefs.SetFloat(prefName, Timer.currentTime);
                Player.GetComponent<GhostRecorder>().ghost.isRecording = false;
                AssetDatabase.Refresh();

                Ghost newGhost = Instantiate(Player.GetComponent<GhostRecorder>().ghost);
                AssetDatabase.DeleteAsset("Assets/ScriptableObjects/GhostTrials/" + prefName + ".asset");
                AssetDatabase.CreateAsset(newGhost, "Assets/ScriptableObjects/GhostTrials/" + prefName + ".asset");
                trophy.SetActive(true);
            }

            winMenuUI.SetActive(true);
        }
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("death", 0);
        Debug.Log(PlayerPrefs.GetInt("death"));
        isWin = false;
        Timer.currentTime = 0f;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Restart()
    {
        PlayerPrefs.SetInt("death", 0);
        Debug.Log(PlayerPrefs.GetInt("death"));
        isWin = false;
        Timer.currentTime = 0f;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        PlayerPrefs.SetInt("death", 0);
         Debug.Log(PlayerPrefs.GetInt("death"));
        isWin = false;
        Timer.currentTime = 0f;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
