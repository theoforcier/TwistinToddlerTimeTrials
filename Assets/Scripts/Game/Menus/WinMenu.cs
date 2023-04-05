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
    public GameObject gameUI;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI deathCount;    
    public GameObject trophy;
    public string prefName;
    public GameObject Player;
    public AudioClip winClip;
    private bool clipPlayed = false;

    public void Update()
    {
        if (isWin)
        {
            // Marathon mode
            if (GameManager.instance.gameMode == "marathon")
            {
                if (GameManager.instance.level < GameManager.instance.totalLevels)
                {
                    Player.GetComponent<Death>().ClearDeaths();
                    NextLevel();
                    return;
                }

                else
                {
                    if (Timer.instance.timing)
                    { 
                        Timer.instance.totalTime += Timer.instance.currentTime; // this is normally done in NextLevel(), so we need to call it here for the level
                        Timer.instance.timing = false;
                    }

                    Time.timeScale = 0f;
                    timeText.text = Timer.instance.totalTime.ToString("0.000");

                    // Playing audio clip
                    if (!clipPlayed)
                    {
                        AudioManager.instance.PlaySound(winClip);
                        clipPlayed = true;
                    }

                    // Setting high scores
                    if (Timer.instance.totalTime < PlayerPrefs.GetFloat("marathon") || PlayerPrefs.GetFloat("marathon") == 0)
                    {
                        PlayerPrefs.SetFloat("marathon", Timer.instance.totalTime);
                        trophy.SetActive(true);
                    }
                }
            }

            else
            {
                // Normal mode
                Time.timeScale = 0f;
                timeText.text = Timer.instance.currentTime.ToString("0.000");
                deathCount.text = PlayerPrefs.GetInt("death").ToString();

                // Playing audio clip
                if (!clipPlayed)
                {
                    AudioManager.instance.PlaySound(winClip);
                    clipPlayed = true;
                }

                // Setting high scores
                if (Timer.instance.currentTime < PlayerPrefs.GetFloat(prefName) || PlayerPrefs.GetFloat(prefName) == 0)
                {
                    PlayerPrefs.SetFloat(prefName, Timer.instance.currentTime);
                    Player.GetComponent<GhostRecorder>().ghost.isRecording = false;

                    Ghost newGhost = Instantiate(Player.GetComponent<GhostRecorder>().ghost);
                    Ghost levelGhost = Resources.Load<Ghost>("GhostTrials/" + prefName);

                    levelGhost.isJumping = newGhost.isJumping;
                    levelGhost.isRecording = newGhost.isRecording;
                    levelGhost.onWall = newGhost.onWall;
                    levelGhost.position = newGhost.position;
                    levelGhost.recordFrequency = newGhost.recordFrequency;
                    levelGhost.speed = newGhost.speed;
                    levelGhost.spriteFliped = newGhost.spriteFliped;
                    levelGhost.timeStamp = newGhost.timeStamp;
                    trophy.SetActive(true);
                }
            }

            winMenuUI.SetActive(true);
            gameUI.SetActive(false);
        }
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("death", 0);
        Player.GetComponent<Death>().ClearDeaths();
        isWin = false;
        gameUI.SetActive(true);

        if (GameManager.instance.gameMode == "marathon")
            Timer.instance.totalTime += Timer.instance.currentTime;

        Timer.instance.currentTime = 0f;
        Time.timeScale = 1f;
        GameManager.instance.level++;
        if (GameManager.instance.level == 10)
            GameManager.instance.level = 1;

        SceneManager.LoadScene("Level " + GameManager.instance.level.ToString());
    }

    public void Restart()
    {
        PlayerPrefs.SetInt("death", 0);
        Player.GetComponent<Death>().ClearDeaths();
        isWin = false;
        gameUI.SetActive(true);
        Timer.instance.currentTime = 0f;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        PlayerPrefs.SetInt("death", 0);
        Player.GetComponent<Death>().ClearDeaths();
        isWin = false;
        gameUI.SetActive(false);
        Timer.instance.currentTime = 0f;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
