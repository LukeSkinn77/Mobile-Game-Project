using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesCollision : MonoBehaviour
{
    // This has to be attached to the particle we want to use...
    // The method OnEnable will call the Destruct method to set all particles 
    // Then OnDisable will cancel the function's run time.

    void OnEnable()
    {
        Invoke("Destruct", 1.5f);
    }

    void Destruct()
    {
        Debug.Log("Destroyed Particles");
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}
