using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_camera_control : MonoBehaviour {

	public GameObject player;

	private Vector3 offset, offset_temp;

	public float speed = 1.0f;

	// Use this for initialization
	void Start () 
	{
		offset = transform.position - player.transform.position;
		offset_temp = offset;
		transform.LookAt (player.transform.position);

	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = player.transform.position + offset;
	}

	void OnTriggerEnter(Collider other)
	{

	}

	public void TurnRight()
	{
		offset = Quaternion.AngleAxis (1 * speed, Vector3.up) * offset;
		transform.position = player.transform.position + offset; 
		transform.LookAt (player.transform.position);
		Vector3 eulers = new Vector3 (0, transform.eulerAngles.y, 0);
		player.transform.rotation = Quaternion.Euler (eulers);
		//player.transform.Rotate (player.transform.rotation.x, transform.rotation.y, player.transform.rotation.z);
	}

	public void TurnLeft()
	{
		offset = Quaternion.AngleAxis (1 * speed, Vector3.down) * offset;
		transform.position = player.transform.position + offset; 
		transform.LookAt (player.transform.position);
		Vector3 eulers = new Vector3 (0, transform.eulerAngles.y, 0);
		player.transform.rotation = Quaternion.Euler (eulers);
		//player.transform.rotation = new Vector3 (player.transform.rotation.x, transform.rotation.y, player.transform.rotation.z);
	}
}
