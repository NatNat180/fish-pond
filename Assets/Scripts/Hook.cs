﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {
    private bool isFishCaught;
    private bool startTimer;
    private float timer;
    private Fish currentCatch;
    private const string FISH = "Fish";

    void Start() {

        isFishCaught = false;
        startTimer = false;
        timer = 0;
    }

    void Update() {

        if (startTimer) {

            beginCatch(currentCatch.CatchReq);
            timer -= Time.deltaTime;

            if (isFishCaught) {

                isFishCaught = false;
                
                Game.Score += currentCatch.Grade;
                Debug.Log("You caught a " + currentCatch.FishName 
                    + "!  +" + Game.Score);
                
                Destroy(currentCatch.gameObject);
                startTimer = false;
                Game.FishAreCatcheable = true;
            }

            if (timer <= 0) {

                currentCatch.GetComponent<Collider>()
                    .attachedRigidbody.constraints = RigidbodyConstraints.None;
                
                startTimer = false;
                StartCoroutine(CoolDownTimer());
            }
        }
    }

    IEnumerator CoolDownTimer() {
        int time = 2;
        while (time > 0) {
            yield return new WaitForSeconds(1f);
            time--;
        }
        Game.FishAreCatcheable = true;
    }

    /* If hook collides with a fish, freeze fish, 
    prevent other fish from being caught in hook, 
    and begin monitoring of user input -- if catch 
    requirements are met, reset isFishCaught variable, 
    increment score and destroy instance of fish 
    -- otherwise, unfreeze caught fish and allow for 
    other fish to be caught again */
    void OnTriggerEnter(Collider collider) {

        if (FISH.Equals(collider.tag) &&
            Game.FishAreCatcheable == true) {

            Debug.Log("A fish has collided with the hook!");
            Game.FishAreCatcheable = false;
            collider.attachedRigidbody.constraints = RigidbodyConstraints.FreezeAll;

            Fish fish = collider.GetComponent<Fish>();
            currentCatch = fish;
            timer = fish.CatchTime;
            startTimer = true;
        }
    }

    void beginCatch(KeyCode[] catchCode) {
        int catchProgress = 0;
        // detect if user is pressing catch code in order - reset progress if wrong key is pressed
        if (Input.GetKeyDown(catchCode[catchProgress])) {
            catchProgress++;
        } else { catchProgress = 0; }

        // if catch progress reaches length of catch code array, fish has been caught
        if (catchProgress >= catchCode.Length) { isFishCaught = true; }
    }

}