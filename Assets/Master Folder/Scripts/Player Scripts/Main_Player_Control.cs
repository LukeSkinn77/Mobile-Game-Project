using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PS4;


public class Main_Player_Control : MonoBehaviour {

	public Rigidbody rb;
	AudioSource audi;

	public int movetol = 140;
	public int playerpowerupstate = 0;
	public int playerpowerupstate_temp = 0;

	public float speed = 8.0f;
	public float timernum = 0.1f;
	public float jumpforce = 7.0f;

	private float currenttime;
	private float endtime;

	public bool goingForward;
	public bool goingBackward;
	public bool direcho;

	public Space PlayerSpace;

	private Vector2 starttouch;
	private Vector2 endtouch;

	private Vector3 initialDirection;

	public Main_Player_Ground_Checker pgc;
	Main_Player_Score_Manager ph;
	public Main_Player_Camera_Control cam;
	public Transform model;

	public Material mat_levelone;
	public Material mat_leveltwo;
	public Material mat_levelthree;

	public GameObject explo;

	public AudioClip audioJump;
    public AudioClip audioFloat;
    public AudioClip audioLevel;

    public Color normalColour;
    public Color doubleJumpColour;

    private void Awake()
    {
        audi = GetComponent<AudioSource>();
        LevelOneModifier();
    }

    // Use this for initialization
    void Start () 
	{
        PlayerTexture();
        PlayerPosition();
		//initialDirection = Input.acceleration; //new Vector3 (Input.acceleration.x, 0.0f, Input.acceleration.z);
		Game_Manager.Instance.savedPlayerLocation = transform.position;
		Game_Manager.Instance.SavePlayer ();
		
        //Gets components
		rb = GetComponent<Rigidbody>();
		ph = GetComponent<Main_Player_Score_Manager> ();
        PS4Input.PadResetOrientation(0);

    }

    void PlayerTexture()
    {
        //Sets player texture to the one stored in the game manager
        mat_levelone = Game_Manager.Instance.currentSkin.oneLevel;
        mat_leveltwo = Game_Manager.Instance.currentSkin.twoLevel;
        mat_levelthree = Game_Manager.Instance.currentSkin.threeLevel;
    }

    void PlayerPosition()
	{
		if (Game_Manager.Instance.savedPlayerLocation == Vector3.zero)
		{
			return;
		}
		transform.position = Game_Manager.Instance.savedPlayerLocation;
	}

	// Update is called once per frame
	void Update () 
	{
        if (Time.timeScale > 0.0f)
        {
            PlayerControllerMovement();
            CameraControl();
        }
	}
		
	//void TouchControls()
	//{
		//if (Input.touchCount > 0)
		//{
		//	Touch touch = Input.GetTouch(0);
		//	switch (touch.phase)
		//	{
		//	case TouchPhase.Began:
		//		starttouch = touch.position;
		//		direcho = false;
		//		break;
		//	case TouchPhase.Moved:
		//		endtouch = touch.position - starttouch;
		//		break;
		//	case TouchPhase.Ended:
  //              //Checks Y and X values of endtouch to decide on action
		//		if (endtouch.y >= 100) 
		//		{
		//			goingForward = true;
		//			goingBackward = false;
		//		} 
		//		else if (endtouch.y <= -100) 
		//		{
		//			goingBackward = true;
		//			goingForward = false;
		//		}

  //              if ((endtouch.x >= 50) || (endtouch.x <= -50))
  //              {
  //                  cam.RotateOverTime(endtouch.x / 20);
  //              }
  //              //If little to no movement, jump
  //              if ((endtouch.x <= 50) && (endtouch.x >= -50) && (endtouch.y <= 100) && (endtouch.y >= -100)) 
		//		{
		//			TapAction ();
		//		} 
		//		direcho = true;
  //              //Resets endtouch
		//		endtouch = Vector2.zero;
		//		break;
		//	}
		//}	
	//}


	void FixedUpdate()
	{
		PlayerXMovement ();
		PlayerZMovement ();
		GlideCheck ();
	}

