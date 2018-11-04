using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus_Player_Collision : MonoBehaviour {

	public Color damagecolour;
	public Color normalcolour;

	Renderer rend;

	// Use this for initialization
	void Awake () 
	{
		rend = GetComponent<Renderer> ();
		normalcolour = rend.material.color;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Enemy_Hitzone") 
		{

			StartCoroutine (DamageColourTrigger());
		}
	}

	IEnumerator DamageColourTrigger()
	{
		rend.material.color = damagecolour;
		yield return new WaitForSeconds (0.5f);
		rend.material.color = normalcolour;
		yield return new WaitForSeconds (0.5f);
	}
}
