﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level_Changer : MonoBehaviour {

	public string lvl;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			StartCoroutine(Level_ui_manager.Current.LoadScene(lvl));
		}
	}
}
