
using UnityEngine;
using UnityEngine.AI;

public class Fish : MonoBehaviour {

    public FishDefinition traits;
    private string fishName;
    private int grade;
    private KeyCode[] catchReq;
    private int catchTime;
    public NavMeshAgent agent;
    public Camera cam;

	public string FishName { get {return fishName;} }
	public int Grade { get {return grade;} }
	public KeyCode[] CatchReq { get {return catchReq;} }
    public int CatchTime { get {return catchTime;} }

    void Start() {
        this.fishName = traits.fishName;
        this.grade = traits.grade;
        this.catchReq = traits.catchReq;
        this.catchTime = traits.catchTime;
        Debug.Log("I am a new fish! My name is " + fishName + ", and my grade is " + grade + "!");
    }

    void Update() {

        // Testing AI movement with NavMesh 
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                agent.SetDestination(hit.point);
            }
        }

    }

}