	void PlayerZMovement()
	{
        // declare a z value holder
        float zval;

        // move the player up and down using the left thumbstick
        // up is positive, down is negative...
#if UNITY_PS4
        if (PS4Input.PadIsConnected(0))
        {
            zval = Input.GetAxis("LeftVertical");
            float newVal = Mathf.Clamp(zval, -1, 1);
            transform.Translate(new Vector3(0, 0, zval * speed * Time.deltaTime));
            model.transform.Rotate(new Vector3(zval, 0, 0 * speed * Time.deltaTime));
        }
#endif
//#if UNITY_EDITOR
//        zval = Input.GetAxis("Vertical");
//        float nextVal = Mathf.Clamp(zval, -1, 1);
//        transform.Translate(new Vector3(0, 0, zval * speed * Time.deltaTime));
//        model.transform.Rotate(new Vector3(zval, 0, 0 * speed * Time.deltaTime));
//#endif
    }

    void PlayerXMovement()
	{
        // check if pad is connected
        // move the player with the sixaxis

        if (PS4Input.PadIsConnected(0))
        {
            Debug.Log("Controller connected");
            Vector4 controllerPos = PS4Input.PadGetLastOrientation(0);
            float xval = Mathf.Clamp(controllerPos.x * 2, -1, 1);
            transform.Translate(new Vector3(xval, 0, 0) * speed * Time.deltaTime, Space.Self);
            model.transform.Rotate(new Vector3(0, 0, xval * speed * Time.deltaTime));
        }
        else
        {
            Debug.Log("Controller not connected");
        }
        //float xval = Input.acceleration.x;
    }

	void PlayerControllerMovement()
	{
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            PlayerJumpAction();
        }

