﻿using System.Collections;
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
    private bool FishReel = false;
    private bool CastStart = false;
    private bool hookIsCast = false;
    private float coolDownTimer = 0.0f;


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
        if (Game.GameIsPaused == false)
        {
            if (coolDownTimer > 0.0f)
            {
                coolDownTimer -= Time.deltaTime;
            }

            animator.SetBool("DestinationReached", DestinationReached);
            animator.SetBool("CastStart", CastStart);
            animator.SetBool("FishReel", FishReel);

            if (Hook.FishStruggling)
            {
                FishReel = true;
            }
            else
            {
                FishReel = false;
            }

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
                }
            }
            if (agent.remainingDistance > agent.stoppingDistance + .25f)
            {
                DestinationReached = false;
            }
            else
            {
                DestinationReached = true;
            }
            if (Input.GetButton("Cast") && Pond.PlayerCanCast && coolDownTimer <= 0.0f)
            {
                CastStart = true;
                FishReel = false;
                // get rid of any extra hooks in water
                destroyHookInstances();
                // capture cursor position if it hasn't already been captured
                if (Vector3.zero.Equals(hookCastPos) && Physics.Raycast(ray, out hit))
                {
                    hookCastPos = hit.point;
                    playerFaceDirection = mousePosition;
                    playerFaceDirection.x = playerFaceDirection.x * 0.88f;
                    playerFaceDirection.y = playerFaceDirection.y * 0.95f;
                }

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

            // ensure casting animation is complete before instantiating hook
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("CastIdle") && hookIsCast == false)
            {
                Vector3 hitPoint = hookCastPos;
                // make sure y-coordinate of hook is level with pond
                hitPoint.y = pond.position.y + 0.2f;
                hitPoint.x = hitPoint.x + 0.2f;
                hitPoint.z = hitPoint.z - 0.2f;
                Instantiate(hook, hitPoint, Quaternion.Euler(-90, 0, 0));
                hookIsCast = true;
                hookCastPos = Vector3.zero;
            }

            // if player catches fish, destroy hook instance and reset animation to idle
            if (Hook.toggleFishCaught)
            {
                destroyHookInstances();
                Hook.toggleFishCaught = false;
                coolDownTimer = 1.5f;
            }
        }
    }

    void displayProgressMeter()
    {
        // make cursor invisible while player is casting line
        Cursor.visible = false;
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
        // hook has not yet been cast
        hookIsCast = false;
        progressSlider.SetActive(false);
        playerCastingLine = false;
        counter = 0f;
        Debug.Log("Player used " + (int)Mathf.Round(meter) + "0% accuracy to cast!");
        // make cursor visible again
        Cursor.visible = true;
        CastStart = false;
    }

    void destroyHookInstances()
    {
        Destroy(GameObject.FindGameObjectWithTag("Hook"));
    }

}