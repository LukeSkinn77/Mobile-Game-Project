using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast_Turret_Control : Base_Turret_Script {

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

	public override void Fire()
	{
		var obj = Instantiate (Cnball, firingpoint.transform.position, firingpoint.transform.rotation);
		obj.GetComponent<Rigidbody> ().AddRelativeForce (0.0f, 0.0f, 20.0f, ForceMode.Impulse);
	}
}
