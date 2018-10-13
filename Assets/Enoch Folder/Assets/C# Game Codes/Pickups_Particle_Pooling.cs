using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups_Particle_Pooling : MonoBehaviour {

    public static Pickups_Particle_Pooling pickupPool; // create an instance of this and no need to reference
    public GameObject pooledParticle; // 
    public int pooledAmount = 2;
    public bool expandPool = true;
    List<GameObject> pooledParticles;


    void Awake()
    {
        // the current script is equals to everything in this script
        pickupPool = this;
    }

    // Use this for initialization
    void Start ()
    {
        pooledParticles = new List<GameObject>(); // create new list that will store particles as 'Gameobjects'

        // loop through this condition
        // then instantiate the game object
        // then add the particle gameobject to our list of pooledParticles
        for (int x = 0; x < pooledAmount; x++)
        {
            //GameObject particle = Instantiate(pooledParticle);
            GameObject particle = Instantiate(pooledParticle, transform.position, transform.rotation);
            particle.SetActive(false);
            pooledParticles.Add(particle);
        }
	}

    // We call this method in our collectables script to instantiate the particle...
    public GameObject GetParticleObject()
    {
        // loop through the number of particle game objects in the list
        for (int x = 0; x < pooledParticles.Count; x++)
        {
            // if the particle game object is not active in the game
            // then return the particles in the game object list.
            if (!pooledParticles[x].activeInHierarchy)
            {
                return pooledParticles[x];
            }
        }

        // if we are growing the pool of particles.
        // then instantiate the particle game object
        // and add it to the list
        // return the particle afterwards
        if (expandPool)
        {
            //GameObject particle = Instantiate(pooledParticle);
            GameObject particle = Instantiate(pooledParticle, transform.position, transform.rotation);
            pooledParticles.Add(particle);
            return particle;
        }

        return null;
    }
}
