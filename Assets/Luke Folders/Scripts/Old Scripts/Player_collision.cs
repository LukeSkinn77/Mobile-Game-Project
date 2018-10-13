using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_collision : MonoBehaviour {

	player_control_mark2 pl;
	Player_health ph;

	public Color damagecolour;
	public Color normalcolour;

	Renderer rend;

	public GameObject model;

	void Start()
	{
		pl = GetComponent<player_control_mark2> ();
		ph = GetComponent<Player_health> ();
		rend = model.GetComponent<Renderer> ();
		normalcolour = rend.material.color;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "PowerUp") 
		{
			int powerstate = other.GetComponent<Power_up_base> ().Powerupstate;
			ph.powerupbar = 100;
			pl.playerpowerupstate = powerstate;
			Level_ui_manager.Current.PowerupTextChanger (powerstate);
			Level_ui_manager.Current.PowerupUpdate (100);
			other.gameObject.SetActive (false);
		}

		if (other.tag == "Collectible") 
		{
			ph.PlayerScoreInc (100);
			other.gameObject.SetActive (false);
			ScoreCheck ();
		}

		if (other.tag == "Enemy") 
		{
			ph.PlayerScoreDamage (20);
			pl.rb.AddForce (0.0f, 0.5f, 0.0f, ForceMode.Impulse);
			StartCoroutine (DamageColourTrigger ());
			ScoreCheck();
		}
	}

	void ScoreCheck()
	{
		if (ph.score < 200) 
		{
			if (pl.playerpowerupstate != 0) 
			{
				pl.LevelOneModifier ();
				pl.playerpowerupstate = 0;
				pl.playerpowerupstate_temp = 0;
				normalcolour = rend.material.color;
			}
		} 
		else if ((ph.score >= 200) && (ph.score < 300)) 
		{
			if (pl.playerpowerupstate != 1) 
			{
				pl.LevelTwoModifier ();
				pl.playerpowerupstate = 1;
				pl.playerpowerupstate_temp = 1;
				normalcolour = rend.material.color;
			}
		} 
		else if (ph.score >= 300)
		{
			if (pl.playerpowerupstate != 2) 
			{
				pl.LevelThreeModifier ();
				pl.playerpowerupstate = 2;
				pl.playerpowerupstate_temp = 2;
				normalcolour = rend.material.color;
			}
		}
	}

	IEnumerator DamageColourTrigger()
	{
		for (int i = 0; i < 10; i++) 
		{
			rend.material.color = damagecolour;
			yield return new WaitForSeconds (0.05f);
			rend.material.color = normalcolour;
			yield return new WaitForSeconds (0.05f);
		}
	}
}
