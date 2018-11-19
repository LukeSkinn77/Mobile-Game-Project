using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Player_Score_Manager : MonoBehaviour {
	
	public int score = 100;
	public float powerupBar = 100;

	// Use this for initialization
	void Start () 
	{
		//Updates powerub bar amount
		Level_ui_manager.Current.PowerupUpdate (powerupBar);
		score = 0;
	}

	public void PlayerScoreDamage(int damage)
	{
		//Reduces score amount and ui amount
		score -= damage;
        if (score < 0)
        {
            score = 0;
            Debug.Log("You died");
            // add the melon die effect here...
            // return to level...
        }
        Level_ui_manager.Current.ScoreUpdate (score);
	}

	public void PlayerScoreInc(int incre)
	{
		//Increases score amount and ui amount
		score = score + incre;
		Level_ui_manager.Current.ScoreUpdate (score);
	}

	public void PlayerPowerUpDrain(float drainage)
	{
		//Reduces powerup amount and bar fill
		powerupBar -= drainage;
		Level_ui_manager.Current.PowerupUpdate (powerupBar);
	}
}
