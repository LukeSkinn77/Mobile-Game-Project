using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Player_Ground_Checker : MonoBehaviour {

	public bool ground;
	public bool doubleJump;
	public bool glideJump;

	Quaternion roation;
	public ParticleSystem par;


	void Awake()
	{
		roation = transform.rotation;
		par = GetComponent<ParticleSystem> ();
		par.Stop ();
	}

	void FixedUpdate()
	{
		transform.rotation = roation;

		RaycastHit rhit;

		//Capsule cast to check for objects of terrain tag, then 
		//Sets ground bool to true and other bools to false
		if (Physics.CapsuleCast(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z), 0.1f, -transform.up, out rhit, 1.0f))
		{
			if (rhit.transform.gameObject.tag == "Terrain")
			{
				ground = true;
				doubleJump = false;
				glideJump = false;
				par.Stop ();

//				if (Physics.gravity != new Vector3 (0.0f, -9.81f, 0.0f)) 
//				{
//					Physics.gravity = new Vector3 (0.0f, -9.81f, 0.0f);
//				}
			}
			if (rhit.transform.gameObject.name == "Boundary") 
			{
				GetComponentInParent<Main_Player_Control> ().transform.position = Game_Manager.Instance.savedPlayerLocation;
				GetComponentInParent<Main_Player_Control> ().goingForward = false;
				GetComponentInParent<Main_Player_Control> ().goingBackward = false;
			}
		}
		else
		{
			ground = false;
		}
	}
}
