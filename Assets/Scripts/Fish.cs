﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {

    public FishDefinition traits;
    private string fishName;
    private int grade;
    private KeyCode[] catchReq;
    private int catchTime;

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

    }

}
