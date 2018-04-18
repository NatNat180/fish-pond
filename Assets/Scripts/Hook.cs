using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {

	private static bool isFishCaught;
	private static string FISH = "Fish"; 

	void Start () {
		isFishCaught = false;
	}
	
	void Update () {
		
	}

	/*	If hook collides with a fish, freeze fish and begin monitoring 
		of user input -- if catch requirements are met, 
		reset isFishCaught variable, increment score and 
		destroy instance of fish -- otherwise, unfreeze fish */
	void OnTriggerEnter(Collider collider) {
		if (FISH.Equals(collider.tag)) {
			Debug.Log("A fish has collided with the hook!");
			collider.attachedRigidbody.constraints = RigidbodyConstraints.FreezeAll;
			Fish fish = collider.GetComponent<Fish>();
			StartCoroutine(BeginCatch(fish.CatchReq, fish.CatchTime));
			if (isFishCaught) {
				isFishCaught = false;
				Game.Score += fish.Grade;
				Destroy(collider.gameObject);
			}
			collider.attachedRigidbody.constraints = RigidbodyConstraints.None;
		}
		Debug.Log("Current score = " + Game.Score);
	}

	IEnumerator BeginCatch(KeyCode[] catchCode, int catchTime) { 
		int countdown = catchTime;
		int catchProgress = 0;
		while (countdown > 0) {
			
			// start timer
			yield return new WaitForSeconds(1.0f);
			countdown--;
			Debug.Log(countdown);
			
			// detect if user is pressing catch code in order - reset progress if wrong key is pressed
			if (Input.GetKeyDown(catchCode[catchProgress])) {
				Debug.Log("right key pressed!");
				catchProgress++;
			} else { catchProgress = 0; }
			
			// if catch progress reaches length of catch code array, fish has been caught
			if (catchProgress >= catchCode.Length) { isFishCaught = true; }
		}
	}
}