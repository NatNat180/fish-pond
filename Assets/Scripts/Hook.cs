using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.rigidbody.tag == "Fish") {
			Fish fish = collision.collider.GetComponent<Fish>();
			KeyCode[] catchCode = fish.CatchReq;
			Debug.Log("---Catch Requirements---");
			foreach (KeyCode code in catchCode) {
				Debug.Log(code);
			}
		}
	}
}
