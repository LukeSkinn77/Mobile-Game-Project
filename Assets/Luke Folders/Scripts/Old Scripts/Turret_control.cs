using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_control : MonoBehaviour {

	public GameObject firingpoint;
	public GameObject Cnball;

	public Transform playerobj;

	Vector3 rayheight = new Vector3(0.0f,0.1f,0.0f);

	public int rotatespeed = 30;
	public int turretaistate = 1;

	public float firerate;
	private float firetime;

	LineRenderer lineren;

	// Use this for initialization
	void Start () 
	{
		lineren = firingpoint.GetComponent<LineRenderer> ();
	}

	void FixedUpdate () 
	{
		//Sets up a raycast
		Ray ray1 = new Ray (firingpoint.transform.position - rayheight, firingpoint.transform.forward);
		float raylgh = 9.0f;
		lineren.SetPosition (0, ray1.origin);
		lineren.SetPosition (1, ray1.GetPoint (raylgh));
		RaycastHit rayhit;

		switch (turretaistate) 
		{
		case 1:
			transform.Rotate (new Vector3 (0, rotatespeed, 0) * Time.deltaTime);
			if (Physics.Raycast (ray1, out rayhit, raylgh)) 
			{
				lineren.SetPosition (1, rayhit.point);
				//Checks for collision with player, if so, turret ai state set to 2
				if (rayhit.transform.gameObject.tag == "Player") 
				{
					turretaistate = 2;
				}
			}
			break;
		case 2:
			raylgh = 90.0f;
			Vector3 playerposition = playerobj.position - firingpoint.transform.position;
			Quaternion rot = Quaternion.LookRotation (playerposition);

			Vector3 euler = rot.eulerAngles;
			transform.rotation = Quaternion.Euler (0, euler.y, 0);
			firingpoint.transform.rotation = rot;

			if (Physics.Raycast (ray1, out rayhit, raylgh)) 
			{
				lineren.SetPosition (1, rayhit.point);
			}

			if (Time.time > firetime) 
			{
				Fire ();
				firetime = Time.time + firerate;
			}
			break;
		}
	}

	public void RotateToDefault()
	{
		Vector3 currenteuler = lineren.transform.eulerAngles;
		lineren.transform.rotation = Quaternion.Euler (0, currenteuler.y, 0);
	}

	void Fire()
	{
		var obj = Instantiate (Cnball, firingpoint.transform.position, firingpoint.transform.rotation);
		obj.GetComponent<Rigidbody> ().AddRelativeForce (0.0f, 20.0f, 20.0f, ForceMode.Impulse);
	}
}
