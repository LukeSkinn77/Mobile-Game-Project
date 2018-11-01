using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Turret_Script : MonoBehaviour {

	public GameObject firingpoint;
	public GameObject Cnball;

	public Transform playerobj;

	public Vector3 rayheight = new Vector3(0.0f,0.1f,0.0f);

	public int rotatespeed = 30;
	public int turretaistate = 1;

	public float firerate;
	public float firetime;

	public LineRenderer lineren;

	// Use this for initialization
	void Start () 
	{
		playerobj = GameObject.Find ("Player").GetComponent<Transform> ();
		lineren = firingpoint.GetComponent<LineRenderer> ();
	}

	public void RotateToDefault()
	{
		Vector3 currenteuler = lineren.transform.eulerAngles;
		lineren.transform.rotation = Quaternion.Euler (0, currenteuler.y, 0);
	}

	public virtual void Fire()
	{
		
	}
}
