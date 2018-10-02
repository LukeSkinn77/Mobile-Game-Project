using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ground_control_mark2 : MonoBehaviour {

	//Vector3 rayheight = new Vector3(0.0f, 0.1f, 0.0f);
	public bool ground;

	Quaternion roation;

	void Awake()
	{
		roation = transform.rotation;
	}

	void FixedUpdate()
	{
		transform.rotation = roation;

		RaycastHit rhit;

		if (Physics.CapsuleCast(transform.position, new Vector3(transform.position.x, transform.position.y/* - 0.1f*/, transform.position.z), 0.1f, -transform.up, out rhit, 1.0f))
		{
			if (rhit.transform.gameObject.tag == "Terrain")
			{
				ground = true;
			}
		}
		else
		{
			ground = false;
		}
	}
}
