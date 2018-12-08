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

    void Start()
    {
        FishAreCatcheable = true;
        FishCanMove = true;
		scoreTextPrefix = "Score:";
    }

    void Update()
    {
		scoreText.text = scoreTextPrefix + Score.ToString();
    }
}
