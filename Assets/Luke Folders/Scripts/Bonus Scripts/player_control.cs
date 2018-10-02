using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_control : MonoBehaviour {

    private Vector2 starttouch;
    private Vector2 endtouch;

    //private int horizontalno = 0;

    public int movetol = 140;
    public float timernum = 0.1f;
	public float jumpforce = 7.0f;

    private float currenttime;
    private float endtime;

    public bool drag;
    public bool movingleft;
    public bool movingright;

    Rigidbody rb;

    public player_ground_control pgc;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

	void Update()
	{
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
			Debug.Log ("Endtouch.x = " + endtouch.x);
			Debug.Log ("Calculation is " + (Mathf.Abs (endtouch.x - starttouch.x) > movetol));
			if (Mathf.Abs(endtouch.x - starttouch.x) > movetol)
			{
				drag = true;
				if ((endtouch.x - starttouch.x) > 0)
				{
					movingleft = false;
					movingright = true;
					//DraggedRight();
				}
				if ((endtouch.x - starttouch.x) < 0)
				{
					movingright = false;
					movingleft = true;
					//DraggedLeft();
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
        if (movingleft)
        {
            DraggedLeft();
        }
        if (movingright)
        {
            DraggedRight();
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
        //Debug.Log("Charged Right");
        rb.velocity = new Vector3(5.0f * 2, rb.velocity.y, rb.velocity.z);
       // rb.AddForce(new Vector3(5.0f * Time.time, 0, 0), ForceMode.Force);
    }

    void DraggedLeft()
    {
        //Debug.Log("Charged Left");
        rb.velocity = new Vector3(-5.0f * 2, rb.velocity.y, rb.velocity.z);
       // rb.AddForce(new Vector3(-5.0f * Time.time, 0, 0), ForceMode.Force);
    }
}
