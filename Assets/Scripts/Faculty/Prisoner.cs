using UnityEngine;
using System.Collections;

public class Prisoner : Faculty {

	// Use this for initialization
	void Awake(){
        SetUpFaculty("Prisoner", new Vector3(10, 1, 0), Vector3.up, 10f, 100);
    }
	
	// Update is called once per frame
	void Update () {
        UpdateMovement();

    }
}
