using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Area_Control : MonoBehaviour {

	public GameObject firingpoint;
	public GameObject Cnball;

	public Transform playerobj;

	Vector3 rayheight = new Vector3(0.0f,0.1f,0.0f);

	public int rotatespeed = 30;
	public int turretaistate = 1;

	public float firerate;
	private float firetime;

	float angle = 45.0f;


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

		float aim = Vector3.Distance (firingpoint.transform.position, playerobj.position);

		float tempval1 = Mathf.Sqrt(aim * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * angle * 2)));
		float velocy, velocz;

		velocy = tempval1 * Mathf.Sin (Mathf.Deg2Rad * angle);
		velocz = tempval1 * Mathf.Cos (Mathf.Deg2Rad * angle);

		Vector3 lv = new Vector3 (0.0f, velocy, velocz);

		Vector3 gv = transform.TransformVector (lv);

		obj.GetComponent<Rigidbody> ().velocity = gv;

		//float squaredGravity = Physics.gravity.sqrMagnitude;
		//float targetDot = 5.0f * 5.0f + Vector3.Dot (playerobj.position, Physics.gravity);


		//obj.GetComponent<Rigidbody> ().velocity = aim;

	}
}
