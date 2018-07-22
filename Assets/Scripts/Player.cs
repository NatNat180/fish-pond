using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour {

	public Camera mainCam;
	private Animator animator;
	private NavMeshAgent playerAgent;
    public Rigidbody hook;
    public Transform pond;

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
            Vector3 cursorPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            // Make sure y coordinate of hook is consistent with pond
            cursorPos.y = pond.position.y;
            Instantiate(hook, cursorPos, Quaternion.identity);
        }

        if (Input.GetButtonDown("CastIdle"))
        {
            animator.Play("CastIdle");
        }
    }
}