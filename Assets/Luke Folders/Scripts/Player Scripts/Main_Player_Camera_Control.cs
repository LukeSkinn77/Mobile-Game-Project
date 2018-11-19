using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Player_Camera_Control : MonoBehaviour {

	public GameObject player;
	//Rigidbody rb;

	private Vector3 offset, offset_temp;

	public float speed = 2.0f;
	bool isRotating = false;
	float rotateDistance;
	float startYpoint;
	Quaternion endRotate;

	// Use this for initialization
	void Start () 
	{
		//Sets the offset to the distance between the camera and player
		//Then sets the camera to look at the player
		offset = transform.position - player.transform.position;
		offset_temp = offset;
		transform.LookAt (player.transform.position);
		//rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () 
	{
		//Continuously maintains a position equivalent to the offset distance
		//transform.position = player.transform.position + offset;
		if (isRotating) 
		{
			Rotate ();
		}

	}
	void Rotate()
	{
//		float angle = Mathf.Lerp(transform.rotation.eulerAngles.y, startYpoint + rotateDistance, Time.deltaTime * speed);
//		Debug.Log (startYpoint + rotateDistance);
//		Debug.Log ("Angle " + angle);
//		transform.rotation = Quaternion.Euler(new Vector3 (transform.rotation.eulerAngles.x, angle, transform.rotation.eulerAngles.z));
		rotateDistance = Mathf.Lerp(rotateDistance, 0, Time.deltaTime* speed);
		//print (rotateDistance);
		player.transform.RotateAround (player.transform.position, Vector3.up, rotateDistance);
		if (Mathf.Abs (rotateDistance) < 0.01f) 
		{
			isRotating = false;
		}
		Vector3 eulers = new Vector3 (0, transform.eulerAngles.y, 0);
		//player.transform.rotation = Quaternion.Euler (eulers);
	}

	public void RotateOverTime(float xval)
	{
		//startYpoint = transform.eulerAngles.y;
		isRotating = true;
		rotateDistance = xval;
		endRotate = transform.rotation * Quaternion.AngleAxis (rotateDistance,this.transform.up);
		endRotate.z = Quaternion.identity.z;
		endRotate.x = Quaternion.identity.x;

		Debug.Log (endRotate);

	}

	public void OldTurnRight()
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

	public void OldTurnLeft()
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