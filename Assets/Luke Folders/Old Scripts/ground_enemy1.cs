using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground_enemy1 : MonoBehaviour {

	public GameObject Hitzone;

	void Awake()
	{
		Hitzone.SetActive (false);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			Hitzone.SetActive (true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			Hitzone.SetActive (false);
		}	
	}
}
