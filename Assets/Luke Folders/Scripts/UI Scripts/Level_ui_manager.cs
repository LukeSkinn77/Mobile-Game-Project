using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level_ui_manager : MonoBehaviour {

	GameObject[] menuobjects;
	GameObject[] loadobjects;


	// Use this for initialization
	void Start () 
	{
		menuobjects = GameObject.FindGameObjectsWithTag ("ShowOnMenu");
		loadobjects = GameObject.FindGameObjectsWithTag ("LoadScreen");
		MenuObjectsOff ();
	}

	public void MenuObjectsOn()
	{
		foreach (GameObject loadobject in loadobjects) 
		{
			loadobject.SetActive (false);
		}
		foreach (GameObject menuobject in menuobjects) 
		{
			menuobject.SetActive (true);
		}
		Time.timeScale = 0.0f;
	}

	public void MenuObjectsOff()
	{
		foreach (GameObject loadobject in loadobjects) 
		{
			loadobject.SetActive (false);
		}
		foreach (GameObject menuobject in menuobjects) 
		{
			menuobject.SetActive (false);
		}
		Time.timeScale = 1.0f;
	}

	public void MenuLoad()
	{
		StartCoroutine(LoadScene ("lvl_mainmenu_test"));
	}

	IEnumerator LoadScene(string lvl)
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
}
