using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus_Player_Ground_Checker : MonoBehaviour {

	public bool ground;

	void FixedUpdate()
	{
		RaycastHit rhit;

		//Capsule cast to check for objects of terrain tag, then sets bool to true
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
