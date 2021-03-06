﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hook : MonoBehaviour
{
    private bool isFishCaught;
    public static bool toggleFishCaught;
    private bool startTimer;
    public static float Timer;
    public static string CatchReq;
    private Fish currentCatch;
    private const string FISH = "Fish";
    private int catchProgress;
    public ParticleSystem catchSplash;
    public ParticleSystem reelSplash;
    public static bool FishStruggling;

    void Start()
    {
        isFishCaught = false;
        startTimer = false;
        Timer = 0;
        toggleFishCaught = false;
        catchProgress = 0;
    }

    void Update()
    {
     
        if (startTimer)
        {
            beginCatch(currentCatch.CatchReq);
            Timer -= Time.deltaTime;
            

            if (isFishCaught)
            {
                toggleFishCaught = true;
                isFishCaught = false;
                Instantiate(catchSplash, transform.position, transform.rotation);
                Game.Score += currentCatch.Grade;
                Debug.Log("You caught a " + currentCatch.FishName
                    + "!  +" + Game.Score);

                Destroy(currentCatch.gameObject);
                Destroy(GameObject.FindWithTag("reelSplash"));
                startTimer = false;
                // reset fish movement bools
                Game.FishAreCatcheable = true;
                Game.FishCanMove = true;
                catchProgress = 0;
            }

            if (Timer <= 0)
            {
                Debug.Log("The fish escaped!");
                startTimer = false;
                Game.FishCanMove = true;
                FishStruggling = false; 
                StartCoroutine(CoolDownTimer());
            }
        }
    }

    /* if fish escapes, give it a second to move away 
    before getting caught in hook again */
    IEnumerator CoolDownTimer()
    {
        int time = 2;
        while (time > 0)
        {
            yield return new WaitForSeconds(1f);
            time--;
        }
        catchProgress = 0;
        Game.FishAreCatcheable = true;
    }

    /* If hook collides with a fish, freeze fish, 
    prevent other fish from being caught in hook, 
    and begin monitoring of user input -- if catch 
    requirements are met, reset isFishCaught variable, 
    increment score and destroy instance of fish 
    -- otherwise, unfreeze caught fish and allow for 
    other fish to be caught again */
    void OnTriggerEnter(Collider collider)
    {
        if (FISH.Equals(collider.tag) &&
            Game.FishAreCatcheable == true)
        {
            Fish fish = collider.GetComponent<Fish>();
            currentCatch = fish;

            Debug.Log("A fish has collided with the hook!");
            Game.FishAreCatcheable = false;
            Game.FishCanMove = false;
            startTimer = true;
            Timer = currentCatch.CatchTime;
            FishStruggling = true;
            Instantiate(reelSplash, transform.position, transform.rotation);
        }
    }

    void beginCatch(KeyCode[] catchCode)
    {
        CatchReq = catchCode[catchProgress].ToString();
        // detect if user is pressing catch code in order - reset progress if wrong key is pressed
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(catchCode[catchProgress]))
            {
                catchProgress += 1;
            }
            else
            {
                catchProgress = 0;
            }
        }

        // if catch progress reaches length of catch code array, fish has been caught
        if (catchProgress >= catchCode.Length) 
        { 
            isFishCaught = true;
            FishStruggling = false; 
        }
    }

}