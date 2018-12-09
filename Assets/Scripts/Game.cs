using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    void Start()
    {
        FishAreCatcheable = true;
        FishCanMove = true;
        scoreTextPrefix = "Score:";
        globalTime = 300.0f;
    }

    void Update()
    {
        scoreText.text = scoreTextPrefix + Score.ToString();

        globalTime -= Time.deltaTime;
        globalTimerText.text = string.Format("{0}:{1}", Mathf.Floor(globalTime / 60).ToString("00"), (globalTime % 60).ToString("00"));
        if (globalTime <= 0)
        {
            // display Game Over UI w/ stats and retry/close options
            Debug.Log("Time Over!");
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
}
