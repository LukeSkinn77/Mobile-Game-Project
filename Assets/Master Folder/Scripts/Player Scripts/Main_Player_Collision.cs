using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Player_Collision : MonoBehaviour {
	
	Main_Player_Control pl;
	Main_Player_Score_Manager ph;

	public Color damageColour;
	public Color normalColour;

    bool isDamaged = false;

	Renderer rend;

	public GameObject model;

	void Awake()
	{

	}

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
            //REMOVE AFTER BETA SHITE
            //			Debug.Log("Cube Absorbed");
            GameObject particle = Pickups_Particle_Pooling.pickupPool.GetPickupParticle();
            if (particle == null) return;
            particle.transform.position = this.transform.position;
            particle.transform.rotation = this.transform.rotation;
            particle.SetActive(true);


            other.gameObject.SetActive (false);
			ScoreCheck ();
		}

        if (other.GetComponent<IDamagerer>() != null) 
		{
            if (!isDamaged)
            {
                //Reduces score, adds upward force, flashes material colour to red
                //And checks the score
                ph.PlayerScoreDamage(other.GetComponent<IDamagerer>().DamageDealt);
                pl.rb.AddForce(0.0f, 0.5f, 0.0f, ForceMode.Impulse);
                StartCoroutine(DamageColourTrigger());
                ScoreCheck();
                isDamaged = true;
            }
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.GetComponent<IDamagerer>() != null)
        {
            if (!isDamaged)
            { 
                //Reduces score, adds upward force, flashes material colour to red
                //And checks the score
                ph.PlayerScoreDamage(other.GetComponent<IDamagerer>().DamageDealt);
                pl.rb.AddForce(0.0f, 0.5f, 0.0f, ForceMode.Impulse);
                StartCoroutine(DamageColourTrigger());
                ScoreCheck();
                isDamaged = true;
            }
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
        isDamaged = false;
    }
}
