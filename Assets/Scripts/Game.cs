using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	public static bool FishAreCatcheable;
	public static bool FishCanMove;
	public static int Score = 0;

	void Start () {
		FishAreCatcheable = true;
		FishCanMove = true;
	}
	
	void Update () {
		
	}
}
