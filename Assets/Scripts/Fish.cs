using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fish", menuName = "Fish")]
public class Fish : ScriptableObject {

	public new string name;
	public int grade;
	public KeyCode[] catchReq;
}
