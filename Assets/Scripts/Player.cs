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
    private bool playerCastLine;
    private float counter = 0f;
    public GameObject progressSlider;
    private ProgressBar progressBar;

    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerCastLine = false;
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
                playerAgent.SetDestination(hit.point);
            }
        }

        if (Input.GetButton("Cast"))
        {
            animator.Play("Cast");
            playerCastLine = true;
        }

        if (playerCastLine)
        {
            Debug.Log(meter);
            // allow progress bar to appear and set counter to timer
            progressSlider.SetActive(true);
            counter += Time.deltaTime;
            // use ping-pong function as generic meter to count up and down
            meter = Mathf.PingPong(counter * 10, 10);
            // display meter progress in the progress bar
            progressBar.progress = meter;
            
            if (Input.GetButtonUp("Cast")) // if user let go of button, cast hook and reset counter so that it will start at zero next time
            {
                progressSlider.SetActive(false);
                playerCastLine = false;
                counter = 0f;
                Debug.Log("Player used " + (int)Mathf.Round(meter) + "0% accuracy to cast!");
                if (Physics.Raycast(ray, out hit))
                {
                    animator.Play("CastIdle");
                    Vector3 hitPoint = hit.point;
                    // make sure y-coordinate of hook is level with pond
                    hitPoint.y = pond.position.y;
                    // get rid of any extra instances of hooks
                    Destroy(GameObject.FindGameObjectWithTag("Hook"));
                    Instantiate(hook, hitPoint, Quaternion.identity);
                }
            }
        }
    }

}