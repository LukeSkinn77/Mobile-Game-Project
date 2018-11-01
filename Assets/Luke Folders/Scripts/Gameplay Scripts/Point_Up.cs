using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point_Up : MonoBehaviour {

	//Sets transform to quaternion identity
	void Update () 
	{
		transform.rotation = Quaternion.identity;
	}
}
