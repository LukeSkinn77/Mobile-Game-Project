using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level_ui_manager : MonoBehaviour {

	public static Level_ui_manager Current;

	GameObject[] menuObjects;
	GameObject[] loadObjects;

	Slider powerupSlider;
	Slider scoreSlider;

	Text powerupTxt;
	Text healthTxt;
	Text levelTxt;

	//float scoreSlidervalue = 0;

	// Use this for initialization
	void Awake () 
	{
		Current = this;
		Initiat ();
		MenuObjectsOff ();
	}
		
	void Initiat()
	{
		//Fills lists with gameobjects withs said tags, finds gameobject references
		//And updates the score
		menuObjects = GameObject.FindGameObjectsWithTag ("ShowOnMenu");
		loadObjects = GameObject.FindGameObjectsWithTag ("LoadScreen");
		powerupSlider = GameObject.Find ("Powerup Slider").GetComponent<Slider>();
		scoreSlider = GameObject.Find ("Health Slider").GetComponent<Slider>();
		powerupTxt = GameObject.Find ("Double Jump Text").GetComponent<Text> ();
		healthTxt = GameObject.Find ("Health Text").GetComponent<Text> ();
		levelTxt = GameObject.Find ("Player Level Text").GetComponent<Text> ();
		powerupTxt.gameObject.SetActive (false);
		powerupSlider.gameObject.SetActive (false);
		ScoreUpdate (0);
	}

	public void MenuObjectsOn()
	{
		//Turns on pause gameobjects in list, while disabling any other list
		foreach (GameObject loadobject in loadObjects) 
		{
			loadobject.SetActive (false);
		}
		foreach (GameObject menuobject in menuObjects) 
		{
			menuobject.SetActive (true);
		}
		Time.timeScale = 0.0f;
	}

	public void MenuObjectsOff()
	{
		//Turns off pause gameobjects in list, while disabling any other list
		foreach (GameObject loadobject in loadObjects) 
		{
			loadobject.SetActive (false);
		}
		foreach (GameObject menuobject in menuObjects) 
		{
			menuobject.SetActive (false);
		}
		Time.timeScale = 1.0f;
	}

	public void MenuLoad()
	{
		//Starts load procedure
		StartCoroutine(LoadScene ("lvl_mainmenu"));
	}

	public IEnumerator LoadScene(string lvl)
	{
		//Turns on loading gameobjects in list
		foreach (GameObject loadobject in loadObjects) 
		{
			loadobject.SetActive (true);
		}
		//Loads scene asynchronously
		AsyncOperation asyn = SceneManager.LoadSceneAsync (lvl);

		while (!asyn.isDone) 
		{
			yield return null;
		}
	}

	public void ScoreUpdate(float score)
	{
		//Updates score ui
		healthTxt.text = "Score: " + score;
		scoreSlider.value = score;
	}

	public void PowerupUpdate(float powerupbar)
	{
		//Updates powerup slider
		powerupSlider.value = powerupbar;
	}

	public void PowerupTextChanger(int state)
	{
		//Turns on and off powerup slider and/or text depending
		//On player state
		switch (state) 
		{
		case 3:
			powerupTxt.gameObject.SetActive (true);
			powerupTxt.text = "Double Jump";
			powerupSlider.gameObject.SetActive (true);
			break;
		case 4:
			powerupTxt.gameObject.SetActive (true);
			powerupTxt.text = "Glide Jump";
			powerupSlider.gameObject.SetActive (true);
			break;
		case 5:
			powerupTxt.gameObject.SetActive (true);
			powerupTxt.text = "Slow Down";
			powerupSlider.gameObject.SetActive (true);
			break;
		default:
			powerupTxt.gameObject.SetActive (false);
			powerupSlider.gameObject.SetActive (false);
			break;
		}
	}

	public void ScoreSliderValue(int maxvalueinp, int score, int playerstate)
	{
		//Sets the score slider to the score
		scoreSlider.value = score;

		//Increases slider max value
		scoreSlider.maxValue = maxvalueinp;

		//Show player level
		levelTxt.text = "" + playerstate;
	}
}
