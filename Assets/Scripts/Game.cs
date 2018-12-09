using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{

    public static bool FishAreCatcheable;
    public static bool FishCanMove;
    public static int Score = 0;
    public TextMeshProUGUI scoreText;
    private string scoreTextPrefix;
    public TextMeshProUGUI catchTimerText;
    public TextMeshProUGUI catchReqText;
    public TextMeshProUGUI globalTimerText;
    private float globalTime;
    public GameObject pauseMenu;
    public GameObject mainPanel;
    public static bool GameIsPaused;
    public Button replayButton;
    public Button exitButton;
    public TextMeshProUGUI finalScoreText;

    void Start()
    {
        FishAreCatcheable = true;
        FishCanMove = true;
        scoreTextPrefix = "Score:";
        globalTime = 500.0f;
        pauseMenu.SetActive(false);
        GameIsPaused = false;
        replayButton.onClick.AddListener(ReloadLevel);
        exitButton.onClick.AddListener(ExitGame);
    }

    void Update()
    {
        scoreText.text = scoreTextPrefix + Score.ToString();

        globalTime -= Time.deltaTime;
        globalTimerText.text = string.Format("{0}:{1}", Mathf.Floor(globalTime / 60).ToString("00"), (globalTime % 60).ToString("00"));
        if (globalTime <= 0)
        {
            // display Game Over UI w/ stats and retry/close options
            mainPanel.SetActive(false);
            pauseMenu.SetActive(true);
            GameIsPaused = true;
            finalScoreText.text = "Final Score: " + Score.ToString();
        }

        if (Hook.Timer > 0.0f && !FishAreCatcheable)
        {
            string seconds = (Hook.Timer % 60).ToString("00");
            catchTimerText.text = string.Format("{0}", seconds);
        }
        else
        {
            catchTimerText.text = "";
        }

        if (!FishAreCatcheable)
        {
            catchReqText.text = Hook.CatchReq;
        }
        else
        {
            catchReqText.text = "";
        }
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
