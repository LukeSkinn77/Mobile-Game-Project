using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Simple : MonoBehaviour {

	public Vector3 rotateValues;

	// Performs a simple rotation in a given direction
	void FixedUpdate () 
	{
		transform.Rotate (rotateValues);
	}
}
