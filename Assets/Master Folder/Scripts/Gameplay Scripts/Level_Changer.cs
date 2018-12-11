using System.Collections;
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
            GetComponent<AudioSource>().Play();
            Game_Manager.Instance.scoreTotal += GameObject.FindGameObjectWithTag("Player").GetComponent<Main_Player_Score_Manager>().score;
            Level_ui_manager.Current.VictoryScreenOn();
		}
	}
}
