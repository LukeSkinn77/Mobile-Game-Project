using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups_Particle_Pooling : MonoBehaviour {

    public static Pickups_Particle_Pooling pickupPool; // create an instance of this and no need to reference
    public int pooledAmount = 1;
    public bool expandPool = true;

    public GameObject pooledPickupParticle;
    public GameObject pooledDamageParticle;
    public GameObject pooledCloudParticle;
    public GameObject pooledCannonball;

    public List<GameObject> pooledPickupParticles;
    public List<GameObject> pooledDamageParticles;
    public List<GameObject> pooledCloudParticles;
    public List<GameObject> pooledCannonballs;

    // Destroy unloads object from the memory and set reference to null so in order to use it again you need to recreate it, via let's say instantiate. 
    //Meanwhile SetActive just hides the object and disables all components on it so if you need you can use it again.



    void Awake()
    {
        // the current script is equals to everything in this script
        pickupPool = this;
    }

    // Use this for initialization
    void Start ()
    {
        SetupParticlePickup();
        SetupDamageParticle();
        SetupCloudParticle();
        SetupCannonball();
    }


    void SetupParticlePickup()
    {
        pooledPickupParticles = new List<GameObject>(); // create new list that will store particles as 'Gameobjects'

        // loop through this condition
        // then instantiate the game object but set the particles at the start to false..
        // then add the particle gameobject to our list of pooledParticles
        for (int x = 0; x < pooledAmount; x++)
        {
            GameObject particle = Instantiate(pooledPickupParticle, transform.position, transform.rotation);
            particle.SetActive(false);
            pooledPickupParticles.Add(particle);
        }
    }

    void SetupDamageParticle()
    {
        pooledDamageParticles = new List<GameObject>();

        for (int y = 0; y < pooledAmount; y++)
        {
            GameObject damageParticle = Instantiate(pooledDamageParticle, transform.position, transform.rotation);
            damageParticle.SetActive(false);
            pooledDamageParticles.Add(damageParticle);
        }

    }

    void SetupCloudParticle()
    {
        pooledCloudParticles = new List<GameObject>();

        for (int y = 0; y < pooledAmount; y++)
        {
            GameObject cloudParticle = Instantiate(pooledCloudParticle, transform.position, transform.rotation);
            cloudParticle.SetActive(false);
            pooledCloudParticles.Add(cloudParticle);
        }

    }

    void SetupCannonball()
    {
        pooledCannonballs = new List<GameObject>();

        for (int y = 0; y < pooledAmount; y++)
        {
            GameObject cannonball = Instantiate(pooledCannonball, transform.position, transform.rotation);
            cannonball.SetActive(false);
            pooledCannonballs.Add(cannonball);
        }

    }

    // ----------------------- CALLABLE METHODS FOR INSTANTIATING PARTICLES ----------------------- //
    // Can call any of these methods in our collectables script or other scripts to instantiate the particles...
    public GameObject GetPickupParticle()
    {

        // loop through the number of particle game objects in the list
        for (int x = 0; x < pooledPickupParticles.Count; x++)
        {
            // if the particle game object is not active in the game
            // then return the particles in the game object list.
            if (!pooledPickupParticles[x].activeInHierarchy)
            {
                return pooledPickupParticles[x];
            }
        }

        // if we are growing the pool of particles.
        // then instantiate the particle game object
        // and add it to the list
        // return the particle afterwards
        // This allows us to expand how many particles we need if instantiated many times
        if (expandPool)
        {
            //GameObject particle = Instantiate(pooledParticle);
            GameObject particle = Instantiate(pooledPickupParticle, transform.position, transform.rotation);
            particle.SetActive(false);
            pooledPickupParticles.Add(particle);
            return particle;
        }

        return null;
    } 

    public GameObject GetDamageParticle()
    {
        for (int y = 0; y < pooledDamageParticles.Count; y++)
        {
            if (!pooledDamageParticles[y].activeInHierarchy)
            {
                return pooledDamageParticles[y];
            }
        }

        if (expandPool)
        {
            GameObject damageParticle = Instantiate(pooledDamageParticle, transform.position, transform.rotation);
            damageParticle.SetActive(false);
            pooledDamageParticles.Add(damageParticle);
            return damageParticle;
        }

        return null;
    }

    public GameObject GetCloudParticle()
    {
        for (int y = 0; y < pooledCloudParticles.Count; y++)
        {
            if (!pooledCloudParticles[y].activeInHierarchy)
            {
                return pooledCloudParticles[y];
            }
        }

        if (expandPool)
        {
            GameObject cloudParticle = Instantiate(pooledCloudParticle, transform.position, transform.rotation);
            cloudParticle.SetActive(false);
            pooledCloudParticles.Add(cloudParticle);
            return cloudParticle;
        }

        return null;
    }

    public GameObject GetCannonball()
    {
        for (int y = 0; y < pooledCannonballs.Count; y++)
        {
            if (!pooledCannonballs[y].activeInHierarchy)
            {
                return pooledCannonballs[y];
            }
        }

        if (expandPool)
        {
            GameObject cannonball = Instantiate(pooledCannonball, transform.position, transform.rotation);
            cannonball.SetActive(false);
            pooledCannonballs.Add(cannonball);
            return cannonball;
        }

        return null;
    }
}
