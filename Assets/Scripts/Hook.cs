using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hook : MonoBehaviour
{
    private bool isFishCaught;
    public static bool toggleFishCaught;
    private bool startTimer;
    public static float Timer;
    private Fish currentCatch;
    private const string FISH = "Fish";

    void Start()
    {
        isFishCaught = false;
        startTimer = false;
        Timer = 0;
        toggleFishCaught = false;
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
                Game.Score += currentCatch.Grade;
                Debug.Log("You caught a " + currentCatch.FishName
                    + "!  +" + Game.Score);

                Destroy(currentCatch.gameObject);
                startTimer = false;
                // reset fish movement bools
                Game.FishAreCatcheable = true;
                Game.FishCanMove = true;
            }

            if (Timer <= 0)
            {
                Debug.Log("The fish escaped!");
                startTimer = false;
                Game.FishCanMove = true;
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
        }
    }

    void beginCatch(KeyCode[] catchCode)
    {
        int catchProgress = 0;
        // detect if user is pressing catch code in order - reset progress if wrong key is pressed
        if (Input.GetKeyDown(catchCode[catchProgress]))
        {
            catchProgress++;
        }
        else { catchProgress = 0; }

        // if catch progress reaches length of catch code array, fish has been caught
        if (catchProgress >= catchCode.Length) { isFishCaught = true; }
    }

}