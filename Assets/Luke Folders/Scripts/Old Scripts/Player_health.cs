using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_health : MonoBehaviour {

	public int score = 100;
	public float powerupbar = 100;
	// Use this for initialization
	void Start () 
	{
		//Level_ui_manager.Current.HealthUpdate (playerhealth);
		Level_ui_manager.Current.PowerupUpdate (powerupbar);

		// score will be zero from start
		// score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayerScoreDamage(int damage)
	{
		score -= damage;
		Level_ui_manager.Current.ScoreUpdate (score);
	}

	public void PlayerScoreInc(int incre)
	{
		score = score + incre;
		Level_ui_manager.Current.ScoreUpdate (score);
	}

	public void PlayerPowerUpDrain(float drainage)
	{
		powerupbar -= drainage;
		Level_ui_manager.Current.PowerupUpdate (powerupbar);
	}
}
