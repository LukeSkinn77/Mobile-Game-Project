using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ground_control_mark2 : MonoBehaviour {

	public bool ground;
	public bool doublejump;

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
				doublejump = false;
			}
		}
		else
		{
			ground = false;
		}
	}
}
