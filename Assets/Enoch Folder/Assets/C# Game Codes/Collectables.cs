using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour {

    public GameObject player;
    //public GameObject hitParticle;

    void OnTriggerEnter(Collider other)
    {
        // Destroy the collectable if the player has contact with it
        if (other.gameObject == player)
        {
            //Instantiate(hitParticle, transform.position, transform.rotation);
            Debug.Log("Cube Absorbed");
            Destroy(this.gameObject); // destroy the mesh first.
            

            // initialise a game object and call the method from the object pooler class created
            // To get the particle gameobject.
            GameObject particle = Pickups_Particle_Pooling.pickupPool.GetParticleObject();
            particle.transform.position = transform.position;
            particle.transform.rotation = transform.rotation;
            particle.SetActive(true);
        }
    }
}
