using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_control : MonoBehaviour {

	public GameObject Player;
	
	// Update is called once per frame
	void Update () 
	{
		//sets x position to be the same as the player's
		transform.position = new Vector3 (Player.transform.position.x, transform.position.y, transform.position.z);
	}
}
