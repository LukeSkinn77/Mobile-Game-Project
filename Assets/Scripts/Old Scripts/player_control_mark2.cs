using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_control_mark2 : MonoBehaviour {

	public Rigidbody rb;
	Player_health ph;

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

	public Player_ground_control_mark2 pgc;
	public Main_Player_Camera_Control cam;
	public Transform model;

	public Material mat_levelone;
	public Material mat_leveltwo;
	public Material mat_levelthree;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		ph = GetComponent<Player_health> ();
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
			if (Time.timeScale > 0) 
			{
				//starttouch = Input.mousePosition;
				endtouch = Input.mousePosition;
				//Debug.Log ("Starttouch.x = " + starttouch.x);
				//Debug.Log ("Endtouch.x = " + endtouch.x);
				//Debug.Log ("Calculation is " + (Mathf.Abs (endtouch.x - starttouch.x) > movetol));
				if (Mathf.Abs (endtouch.x - starttouch.x) > movetol) 
				{
					drag = true;
					if ((endtouch.x - starttouch.x) > 0) 
					{
						movingleft = false;
						movingright = true;
						DraggedRight ();
					}
					if ((endtouch.x - starttouch.x) < 0) 
					{
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
					TapAction ();
				}
				starttouch = Vector2.zero;
				endtouch = Vector2.zero;
				drag = false;
			}
		}

	}

	void FixedUpdate()
	{
		Vector3 rotatvalue = new Vector3 (-Input.acceleration.z * speed, 0, Input.acceleration.x * speed);
		Vector3 tiltval = new Vector3 (Input.acceleration.x * speed * Time.deltaTime, 0, -Input.acceleration.z * speed * Time.deltaTime);

		PlayerSpace = Space.Self;

		model.Rotate (rotatvalue, PlayerSpace);

		transform.Translate (tiltval, Space.Self);

		//Debug Keyboard Commands REMOVE WHEN FINAL BUILD
		if (Input.GetKey (KeyCode.A)) 
		{
			//model.Rotate (new Vector3(-10,0,0) * Time.deltaTime, Space.Self);
			transform.Translate (new Vector3(-10,0,0) * Time.deltaTime, Space.Self);
		}
		if (Input.GetKey (KeyCode.D)) 
		{
			//model.Rotate (new Vector3(10,0,0) * Time.deltaTime, Space.Self);
			transform.Translate (new Vector3(10,0,0) * Time.deltaTime, Space.Self);
		}
		if (Input.GetKey (KeyCode.W)) 
		{
			//model.Rotate (new Vector3(0,0,10) * Time.deltaTime, Space.Self);
			transform.Translate (new Vector3(0,0,10) * Time.deltaTime, Space.Self);
		}
		if (Input.GetKey (KeyCode.S)) 
		{
			//model.Rotate (new Vector3(0,0,-10) * Time.deltaTime, Space.Self);
			transform.Translate (new Vector3(0,0,-10) * Time.deltaTime, Space.Self);
		}
	}

	void TapAction()
	{
		//Check Player powerup state to decide behaviour
		switch (playerpowerupstate) 
		{
//		case 0: //Normal State
//			if (pgc.ground) 
//			{
//				rb.velocity = new Vector3 (rb.velocity.x, 0, rb.velocity.z);
//				rb.AddForce (new Vector3 (0.0f, jumpforce, 0.0f), ForceMode.Impulse);
//			}
//			break;
		case 3: //Double Jump state
			if (pgc.ground) 
			{
				rb.velocity = new Vector3 (rb.velocity.x, 0, rb.velocity.z);
				rb.AddForce (new Vector3 (0.0f, jumpforce, 0.0f), ForceMode.Impulse);
			} 
			if (!pgc.ground && !pgc.doublejump) 
			{
				rb.velocity = new Vector3 (rb.velocity.x, 0, rb.velocity.z);
				rb.AddForce (new Vector3 (0.0f, jumpforce, 0.0f), ForceMode.Impulse);
				pgc.doublejump = true;
				ph.PlayerPowerUpDrain (20);
				if (ph.powerupbar <= 0) 
				{
					playerpowerupstate = playerpowerupstate_temp;
					Level_ui_manager.Current.PowerupTextChanger (playerpowerupstate_temp);
				}
			}
			break;
		default:
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

	public void LevelOneModifier()
	{
		model.gameObject.GetComponent<Renderer> ().material = mat_levelone;
		speed = 9.0f;
		jumpforce = 0.8f;
	}

	public void LevelTwoModifier()
	{
		model.gameObject.GetComponent<Renderer> ().material = mat_leveltwo;
		speed = 11.0f;
		jumpforce = 0.9f;
	}

	public void LevelThreeModifier()
	{
		model.gameObject.GetComponent<Renderer> ().material = mat_levelthree;
		speed = 13.0f;
		jumpforce = 1.0f;
	}
}
//Sets the score slider to the score
//if (scoreSlider.value > maxvalueinp) 
//{
//	//scoreSlider.value = 0;
//} 
//else 
//{
//	scoreSlider.value = score;
//}