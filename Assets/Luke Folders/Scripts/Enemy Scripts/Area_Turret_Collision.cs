using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area_Turret_Collision : Base_Turret_Collision {

	void OnTriggerEnter(Collider other)
	{
		//Sets turret ai state to 2
		if (other.tag == "Player") 
		{
			if (turretcon.turretaistate == 1) 
			{
				turretcon.turretaistate = 2;
			}
		}
	}
}
