using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area_Turret_Control : Base_Turret_Script {

	public float angle = 45.0f;
    public GameObject target;

	void FixedUpdate () 
	{
		//Sets up a raycast and a line
		Ray ray1 = new Ray (firingpoint.transform.position - rayheight, firingpoint.transform.forward);

		float raylgh = 9.0f;
		lineren.SetPosition (0, ray1.origin);
		lineren.SetPosition (1, ray1.GetPoint (raylgh));
		RaycastHit rayhit;

		switch (turretaistate) 
		{
		case 1:
			//Rotates while casting a ray and line
			transform.Rotate (new Vector3 (0, rotatespeed, 0) * Time.deltaTime);
            target.transform.localPosition = new Vector3(0, -1.2f, 0);
            if (Physics.Raycast (ray1, out rayhit, raylgh)) 
			{
				lineren.SetPosition (1, rayhit.point);
                //target.transform.position = lineren.GetPosition(1);
            }
            break;
		case 2:
			//Sets ray to point towards the player's position
			raylgh = 90.0f;
			Vector3 playerposition = playerobj.position - firingpoint.transform.position;
			Quaternion rot = Quaternion.LookRotation (playerposition);

			//Sets rotation of the objects to point towards the player
			Vector3 euler = rot.eulerAngles;
			transform.rotation = Quaternion.Euler (0, euler.y, 0);
			firingpoint.transform.rotation = rot;

			//Line ends at the player object
			if (Physics.Raycast (ray1, out rayhit, raylgh)) 
			{
				lineren.SetPosition (1, rayhit.point);
                target.transform.position = new Vector3(rayhit.transform.position.x, rayhit.transform.position.y - 0.3f, rayhit.transform.position.z);
            }
                //Resets firerate after firing
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
		//var obj = Instantiate (Cnball, firingpoint.transform.position, firingpoint.transform.rotation);
        GameObject obj = Pickups_Particle_Pooling.pickupPool.GetCannonball();
        if (obj == null) return;
        obj.transform.position = firingpoint.transform.position;
        obj.transform.rotation = firingpoint.transform.rotation;
        obj.SetActive(true);

        //calculates space inbetween player and firepoint
        float aim = Vector3.Distance (firingpoint.transform.position, playerobj.position);

		//Calculates the required velocity Y and Z values using the distance inbetween the objects,
		//The current gravity and the angle that the angle the projectile will fire from
		float tempval1 = Mathf.Sqrt(aim * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * angle * 2)));
		float velocy, velocz;
		velocy = tempval1 * Mathf.Sin (Mathf.Deg2Rad * angle);
		velocz = tempval1 * Mathf.Cos (Mathf.Deg2Rad * angle);
		Vector3 lv = new Vector3 (0.0f, velocy, velocz);
		Vector3 gv = transform.TransformVector (lv);

		//Adds the force to the projectile
		obj.GetComponent<Rigidbody> ().velocity = gv;
	}
}
