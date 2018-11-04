using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Camera_Control : MonoBehaviour {

	public Transform centre;

	private Vector3 offset;

	public float speed = 2.0f;
	public float lerpSpeed = 20f;
	//float startTime;
	float tim = 0f;

	public int centrePosition;

	private Vector3 menuIsland, optionIsland, gameIsland;

	// Use this for initialization
	void Start () 
	{
		//Sets the offset to the distance between the camera and camera centre
		//Then sets the camera to look at the camera centre
		offset = transform.position - centre.position;
		transform.LookAt (centre.position);
		menuIsland = centre.position;
		optionIsland = new Vector3 (-58.3f, centre.position.y, 91f);
		gameIsland = new Vector3 (130f, centre.position.y, 128f);

	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		//Continuously maintains a position equivalent to the offset distance
		transform.position = centre.position + offset;
		offset = Quaternion.AngleAxis (1 * speed, Vector3.up) * offset;
		transform.position = centre.position + offset; 
		transform.LookAt (centre.position);
		switch (centrePosition) 
		{
		case 1:
			break;
		case 2:
			CameraMove (menuIsland, optionIsland);
			break;
		case 3:
			CameraMove (menuIsland, gameIsland);
			break;
		case 4:
			CameraMove (centre.position, menuIsland);
			break;
		}
	}

	void CameraMove (Vector3 firstIsland, Vector3 secondIsland)
	{
		tim += Time.deltaTime / lerpSpeed; 
		if (centre.position != secondIsland) 
		{
			centre.position = Vector3.Lerp (firstIsland, secondIsland, tim);
		} 
		else 
		{
			centrePosition = 1;
			tim = 0f;
		}
	}
}
