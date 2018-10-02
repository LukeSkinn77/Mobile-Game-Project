using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_control_mark2 : MonoBehaviour {

	Rigidbody rb;

	public int movetol = 140;

	public float speed = 3.0f;
	public float timernum = 0.1f;
	public float jumpforce = 7.0f;

	private float currenttime;
	private float endtime;

	public bool drag;
	public bool movingleft;
	public bool movingright;

	private Vector2 starttouch;
	private Vector2 endtouch;

	public Player_ground_control_mark2 pgc;
	public ball_camera_control cam;
	public Transform model;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//if (Input.deviceOrientation == DeviceOrientation.FaceUp) 
		{

		}

		if (Input.GetMouseButtonDown(0))
		{
			drag = false;
			starttouch = Input.mousePosition;
			Debug.Log ("Starttouch.x = " + starttouch.x);
			//endtouch = Input.mousePosition;
			//StartCoroutine(Tap(timernum));
		}

		if (Input.GetMouseButton(0))
		{
			//starttouch = Input.mousePosition;
			endtouch = Input.mousePosition;
			//Debug.Log ("Starttouch.x = " + starttouch.x);
			//Debug.Log ("Endtouch.x = " + endtouch.x);
			//Debug.Log ("Calculation is " + (Mathf.Abs (endtouch.x - starttouch.x) > movetol));
			if (Mathf.Abs(endtouch.x - starttouch.x) > movetol)
			{
				drag = true;
				if ((endtouch.x - starttouch.x) > 0)
				{
					movingleft = false;
					movingright = true;
					DraggedRight();
				}
				if ((endtouch.x - starttouch.x) < 0)
				{
					movingright = false;
					movingleft = true;
					DraggedLeft();
				}

				//starttouch = endtouch;
			}
			//starttouch = endtouch;
		}

		if (Input.GetMouseButtonUp(0))
		{
			if (!drag)
			{
				TapAction();
			}
			starttouch = Vector2.zero;
			endtouch = Vector2.zero;
			drag = false;
		}

	}

	void FixedUpdate()
	{
		//Vector3 tiltval = Input.acceleration;
		//Vector3 tiltval = Vector3.zero;
		Vector3 rotatvalue = new Vector3 (-Input.acceleration.z * speed, 0, -Input.acceleration.x * speed);
		Vector3 tiltval = new Vector3 (Input.acceleration.x * speed * Time.deltaTime, 0, -Input.acceleration.z * speed * Time.deltaTime);
		//tiltval.Normalize ();
		//tiltval = Quaternion.Euler (90, rb.velocity.y, 0) * tiltval;
		//Vector3 move = new Vector3 (Input.acceleration.x, 0, -Input.acceleration.z);
		//rb.AddRelativeForce (tiltval, ForceMode.Force);

		//rb.AddRelativeForce (tiltval, ForceMode.Force);
		model.Rotate (rotatvalue, Space.Self);

		transform.Translate (tiltval, Space.Self);
		//Debug Keyboard Commands REMOVE WHEN FINAL BUILD
		if (Input.GetKey (KeyCode.A)) 
		{
			model.Rotate (new Vector3(-10,0,0) * Time.deltaTime, Space.Self);
			transform.Translate (new Vector3(-10,0,0) * Time.deltaTime, Space.Self);
		}
		if (Input.GetKey (KeyCode.D)) 
		{
			model.Rotate (new Vector3(10,0,0) * Time.deltaTime, Space.Self);
			transform.Translate (new Vector3(10,0,0) * Time.deltaTime, Space.Self);
		}
		if (Input.GetKey (KeyCode.W)) 
		{
			model.Rotate (new Vector3(0,0,10) * Time.deltaTime, Space.Self);
			transform.Translate (new Vector3(0,0,10) * Time.deltaTime, Space.Self);
		}
		if (Input.GetKey (KeyCode.S)) 
		{
			model.Rotate (new Vector3(0,0,-10) * Time.deltaTime, Space.Self);
			transform.Translate (new Vector3(0,0,-10) * Time.deltaTime, Space.Self);
		}
	}

	void TapAction()
	{
		if (pgc.ground)
		{
			rb.velocity = new Vector3 (rb.velocity.x, 0, rb.velocity.z);
			rb.AddForce(new Vector3(0.0f, jumpforce, 0.0f), ForceMode.Impulse);
		}
	}

	void DraggedRight()
	{
		cam.TurnRight ();
		//Debug.Log("Charged Right");
		//rb.velocity = new Vector3(5.0f * 2, rb.velocity.y, rb.velocity.z);
		//rb.AddRelativeForce(new Vector3(5.0f * Time.time, 0, 0), ForceMode.Force);
	}

	void DraggedLeft()
	{
		cam.TurnLeft ();
		//Debug.Log("Charged Left");
		//rb.velocity = new Vector3(-5.0f * 2, rb.velocity.y, rb.velocity.z);
		//rb.AddForce(new Vector3(-5.0f * Time.time, 0, 0), ForceMode.Force);
	}
}
