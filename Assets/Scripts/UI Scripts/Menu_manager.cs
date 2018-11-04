using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_manager : MonoBehaviour {

	public static Menu_manager current;

	GameObject[] menuobjects;
	GameObject[] lvlslctobjects;
	GameObject[] loadobjects;
	GameObject[] optionobjects;

	public Slider volumesl;

	public Text framerateText;

	Menu_Camera_Control cn;

	[SerializeField]
	Game_Manager gm;

	void Awake()
	{
		current = this;
		gm = GameObject.Find ("Game Manager").GetComponent<Game_Manager> ();
		gm.LoadOptions ();

		menuobjects = GameObject.FindGameObjectsWithTag ("ShowOnMenu");
		loadobjects = GameObject.FindGameObjectsWithTag ("LoadScreen");
		optionobjects = GameObject.FindGameObjectsWithTag ("OptionScreen");
		lvlslctobjects = GameObject.FindGameObjectsWithTag ("LevelScreen");

		cn = Camera.main.GetComponent<Menu_Camera_Control> ();

		FrameRateEnabler ();
		MenuObjectsOn ();
		volumesl.value = AudioListener.volume;
	}

	//Disables all UI objects but the one selected
	public void MenuObjectsOn()
	{
		foreach (GameObject levelobject in lvlslctobjects) 
		{
			levelobject.SetActive (false);
		}
		foreach (GameObject optionobject in optionobjects) 
		{
			optionobject.SetActive (false);
		}
		foreach (GameObject loadobject in loadobjects) 
		{
			loadobject.SetActive (false);
		}
		foreach (GameObject menuobject in menuobjects) 
		{
			menuobject.SetActive (true);
		}
		Time.timeScale = 1.0f;
		cn.centrePosition = 4;
	}

	//Disables all UI objects but the one selected
	public void OptionObjectsOn()
	{
		foreach (GameObject levelobject in lvlslctobjects) 
		{
			levelobject.SetActive (false);
		}
		foreach (GameObject loadobject in loadobjects) 
		{
			loadobject.SetActive (false);
		}
		foreach (GameObject menuobject in menuobjects) 
		{
			menuobject.SetActive (false);
		}
		foreach (GameObject optionobject in optionobjects) 
		{
			optionobject.SetActive (true);
		}
		Time.timeScale = 1.0f;
		cn.centrePosition = 2;
	}

	//Disables all UI objects but the one selected
	public void LevelObjectsOn()
	{
		foreach (GameObject optionobject in optionobjects) 
		{
			optionobject.SetActive (false);
		}
		foreach (GameObject loadobject in loadobjects) 
		{
			loadobject.SetActive (false);
		}
		foreach (GameObject menuobject in menuobjects) 
		{
			menuobject.SetActive (false);
		}
		foreach (GameObject levelobject in lvlslctobjects) 
		{
			levelobject.SetActive (true);
		}
		Time.timeScale = 1.0f;
		cn.centrePosition = 3;
	}

	public void StartGame()
	{
		//Starts asynchronous loading of scene
		StartCoroutine(LoadScene("lvl_test3"));
	}

	public void BonusStage()
	{
		//Starts asynchronous loading of scene
		StartCoroutine(LoadScene("lvl_test2"));
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public IEnumerator LoadScene(string lvl)
	{
		//Shows loading screen
		foreach (GameObject loadobject in loadobjects) 
		{
			loadobject.SetActive (true);
		}

		//Starts loading scene
		AsyncOperation asyn = SceneManager.LoadSceneAsync (lvl);

		while (!asyn.isDone) 
		{
			yield return null;
		}
	}

	public void AdjustVolume(Slider slider)
	{
		//Takes slider value and applies it to volume
		AudioListener.volume = slider.value;
	}

	public void OptionSaveTrigger()
	{
		//Saves option data
		gm.SaveOptions ();
	}

	public void Continue()
	{
		//Loads player level data
		gm.LoadPlayer ();
	}

	public void FrameRateSwitcher()
	{
		//Switches the framerate to and from a higher
		//And lower framerate
		if (gm.frameRate == 30) 
		{
			gm.frameRate = 60;
			framerateText.text = "60";
		}
		else if (gm.frameRate == 60) 
		{
			gm.frameRate = 30;
			framerateText.text = "30";
		}
	}

	public void FrameRateEnabler()
	{
		//Switches text
		if (gm.frameRate == 30) 
		{
			framerateText.text = "30";
		}
		else if (gm.frameRate == 60) 
		{
			framerateText.text = "60";
		}
	}
}
