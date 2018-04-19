using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private bool isFishCaught;
    private static string FISH = "Fish";
    private bool startTimer;
    private float timer;
    private Fish currentCatch;

    void Start()
    {
        isFishCaught = false;
        startTimer = false;
		timer = 0;
        currentCatch = new Fish();
    }

    void Update()
    {
        if (startTimer)
        {
			beginCatch(currentCatch.CatchReq);
            while (timer > 0)
            {
				timer -= Time.deltaTime;
				Debug.Log(timer);
            }

			if (isFishCaught)
            {
                isFishCaught = false;
                Game.Score += currentCatch.Grade;
                Destroy(currentCatch.gameObject);
            }

			if (timer <= 0)
			{
				startTimer = false;
			}
        }
    }

    /*	If hook collides with a fish, freeze fish and begin monitoring 
		of user input -- if catch requirements are met, 
		reset isFishCaught variable, increment score and 
		destroy instance of fish -- otherwise, unfreeze fish */
    void OnTriggerEnter(Collider collider)
    {
        if (FISH.Equals(collider.tag))
        {
            Debug.Log("A fish has collided with the hook!");
            //collider.attachedRigidbody.constraints = RigidbodyConstraints.FreezeAll;

            Fish fish = collider.GetComponent<Fish>();
            currentCatch = fish;
            timer = fish.CatchTime;
            startTimer = true;

            //collider.attachedRigidbody.constraints = RigidbodyConstraints.None;
        }
        
		Debug.Log("Current score = " + Game.Score);
    }

    void beginCatch(KeyCode[] catchCode)
    {
        int catchProgress = 0;
        // detect if user is pressing catch code in order - reset progress if wrong key is pressed
        if (Input.GetKeyDown(catchCode[catchProgress]))
        {
            Debug.Log("right key pressed!");
            catchProgress++;
        }
        else { catchProgress = 0; }

        // if catch progress reaches length of catch code array, fish has been caught
        if (catchProgress >= catchCode.Length) { isFishCaught = true; }
    }

}