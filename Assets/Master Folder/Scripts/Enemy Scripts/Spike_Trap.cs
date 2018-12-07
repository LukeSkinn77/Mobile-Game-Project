using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike_Trap : MonoBehaviour, IDamagerer {

    Vector3 upPos;
    Vector3 downPos;

    [SerializeField]
    private int damageDealt;
    public int DamageDealt { get { return damageDealt; } }

    void Awake ()
    {
        //Retrieves spike positions
        upPos = transform.position;
        downPos = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        StartCoroutine (DownSpikes(1.5f));
	}
	
    IEnumerator DownSpikes(float timer)
    {
        while (transform.position.y > downPos.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (Time.deltaTime / timer), transform.position.z);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(UpSpikes(0.15f));
    }

    IEnumerator UpSpikes(float timer)
    {
        while (transform.position.y < upPos.y - 0.05f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (Time.deltaTime / timer), transform.position.z);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(DownSpikes(1.5f));
    }
}