        //Debug Keyboard Commands REMOVE WHEN FINAL BUILD
        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.Translate(new Vector3(-10, 0, 0) * Time.deltaTime, Space.Self);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.Translate(new Vector3(10, 0, 0) * Time.deltaTime, Space.Self);
        //}
        //if (Input.GetKey(KeyCode.W))
        //{
        //    transform.Translate(new Vector3(0, 0, 10) * Time.deltaTime, Space.Self);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    transform.Translate(new Vector3(0, 0, -10) * Time.deltaTime, Space.Self);
        //}

    }
		
	void PlayerJumpAction()
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
				audi.clip = audioJump;
				audi.Play ();
				rb.velocity = new Vector3 (rb.velocity.x, 0, rb.velocity.z);
				rb.AddForce (new Vector3 (0.0f, jumpforce, 0.0f), ForceMode.Impulse);
				pgc.doubleJump = true;
				ph.PlayerPowerUpDrain (20);
				//Instantiate (explo, transform.position, transform.rotation);
                GameObject particle = Pickups_Particle_Pooling.pickupPool.GetCloudParticle();
                if (particle == null) return;
                particle.transform.position = this.transform.position;
                particle.transform.rotation = this.transform.rotation;
                particle.SetActive(true);
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
                audi.clip = audioFloat;
                audi.Play();
                rb.velocity = new Vector3 (rb.velocity.x, 0, rb.velocity.z);
				pgc.par.Play ();
				//Physics.gravity = new Vector3 (0.0f, -6.0f, 0.0f);
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

    IEnumerator LightChanger()
    {
        PS4Input.PadSetLightBar(0, (int)doubleJumpColour.r, (int)doubleJumpColour.g, (int)doubleJumpColour.b);
        yield return new WaitForSeconds(0.5f);
        PS4Input.PadSetLightBar(0, (int)normalColour.r, (int)normalColour.g, (int)normalColour.b);
    }

    void GlideCheck()
	{
		//Checks that the player is gliding, then adds a force upwards
		if (pgc.glideJump) 
		{
			rb.AddForce(-Physics.gravity/11.0f);
		}
	}

	public void LevelOneModifier()
	{
        audi.clip = audioLevel;
        audi.Play();
        //Sets material, speed and jump force for player level one
        model.gameObject.GetComponent<Renderer> ().material = mat_levelone;
		speed = 8.0f;
		jumpforce = 0.8f;
	}

	public void LevelTwoModifier()
	{
        audi.clip = audioLevel;
        audi.Play();
        //Sets material, speed and jump force for player level two
        model.gameObject.GetComponent<Renderer> ().material = mat_leveltwo;
		speed = 10.0f;
		jumpforce = 0.9f;
	}

	public void LevelThreeModifier()
	{
        audi.clip = audioLevel;
        audi.Play();
        //Sets material, speed and jump force for player level three
        model.gameObject.GetComponent<Renderer> ().material = mat_levelthree;
		speed = 12.0f;
		jumpforce = 1.0f;
	}

    void CameraControl ()
    {
        float camValue;
#if UNITY_PS4
        camValue = Input.GetAxis("RightHorizontal") * speed;
        transform.Rotate(0, camValue, 0);
#endif
#if UNITY_EDITOR
        camValue = Input.GetAxis("Mouse X") * speed;
        transform.Rotate(0, camValue, 0);
#endif

    }



    void OldDraggedRight()
	{
		//Turns camera right
		cam.OldTurnRight ();
	}

	void OldDraggedLeft()
	{
		//Turns camera left
		cam.OldTurnLeft ();
	}

	//	void OldTouchControls()
	//	{
	//		if (Input.GetMouseButtonDown(0))
	//		{
	//			//Sets the initial touch position variable
	//			//To the current touch position
	//			drag = false;
	//			starttouch = Input.mousePosition;
	//		}
	//
	//		if (Input.GetMouseButton(0))
	//		{
	//			if (Time.timeScale > 0) 
	//			{
	//				//Sets the end touch position
	//				//To the current touch position
	//				endtouch = Input.mousePosition;
	//
	//				if (Mathf.Abs (endtouch.x - starttouch.x) > movetol) 
	//				{
	//					//If the touch drag has moved beyond the tolerance
	//					//Sets the drag bool to true and checks the end touch position
	//					//Against the start touch position to determine if the
	//					//Player has dragged left or right
	//					drag = true;
	//					if ((endtouch.x - starttouch.x) > 0) 
	//					{
	//						//calls camera turn right method
	//						movingleft = false;
	//						movingright = true;
	//						DraggedRight ();
	//					}
	//					if ((endtouch.x - starttouch.x) < 0) 
	//					{
	//						//calls camera turn left method
	//						movingright = false;
	//						movingleft = true;
	//						DraggedLeft ();
	//					}
	//				}
	//			}
	//		}
	//
	//		if (Input.GetMouseButtonUp(0))
	//		{
	//			if (Time.timeScale > 0) 
	//			{
	//				if (!drag) 
	//				{
	//					//If the player has not dragged from the initial position
	//					//Call the tap method function
	//					TapAction ();
	//				}
	//				//Resets values
	//				starttouch = Vector2.zero;
	//				endtouch = Vector2.zero;
	//				drag = false;
	//			}
	//		}
	//	}

	//	void OldPlayerMovement()
	//	{
	//		Vector3 direction = Vector3.zero;
	//
	//		direction.x = Input.acceleration.x;
	//		if ((Input.deviceOrientation == DeviceOrientation.LandscapeLeft) || (Input.deviceOrientation == DeviceOrientation.LandscapeRight)) 
	//		{
	//			direction.z = Input.acceleration.z;
	//		} 
	//		else 
	//		{
	//			direction.z = -Input.acceleration.y;
	//		}
	//
	//		if (direction.magnitude > 1) 
	//		{
	//			direction.Normalize ();
	//		}
	//
	//		//Sets accelerometer values
	//		Vector3 rotatvalue = new Vector3 (-direction.z * speed * Time.timeScale, 0, direction.x * speed* Time.timeScale);
	//		Vector3 tiltval = new Vector3 (direction.x * Time.deltaTime, 0, -direction.z * Time.deltaTime);
	//
	//		PlayerSpace = Space.Self;
	//
	//		//Rotates player model
	//		model.Rotate (rotatvalue, PlayerSpace);
	//
	//		//Translates based on accelerometer
	//		transform.Translate (tiltval * speed, Space.Self);
	//	}
}
