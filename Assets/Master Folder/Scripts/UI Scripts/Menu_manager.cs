using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_manager : MonoBehaviour {

	public static Menu_manager Current;

	GameObject[] menuobjects;
	GameObject[] startobjects;
	GameObject[] lvlslctobjects;
	GameObject[] loadobjects;
	GameObject[] optionobjects;
	GameObject[] creditobjects;

	public Slider volumesl;

	public Text framerateText;

	public string levelOne, levelTwo, levelThree;

	Menu_Camera_Control cn;

    public int skinSlct = 0;
    public Text skinTxt;

    public Text scoreText;

    [SerializeField]
	Game_Manager gm;

    AudioSource aud;

    void Awake()
	{
		Current = this;
		gm = GameObject.Find ("Game Manager").GetComponent<Game_Manager> ();
		gm.LoadOptions ();

        aud = GetComponent<AudioSource>();

        menuobjects = GameObject.FindGameObjectsWithTag ("ShowOnMenu");
		loadobjects = GameObject.FindGameObjectsWithTag ("LoadScreen");
		optionobjects = GameObject.FindGameObjectsWithTag ("OptionScreen");
		lvlslctobjects = GameObject.FindGameObjectsWithTag ("LevelScreen");
		creditobjects = GameObject.FindGameObjectsWithTag ("CreditScreen");
		startobjects = GameObject.FindGameObjectsWithTag ("StartScreen");

		cn = Camera.main.GetComponent<Menu_Camera_Control> ();

		FrameRateEnabler ();
		MenuObjectsOn ();
		volumesl.value = AudioListener.volume;
    }

    private void Start()
    {
        gm.savedPlayerLocation = Vector3.zero;
        gm.scoreTotal = 0;
        scoreText.text = "High Score: " + gm.highScore;
        skinSlct = gm.loadedSkin;
        skinTxt.text = gm.playerSkins[skinSlct].skinName;
    }

    //Disables all UI objects but the one selected
    public void MenuObjectsOn()
	{
        aud.Play();
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
		foreach (GameObject creditobject in creditobjects) 
		{
			creditobject.SetActive (false);
		}
		foreach (GameObject startobject in startobjects) 
		{
			startobject.SetActive (false);
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
        aud.Play();

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
		foreach (GameObject creditobject in creditobjects) 
		{
			creditobject.SetActive (false);
		}
		foreach (GameObject startobject in startobjects) 
		{
			startobject.SetActive (false);
		}
		foreach (GameObject optionobject in optionobjects) 
		{
			optionobject.SetActive (true);
		}
		Time.timeScale = 1.0f;
		cn.centrePosition = 2;
	}

	//Disables all UI objects but the one selected
	public void StartObjectsOn()
	{
        aud.Play();

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
		foreach (GameObject creditobject in creditobjects) 
		{
			creditobject.SetActive (false);
		}
		foreach (GameObject levelobject in lvlslctobjects) 
		{
			levelobject.SetActive (false);
		}
		foreach (GameObject startobject in startobjects) 
		{
			startobject.SetActive (true);
		}
		gm.savedPlayerLocation = Vector3.zero;
		Time.timeScale = 1.0f;
		cn.centrePosition = 3;
	}


	//Disables all UI objects but the one selected
	public void LevelObjectsOn()
	{
        aud.Play();

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
		foreach (GameObject creditobject in creditobjects) 
		{
			creditobject.SetActive (false);
		}
		foreach (GameObject startobject in startobjects) 
		{
			startobject.SetActive (false);
		}
		foreach (GameObject levelobject in lvlslctobjects) 
		{
			levelobject.SetActive (true);
		}
		Time.timeScale = 1.0f;
		cn.centrePosition = 3;
	}

	//Disables all UI objects but the one selected
	public void CreditObjectsOn()
	{
        aud.Play();

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
			levelobject.SetActive (false);
		}
		foreach (GameObject startobject in startobjects) 
		{
			startobject.SetActive (false);
		}
		foreach (GameObject creditobject in creditobjects) 
		{
			creditobject.SetActive (true);
		}
		Time.timeScale = 1.0f;
		cn.centrePosition = 5;
	}

	public void StartLevelOne()
	{
        aud.Play();

        //Starts asynchronous loading of scene
        StartCoroutine(LoadScene(levelOne));
	}

	public void StartLevelTwo()
	{
        aud.Play();

        //Starts asynchronous loading of scene
        StartCoroutine(LoadScene(levelTwo));
	}

	public void StartLevelThree()
	{
        aud.Play();

        //Starts asynchronous loading of scene
        StartCoroutine(LoadScene(levelThree));
	}

	public void BonusStage()
	{
        aud.Play();

        //Starts asynchronous loading of scene
        StartCoroutine(LoadScene("lvl_tutorial"));
	}

	public void ExitGame()
	{
        aud.Play();

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

    public void PlayerSkinSelect()
    {
        skinSlct += 1;
        if (skinSlct > gm.playerSkins.Count - 1)
        {
            skinSlct = 0;
        }
        skinTxt.text = gm.playerSkins[skinSlct].skinName;
        gm.currentSkin = gm.playerSkins[skinSlct];
    }
}
