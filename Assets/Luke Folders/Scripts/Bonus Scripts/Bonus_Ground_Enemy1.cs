using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus_Ground_Enemy1 : MonoBehaviour {

	public GameObject Hitzone;

	void Awake()
	{
		//Turns the hitzone off
		Hitzone.SetActive (false);
	}

	void OnTriggerEnter(Collider other)
	{
		//On player contact with trigger zone, turns on hitzone
		if (other.gameObject.tag == "Player") 
		{
			Hitzone.SetActive (true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		//On player exit of trigger zone, turns off hitzone
		if (other.gameObject.tag == "Player") 
		{
			Hitzone.SetActive (false);
		}	
	}
}
