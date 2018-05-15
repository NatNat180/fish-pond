using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour {

	public Camera mainCam;
	public Animation anim;
	private NavMeshAgent playerAgent;

	void Start () {
		playerAgent = GetComponent<NavMeshAgent>();
	}
	
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit)) {
				playerAgent.SetDestination(hit.point);
			}
		}

		if (Input.GetButtonDown("e")) {
			anim.Play("cast");
		}
	}
}