using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{

    public Camera mainCam;
    private Animator animator;
    private NavMeshAgent playerAgent;
    public Rigidbody hook;
    public Transform pond;

    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                playerAgent.SetDestination(hit.point);
            }
        }

        if (Input.GetButtonDown("Cast"))
        {
            animator.Play("Cast");
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 hitPoint = hit.point;
                // make sure y-coordinate of hook is level with pond
                hitPoint.y = pond.position.y;
                // get rid of any extra instances of hooks
                Destroy(GameObject.FindGameObjectWithTag("Hook"));
                Instantiate(hook, hitPoint, Quaternion.identity);
            }
        }

        if (Input.GetButtonDown("CastIdle"))
        {
            animator.Play("CastIdle");
        }
    }
}