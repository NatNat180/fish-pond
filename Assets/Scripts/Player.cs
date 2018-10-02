using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public NavMeshAgent agent;
    public Camera mainCam;
    private Animator animator;
    public Rigidbody hook;
    public Transform pond;
    private NavMeshAgent playerAgent;
    private float meter = 0;
    private Vector3 hookCastPos;
    private Vector3 playerFaceDirection;
    private bool playerCastingLine;
    private float counter = 0f;
    public GameObject progressSlider;
    private ProgressBar progressBar;
    private bool playerWalking;
    private bool DestinationReached;

    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerCastingLine = false;
        progressBar = progressSlider.GetComponent<ProgressBar>();
        progressSlider.SetActive(false);
        playerWalking = false;
    }

    void Update()
    {
        animator.SetBool("DestinationReached", DestinationReached);
        Debug.Log(agent.remainingDistance);
        // get cursor and player position
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.position);

        // get raycast from cursor
        Ray ray = mainCam.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // player movement
            if (Input.GetMouseButtonDown(0))
            {
                playerWalking = true;
                // remove any hooks in water if player walks mid-fish
                destroyHookInstances();
                playerAgent.SetDestination(hit.point);
                //animator.Play("Walk");
            }
        }
        if (agent.remainingDistance > agent.stoppingDistance +.25f)
        {
            DestinationReached = false;

        }
        else {
            DestinationReached = true;
                }
        if (Input.GetButton("Cast"))
        {
            // get rid of any extra hooks in water
            destroyHookInstances();
            // TODO: add a cooldown timer if user re-casts

            // capture cursor position if it hasn't already been captured
            if (Vector3.zero.Equals(hookCastPos) && Physics.Raycast(ray, out hit))
            {
                hookCastPos = hit.point;
                playerFaceDirection = mousePosition;
                playerFaceDirection.x = playerFaceDirection.x * 0.88f;
                playerFaceDirection.y = playerFaceDirection.y * 0.95f;
            }

            animator.Play("Cast");
            playerCastingLine = true;
        }

        if (playerCastingLine)
        {

            if (Vector3.zero != hookCastPos)
            {
                // rotate player towards hook cast position
                Vector3 direction = playerFaceDirection - playerPosition;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.AngleAxis(-angle, Vector3.up), Time.deltaTime * 5);
            }

            displayProgressMeter();
         
            if (Input.GetButtonUp("Cast"))
            {
                castLine();
            }
        }

        // if player catches fish, destroy hook instance and reset animation to idle
        if (Hook.toggleFishCaught)
        {
            // TODO: add a cooldown timer if user re-casts
            destroyHookInstances();
            //animator.Play("IdleNonFish");
            Hook.toggleFishCaught = false;
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
        hitPoint.y = pond.position.y + 0.2f;
        hitPoint.x = hitPoint.x + 0.2f;
        hitPoint.z = hitPoint.z - 0.2f;
        Instantiate(hook, hitPoint, Quaternion.Euler(-90, 0, 0));
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