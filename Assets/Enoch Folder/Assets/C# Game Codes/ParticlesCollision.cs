using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesCollision : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Hit!");
    }
}
