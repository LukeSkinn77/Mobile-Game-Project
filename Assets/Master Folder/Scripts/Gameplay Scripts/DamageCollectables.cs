using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollectables : MonoBehaviour
{

    public GameObject player;
    public GameObject[] damageObjects;
    private Vector3 thisPosition;
    private Quaternion thisRotation;

    // Use this for initialization
    void Start()
    {
        damageObjects = GameObject.FindGameObjectsWithTag("Damage");
    }

    void OnTriggerEnter(Collider other)
    {
        LosingScoreCollectable(other);
    }

    void LosingScoreCollectable(Collider other)
    {
        thisPosition = transform.position;
        thisRotation = transform.rotation;

        if (other.gameObject == player)
        {
            foreach (GameObject damage in damageObjects)
            {
                Debug.Log("Cube Absorbed");
                GameObject damageParticle = Pickups_Particle_Pooling.pickupPool.GetDamageParticle();
                if (damageParticle == null) return;
                damageParticle.transform.position = thisPosition;
                damageParticle.transform.rotation = thisRotation;
                damageParticle.SetActive(true);
            }
        }

    }
}

