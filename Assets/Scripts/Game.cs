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

    void Start()
    {
        FishAreCatcheable = true;
        FishCanMove = true;
        scoreTextPrefix = "Score:";
    }

    void Update()
    {
        scoreText.text = scoreTextPrefix + Score.ToString();
        if (Hook.Timer > 0.0f && !FishAreCatcheable)
        {
            string seconds = (Hook.Timer % 60).ToString("00");
            catchTimerText.text = string.Format("{0}", seconds);
        }
        else
        {
            catchTimerText.text = "";
        }
    }
}
