using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Player_Score_Manager : MonoBehaviour {
	
	public int score = 100;
	public float powerupBar = 100;
    public GameObject melon;
    bool isDead = false;

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
        if ((score < 0) && (!isDead))
        {
            score = 0;
            Game_Manager.Instance.savedPlayerLocation = Vector3.zero;
            Level_ui_manager.Current.GameOverScreenOn();
            Instantiate(melon, transform.position, transform.rotation);
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            gameObject.GetComponent<Main_Player_Collision>().enabled = false;
            gameObject.GetComponent<Main_Player_Control>().enabled = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            this.enabled = false;
            isDead = true;
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
