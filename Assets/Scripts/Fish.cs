using UnityEngine;
using UnityEngine.AI;

public class Fish : MonoBehaviour {

    public FishDefinition traits;
    public static bool FreezePos;
    public float swimRadius;
    public float swimTimer;

    private NavMeshAgent agent;
    private string fishName;
    private int grade;
    private KeyCode[] catchReq;
    private int catchTime;
    private float timer;

	public string FishName { get {return fishName;} }
	public int Grade { get {return grade;} }
	public KeyCode[] CatchReq { get {return catchReq;} }
    public int CatchTime { get {return catchTime;} }

    void Start() {
        
        this.fishName = traits.fishName;
        this.grade = traits.grade;
        this.catchReq = traits.catchReq;
        this.catchTime = traits.catchTime;
        agent = GetComponent<NavMeshAgent>();
        timer = swimTimer;
        isCaught = false;
        
        Debug.Log("I am a new fish! My name is " + fishName 
        + ", and my grade is " + grade + "!");
    }

    void Update() {
        
        timer += Time.deltaTime;
        if (timer >= swimTimer && FreezePos == false) {
            Vector3 newPos = RandomNavLocation(transform.position, swimRadius, -1);
            fishAgent.setDesination(newPos);
            timer = 0;
        }

    }

    public static Vector3 RandomNavLocation(Vector3 origin, float distance, int layerMask) {

        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layerMask);
        return navHit.position;
    }

}
