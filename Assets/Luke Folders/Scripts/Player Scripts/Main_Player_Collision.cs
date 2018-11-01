using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Player_Collision : MonoBehaviour {
	
	Main_Player_Control pl;
	Main_Player_Score_Manager ph;

	public Color damageColour;
	public Color normalColour;

	Renderer rend;

	public GameObject model;

	void Start()
	{
		//Retieves components and sets color
		pl = GetComponent<Main_Player_Control> ();
		ph = GetComponent<Main_Player_Score_Manager> ();
		rend = model.GetComponent<Renderer> ();
		normalColour = rend.material.color;
		Level_ui_manager.Current.ScoreSliderValue (200, ph.score, pl.playerpowerupstate_temp);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "PowerUp") 
		{
			//Sets power state to the powerup's state, sets the powerup bar to full
			//Sets the powerup ui to the correct state and disables the powerup
			int powerState = other.GetComponent<Power_up_base> ().Powerupstate;
			ph.powerupBar = 100;
			pl.playerpowerupstate = powerState;
			Level_ui_manager.Current.PowerupTextChanger (powerState);
			Level_ui_manager.Current.PowerupUpdate (100);
			other.gameObject.SetActive (false);
		}

		if (other.tag == "Collectable") 
		{
			//Increases player score, disables collectable and checks score
			ph.PlayerScoreInc (50);
			other.gameObject.SetActive (false);
			ScoreCheck ();
		}

		if (other.tag == "Enemy") 
		{
			//Reduces score, adds upward force, flashes material colour to red
			//And checks the score
			ph.PlayerScoreDamage (20);
			pl.rb.AddForce (0.0f, 0.5f, 0.0f, ForceMode.Impulse);
			StartCoroutine (DamageColourTrigger ());
			ScoreCheck();
		}
	}

	void ScoreCheck()
	{
		//Changes the player state and material based on their score amount
		if (ph.score < 200) 
		{
			if (pl.playerpowerupstate != 0) 
			{
				pl.LevelOneModifier ();
				if (pl.playerpowerupstate < 3)
				{
					pl.playerpowerupstate = 0;
				}				
				pl.playerpowerupstate_temp = 0;
				Level_ui_manager.Current.ScoreSliderValue (200, ph.score, pl.playerpowerupstate_temp);
				normalColour = rend.material.color;
			}
		} 
		else if ((ph.score >= 200) && (ph.score < 300)) 
		{
			if (pl.playerpowerupstate != 1) 
			{
				pl.LevelTwoModifier ();
				if (pl.playerpowerupstate < 3)
				{
					pl.playerpowerupstate = 1;
				}
				pl.playerpowerupstate_temp = 1;
				Level_ui_manager.Current.ScoreSliderValue (300, ph.score, pl.playerpowerupstate_temp);
				normalColour = rend.material.color;
			}
		} 
		else if (ph.score >= 300)
		{
			if (pl.playerpowerupstate != 2) 
			{
				pl.LevelThreeModifier ();
				if (pl.playerpowerupstate < 3)
				{
					pl.playerpowerupstate = 2;
				}				
				pl.playerpowerupstate_temp = 2;
				Level_ui_manager.Current.ScoreSliderValue (300, ph.score, pl.playerpowerupstate_temp);
				normalColour = rend.material.color;
			}
		}
	}

	IEnumerator DamageColourTrigger()
	{
		//Rapidly changes the colour to red and back
		for (int i = 0; i < 10; i++) 
		{
			rend.material.color = damageColour;
			yield return new WaitForSeconds (0.05f);
			rend.material.color = normalColour;
			yield return new WaitForSeconds (0.05f);
		}
	}
}
