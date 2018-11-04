using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder_Player : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		//Sets Player to be parented to the game object
		if (other.tag == "Player") 
		{
			other.transform.parent = gameObject.transform.parent;
		}
	}

	void OnTriggerExit(Collider other)
	{
		//Sets Player to not be parented to the game object
		if (other.tag == "Player") 
		{
			other.transform.parent = null;
		}
	}
}
