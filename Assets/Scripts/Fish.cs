using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fish : MonoBehaviour {

    public FishDefinition traits;
    public float swimRadius;
    public float swimTimer;

    private NavMeshAgent agent;
    private string fishName;
    private int grade;
    private KeyCode[] catchReq;
    private int catchTime;
    private bool freezePos;
    private float timer;
    private const string HOOK = "Hook";

	public string FishName { get {return fishName;} }
	public int Grade { get {return grade;} }
	public KeyCode[] CatchReq { get {return catchReq;} }
    public int CatchTime { get {return catchTime;} }
    public bool FreezePos { get; set; }

    void Start() {
        
        this.fishName = traits.fishName;
        this.grade = traits.grade;
        this.catchReq = traits.catchReq;
        this.catchTime = traits.catchTime;
        agent = GetComponent<NavMeshAgent>();
        timer = swimTimer;
        
        Debug.Log("I am a new fish! My name is " + fishName 
        + ", and my grade is " + grade + "!");
    }

    void Update() {
        
        timer += Time.deltaTime;
        if (timer >= swimTimer && freezePos == false) {
            Vector3 newPos = RandomNavLocation(transform.position, swimRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        } else if (freezePos) { StartCoroutine(CatchTimer()); }

    }

    public static Vector3 RandomNavLocation(Vector3 origin, float distance, int layerMask) {

        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layerMask);
        return navHit.position;
    }

    void OnTriggerEnter(Collider collider) {
        if (HOOK.Equals(collider.tag)) { freezePos = true; }
    }

    IEnumerator CatchTimer() {
        agent.Stop();
        int time = 0;
        while (time <= catchTime) {
            yield return new WaitForSeconds(1f);
            time++;
        }
        freezePos = false;
        timer = swimTimer;
        agent.Resume();
    }

}
