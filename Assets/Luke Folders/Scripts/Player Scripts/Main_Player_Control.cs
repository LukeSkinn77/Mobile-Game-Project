using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Player_Control : MonoBehaviour {

	public Rigidbody rb;
	Main_Player_Score_Manager ph;

	public int movetol = 140;
	public int playerpowerupstate = 0;
	public int playerpowerupstate_temp = 0;

	public float speed = 9.0f;
	public float timernum = 0.1f;
	public float jumpforce = 7.0f;

	private float currenttime;
	private float endtime;

	public bool drag;
	public bool movingleft;
	public bool movingright;

	public Space PlayerSpace;

	private Vector2 starttouch;
	private Vector2 endtouch;

	public Main_Player_Ground_Checker pgc;
	public Main_Player_Camera_Control cam;
	public Transform model;

	public Material mat_levelone;
	public Material mat_leveltwo;
	public Material mat_levelthree;

	// Use this for initialization
	void Start () 
	{
		//Gets components
		rb = GetComponent<Rigidbody> ();
		ph = GetComponent<Main_Player_Score_Manager> ();
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown(0))
		{
			//Sets the initial touch position variable
			//To the current touch position
			drag = false;
			starttouch = Input.mousePosition;
		}

		if (Input.GetMouseButton(0))
		{
			if (Time.timeScale > 0) 
			{
				//Sets the end touch position
				//To the current touch position
				endtouch = Input.mousePosition;

				if (Mathf.Abs (endtouch.x - starttouch.x) > movetol) 
				{
					//If the touch drag has moved beyond the tolerance
					//Sets the drag bool to true and checks the end touch position
					//Against the start touch position to determine if the
					//Player has dragged left or right
					drag = true;
					if ((endtouch.x - starttouch.x) > 0) 
					{
						//calls camera turn right method
						movingleft = false;
						movingright = true;
						DraggedRight ();
					}
					if ((endtouch.x - starttouch.x) < 0) 
					{
						//calls camera turn left method
						movingright = false;
						movingleft = true;
						DraggedLeft ();
					}
				}
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			if (Time.timeScale > 0) 
			{
				if (!drag) 
				{
					//If the player has not dragged from the initial position
					//Call the tap method function
					TapAction ();
				}
				//Resets values
				starttouch = Vector2.zero;
				endtouch = Vector2.zero;
				drag = false;
			}
		}

	}

	void FixedUpdate()
	{
		//Sets accelerometer values
		Vector3 rotatvalue = new Vector3 (-Input.acceleration.z * speed* Time.timeScale, 0, Input.acceleration.x * speed* Time.timeScale);
		Vector3 tiltval = new Vector3 (Input.acceleration.x * speed * Time.deltaTime * Time.timeScale, 0, -Input.acceleration.z * speed * Time.deltaTime * Time.timeScale);

		PlayerSpace = Space.Self;

		//Rotates player model
		model.Rotate (rotatvalue, PlayerSpace);

		//Translates based on accelerometer
		transform.Translate (tiltval, Space.Self);

		//Debug Keyboard Commands REMOVE WHEN FINAL BUILD
		if (Input.GetKey (KeyCode.A)) 
		{
			transform.Translate (new Vector3(-10,0,0) * Time.deltaTime, Space.Self);
		}
		if (Input.GetKey (KeyCode.D)) 
		{
			transform.Translate (new Vector3(10,0,0) * Time.deltaTime, Space.Self);
		}
		if (Input.GetKey (KeyCode.W)) 
		{
			transform.Translate (new Vector3(0,0,10) * Time.deltaTime, Space.Self);
		}
		if (Input.GetKey (KeyCode.S)) 
		{
			transform.Translate (new Vector3(0,0,-10) * Time.deltaTime, Space.Self);
		}
		if (playerpowerupstate == 5) 
		{
			Time.timeScale = 0.5f;
		}
	}

	void TapAction()
	{
		//Check Player powerup state to decide behaviour
		switch (playerpowerupstate) 
		{
		case 3: //Double Jump state
			if (pgc.ground) 
			{
				rb.velocity = new Vector3 (rb.velocity.x, 0, rb.velocity.z);
				rb.AddForce (new Vector3 (0.0f, jumpforce, 0.0f), ForceMode.Impulse);
			} 
			if (!pgc.ground && !pgc.doubleJump) 
			{
				rb.velocity = new Vector3 (rb.velocity.x, 0, rb.velocity.z);
				rb.AddForce (new Vector3 (0.0f, jumpforce, 0.0f), ForceMode.Impulse);
				pgc.doubleJump = true;
				ph.PlayerPowerUpDrain (20);
				if (ph.powerupBar <= 0) 
				{
					playerpowerupstate = playerpowerupstate_temp;
					Level_ui_manager.Current.PowerupTextChanger (playerpowerupstate_temp);
				}
			}
			break;
		case 4: //Glide state
			if (pgc.ground) 
			{
				rb.velocity = new Vector3 (rb.velocity.x, 0, rb.velocity.z);
				rb.AddForce (new Vector3 (0.0f, jumpforce, 0.0f), ForceMode.Impulse);
			} 
			if (!pgc.ground && !pgc.glideJump) 
			{
				rb.velocity = new Vector3 (rb.velocity.x, 0, rb.velocity.z);
				Physics.gravity = new Vector3 (0.0f, -6.0f, 0.0f);
				pgc.glideJump = true;
				ph.PlayerPowerUpDrain (20);
				if (ph.powerupBar <= 0) 
				{
					playerpowerupstate = playerpowerupstate_temp;
					Level_ui_manager.Current.PowerupTextChanger (playerpowerupstate_temp);
				}
			}
			break;
		default: //Main Player State
			if (pgc.ground) 
			{
				rb.velocity = new Vector3 (rb.velocity.x, 0, rb.velocity.z);
				rb.AddForce (new Vector3 (0.0f, jumpforce, 0.0f), ForceMode.Impulse);
			}
			break;
		}
	}

	void DraggedRight()
	{
		//Turns camera right
		cam.TurnRight ();
	}

	void DraggedLeft()
	{
		//Turns camera left
		cam.TurnLeft ();
	}

	public void LevelOneModifier()
	{
		//Sets material, speed and jump force for player level one
		model.gameObject.GetComponent<Renderer> ().material = mat_levelone;
		speed = 9.0f;
		jumpforce = 0.8f;
	}

	public void LevelTwoModifier()
	{
		//Sets material, speed and jump force for player level two
		model.gameObject.GetComponent<Renderer> ().material = mat_leveltwo;
		speed = 11.0f;
		jumpforce = 0.9f;
	}

	public void LevelThreeModifier()
	{
		//Sets material, speed and jump force for player level three
		model.gameObject.GetComponent<Renderer> ().material = mat_levelthree;
		speed = 13.0f;
		jumpforce = 1.0f;
	}
}
