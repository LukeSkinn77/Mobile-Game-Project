using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_collider : MonoBehaviour {

	public Color damagecolour;
	public Color normalcolour;

	Renderer rend;
//	Rigidbody rb;

	// Use this for initialization
	void Awake () 
	{
		rend = GetComponent<Renderer> ();
		//rb = GetComponent<Rigidbody> ();
		normalcolour = rend.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		
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
