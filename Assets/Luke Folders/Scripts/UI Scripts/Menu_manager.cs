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
		//SceneManager.LoadScene("lvl_test2");
		StartCoroutine(LoadScene("lvl_test3"));
	}

	public void BonusStage()
	{
		//SceneManager.LoadScene("lvl_test1");
		StartCoroutine(LoadScene("lvl_test2"));
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public IEnumerator LoadScene(string lvl)
	{
		foreach (GameObject loadobject in loadobjects) 
		{
			loadobject.SetActive (true);
		}

		AsyncOperation asyn = SceneManager.LoadSceneAsync (lvl);

		while (!asyn.isDone) 
		{
			yield return null;
		}
	}

	public void AdjustVolume(Slider slider)
	{
		AudioListener.volume = slider.value;
	}

	public void OptionSaveTrigger()
	{
		gm.SaveOptions ();
	}

	public void Continue()
	{
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
