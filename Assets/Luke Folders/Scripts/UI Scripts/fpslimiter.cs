using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fpslimiter : MonoBehaviour {

	public int fpslimit = 30;

	void Start () 
	{
		fpslimit = Game_Manager.Instance.frameRate;
		//Sets the framerate to the variable
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = fpslimit;
	}
	
	void Update () 
	{
		//Keeps the framerate to the variable
		if (Application.targetFrameRate != fpslimit) 
		{
			Application.targetFrameRate = fpslimit;
		}
	}
}
