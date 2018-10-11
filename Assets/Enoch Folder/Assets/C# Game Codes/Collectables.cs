using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour {

    public GameObject player;
    public GameObject hitParticle;

    void OnTriggerEnter(Collider other)
    {
        // Destroy the collectable if the player has contact with it
        if (other.gameObject == player)
        {
            Debug.Log("Cube Absorbed");
            Destroy(this.gameObject);
            Instantiate(hitParticle, transform.position, transform.rotation);
        }
    }
}
