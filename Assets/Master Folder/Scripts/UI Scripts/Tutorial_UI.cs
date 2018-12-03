using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_UI : MonoBehaviour {

    Text textTutorial;
    public int tutorialID;

	void Start ()
    {
        //Retrieves Tutorial Text
        textTutorial = GameObject.Find("Tutorial Text").GetComponent<Text>();
	}
	
	void OnTriggerEnter (Collider other)
    {
		if (other.tag == "Player")
        {
            switch (tutorialID)
            {
                //Set the text depending on the tutorial ID
                case 0:
                    textTutorial.text = "Swipe up and down to move forwards and backwards";
                    break;
                case 1:
                    textTutorial.text = "Tap to jump";
                    break;
                case 2:
                    textTutorial.text = "Swipe left and right to rotate the camera";
                    break;
                case 3:
                    textTutorial.text = "Tap after jumping to use a power up when collected";
                    break;
                case 4:
                    textTutorial.text = "Use checkpoints to restart after falling down";
                    break;
                case 5:
                    textTutorial.text = "Avoid Enemies or else lose points";
                    break;
                case 6:
                    textTutorial.text = "Touch the end point to get to the next level";
                    break;
                case 7:
                    textTutorial.text = "Collect points to level up and increase speed";
                    break;
            }
            textTutorial.gameObject.SetActive(true);
            StartCoroutine(TextFadeIn(0.75f));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(TextFadeOut(0.75f));
        }
    }

    IEnumerator TextFadeIn(float fadeTimer)
    {
        //Fade the text in by increasing the alpha
        textTutorial.color = new Color(textTutorial.color.r, textTutorial.color.g, textTutorial.color.b, 0);
        while (textTutorial.color.a < 1f)
        {
            textTutorial.color = new Color(textTutorial.color.r, textTutorial.color.g, textTutorial.color.b, textTutorial.color.a + (Time.deltaTime / fadeTimer));
            yield return null;
        }
    }

    IEnumerator TextFadeOut(float fadeTimer)
    {
        //Fade the text out by decreasing the alpha
        textTutorial.color = new Color(textTutorial.color.r, textTutorial.color.g, textTutorial.color.b, 1);
        while (textTutorial.color.a > 0f)
        {
            textTutorial.color = new Color(textTutorial.color.r, textTutorial.color.g, textTutorial.color.b, textTutorial.color.a - (Time.deltaTime / fadeTimer));
            yield return null;
        }
        //Disable and blank the text
        textTutorial.text = "";
        textTutorial.gameObject.SetActive(false);
    }
}
