using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Player_Camera_Control : MonoBehaviour {

	public GameObject player;

	private Vector3 offset, offset_temp;

	public float speed = 2.0f;

	// Use this for initialization
	void Start () 
	{
		//Sets the offset to the distance between the camera and player
		//Then sets the camera to look at the player
		offset = transform.position - player.transform.position;
		offset_temp = offset;
		transform.LookAt (player.transform.position);
	}

	// Update is called once per frame
	void Update () 
	{
		//Continuously maintains a position equivalent to the offset distance
		transform.position = player.transform.position + offset;
	}

	public void TurnRight()
	{
		//Alters the camera's up rotation axis to move right while
		//Still pointed towards the player, then sets the player's rotation
		//To be the same as the camera
		offset = Quaternion.AngleAxis (1 * speed, Vector3.up) * offset;
		transform.position = player.transform.position + offset; 
		transform.LookAt (player.transform.position);
		Vector3 eulers = new Vector3 (0, transform.eulerAngles.y, 0);
		player.transform.rotation = Quaternion.Euler (eulers);
	}

	public void TurnLeft()
	{
		//Alters the camera's down rotation axis to move left while
		//Still pointed towards the player, then sets the player's rotation
		//To be the same as the camera
		offset = Quaternion.AngleAxis (1 * speed, Vector3.down) * offset;
		transform.position = player.transform.position + offset; 
		transform.LookAt (player.transform.position);
		Vector3 eulers = new Vector3 (0, transform.eulerAngles.y, 0);
		player.transform.rotation = Quaternion.Euler (eulers);
	}
}