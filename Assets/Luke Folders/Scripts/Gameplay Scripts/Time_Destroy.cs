using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time_Destroy : MonoBehaviour {

	public float ttd = 1.0f;

	void OnEnable()
	{
		Invoke ("DisableAndDestroy", ttd);
	}

	void DisableAndDestroy()
	{
		gameObject.SetActive (false);
	}

	void OnDisable()
	{
		CancelInvoke ();
	}
}
