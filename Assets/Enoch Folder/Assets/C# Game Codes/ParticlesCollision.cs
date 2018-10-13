using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesCollision : MonoBehaviour
{
    // This has to be attached to the particle we want to use...

    void OnEnable()
    {
        Invoke("Destruct", 2.0f);
    }

    void Destruct()
    {
        Debug.Log("Destroyed Particles");
        gameObject.SetActive(false);
        //Destroy(this.gameObject);
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}
