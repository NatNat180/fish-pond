using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour {

	public Camera mainCam;
	private Animator animator;
	private NavMeshAgent playerAgent;

	void Start () {
		playerAgent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();

	}

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                playerAgent.SetDestination(hit.point);
            }
        }

        if (Input.GetButtonDown("Cast"))
        {
            animator.Play("Cast");
        }

        if (Input.GetButtonDown("CastIdle"))
        {
            animator.Play("CastIdle");
        }
    }
}