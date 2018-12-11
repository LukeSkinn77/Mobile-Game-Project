using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline_Player : MonoBehaviour {

	[SerializeField]
	private float jumpForce = 3.0f;

	void OnTriggerEnter(Collider other)
	{
		//Checks if it is the player, then adds force
		if (other.tag == "Player") 
		{
            GetComponent<AudioSource>().Play();
			var vel = other.gameObject.GetComponent<Rigidbody> ();
			vel.velocity = new Vector3(vel.velocity.x, 0, vel.velocity.z);
			vel.AddForce (new Vector3 (0, jumpForce, 0), ForceMode.Impulse);
		}
	}
}
