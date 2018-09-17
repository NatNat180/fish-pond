using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public Camera mainCam;
    private Animator animator;
    public Rigidbody hook;
    public Transform pond;
    private NavMeshAgent playerAgent;
    private float meter = 0;
    private Vector3 hookCastPos;
    private bool playerCastingLine;
    private float counter = 0f;
    public GameObject progressSlider;
    private ProgressBar progressBar;

    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerCastingLine = false;
        progressBar = progressSlider.GetComponent<ProgressBar>();
        progressSlider.SetActive(false);
    }

    void Update()
    {
        // player movement and cursor
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                // remove any hooks in water if player walks mid-fish
                destroyHookInstances();
                playerAgent.SetDestination(hit.point);
                animator.Play("Walk");
            }
        }

        if (Input.GetButton("Cast"))
        {
            // capture cursor position if it hasn't already been captured
            if (Vector3.zero.Equals(hookCastPos) && Physics.Raycast(ray, out hit))
            {
                hookCastPos = hit.point;
            }
            animator.Play("Cast");
            playerCastingLine = true;
        }

        if (playerCastingLine)
        {
            displayProgressMeter();
            if (Input.GetButtonUp("Cast"))
            {
                castLine();
            }
        }
    }

    void displayProgressMeter()
    {
        // make cursor invisible while player is casting line
        Cursor.visible = false;
        Debug.Log(meter);
        // allow progress bar to appear and set counter to timer
        progressSlider.SetActive(true);
        counter += Time.deltaTime;
        // use ping-pong function as generic meter to count up and down
        meter = Mathf.PingPong(counter * 10, 10);
        // display meter progress in the progress bar
        progressBar.progress = meter;
    }

    void castLine() // if user let go of button, cast hook and reset counter so that it will start at zero next time
    {
        progressSlider.SetActive(false);
        playerCastingLine = false;
        counter = 0f;
        Debug.Log("Player used " + (int)Mathf.Round(meter) + "0% accuracy to cast!");
        animator.Play("CastIdle");
        Vector3 hitPoint = hookCastPos;
        // make sure y-coordinate of hook is level with pond
        hitPoint.y = pond.position.y;
        // get rid of any extra instances of hooks
        destroyHookInstances();
        Instantiate(hook, hitPoint, Quaternion.identity);
        // reset cast position so player can cast to different location next time
        hookCastPos = Vector3.zero;
        // make cursor visible again
        Cursor.visible = true;
    }

    void destroyHookInstances()
    {
        Destroy(GameObject.FindGameObjectWithTag("Hook"));
    }

}