using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_up_base : MonoBehaviour {

	public int Powerupstate = 0;

	void OnDisable()
	{
		//Respawns powerup after an amount of time
		Invoke ("Respawn", 2.0f);
	}

	void Respawn()
	{
		gameObject.SetActive (true);
	}
}
