using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_collision : MonoBehaviour {

	Turret_control turretcon;

	// Use this for initialization
	void Start () 
	{
		turretcon = gameObject.GetComponentInParent<Turret_control> ();
	}
	
	void OnTriggerExit(Collider other)
	{
		//Sets turret ai state to 1 and calls
		//rotate to default function on the turret object
		if (other.tag == "Player") 
		{
			if (turretcon.turretaistate == 2) 
			{
				turretcon.turretaistate = 1;
				turretcon.RotateToDefault ();
			}
		}
	}
}
