using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesCollision : MonoBehaviour
{
    //public GameObject player;
    //public GameObject hitParticle;

     // This has to be attached to the particle we want to use...

    private void Awake()
    {
        Invoke("Destruct", 2.0f);
    }

    void Destruct()
    {
        Debug.Log("Destroyed Particles");
        gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
