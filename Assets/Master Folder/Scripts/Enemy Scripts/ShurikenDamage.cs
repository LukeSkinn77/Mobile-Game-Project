using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenDamage : MonoBehaviour {

    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("You've been hit");
        }
    }
}
