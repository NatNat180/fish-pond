﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {

    public FishDefinition traits;
    private new string name;
    private int grade;
    private KeyCode[] catchReq;
	private bool isCaught;
    private int catchTime;

	public string Name { get {return name;} }
	public int Grade { get {return grade;} }
	public KeyCode[] CatchReq { get {return catchReq;} }
	public bool IsCaught { get {return isCaught;} }
    public int CatchTime { get {return catchTime;} }

    void Start() {
        this.name = traits.name;
        this.grade = traits.grade;
        this.catchReq = traits.catchReq;
        this.catchTime = traits.catchTime;
        Debug.Log("I am a new fish! My name is " + name + ", and my grade is " + grade + "!");
    }

    void Update() {

    }

	void OnCollisionEnter(Collision collision) {
		if (Game.FishAreCatcheable && collision.collider.tag == "hook") {
			isCaught = true;
			Game.FishAreCatcheable = false;
		}
	}
}
