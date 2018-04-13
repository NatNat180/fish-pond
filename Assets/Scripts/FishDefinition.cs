using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fish", menuName = "Fish Definition")]
public class FishDefinition : ScriptableObject {

	public new string name;
	public int grade;
	public KeyCode[] catchReq;
	public int catchTime;
}
