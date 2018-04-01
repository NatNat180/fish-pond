using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}

	/*	If hook collides with a fish, begin monitoring of user input.

	 */
	void OnCollisionEnter(Collision collision) {
		if (collision.rigidbody.tag == "Fish") {
			Fish fish = collision.collider.GetComponent<Fish>();
			KeyCode[] catchCode = fish.CatchReq;
			bool isFishCaught = beginCatch(catchCode, fish);
			if (isFishCaught) {
				Game.Score += fish.Grade;
				Destroy(collision.gameObject);
			}
			Debug.Log("Current score = " + Game.Score);
		}
	}

	bool beginCatch(KeyCode[] catchCode, Fish fish) {
		int catchProgress = 0;
		while (catchProgress < catchCode.Length) {
			if (Input.GetKeyDown(catchCode[catchProgress])) {
				Debug.Log(catchCode[catchProgress]); 
				catchProgress++;
			} else { Debug.Log("try again!"); catchProgress = 0; } 
		}
		return true;
	}
}
