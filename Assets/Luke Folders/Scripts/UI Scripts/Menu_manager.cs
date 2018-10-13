using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_manager : MonoBehaviour {

	GameObject[] menuobjects;
	GameObject[] lvlslctobjects;
	GameObject[] loadobjects;

	// Use this for initialization
	void Start () 
	{
		menuobjects = GameObject.FindGameObjectsWithTag ("ShowOnMenu");
		loadobjects = GameObject.FindGameObjectsWithTag ("LoadScreen");
		MenuObjectsOn ();
	}
	
	// Update is called once per frame
	void Update () {
		
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
		Time.timeScale = 1.0f;
	}

	public void StartGame()
	{
		//SceneManager.LoadScene("lvl_test2");
		StartCoroutine(LoadScene("lvl_test2"));
	}

	public void BonusStage()
	{
		//SceneManager.LoadScene("lvl_test1");
		StartCoroutine(LoadScene("lvl_test3"));
	}

	public void ExitGame()
	{
		Application.Quit();
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
