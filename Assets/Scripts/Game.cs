using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	public static bool FishAreCatcheable;
	public static int Score = 0;
	public GameObject pond;
	public static Vector3 PondPosition;

	void Start () {
		FishAreCatcheable = true;
		PondPosition = pond.transform.position;
	}
	
	void Update () {
		
	}
}
