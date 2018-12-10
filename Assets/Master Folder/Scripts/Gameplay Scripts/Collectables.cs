using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour {

    private GameObject player;
    private GameObject[] collectableObjects;
    private Vector3 thisPosition;
    private Quaternion thisRotation;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        ScoringCollectable(other);
    }

    void ScoringCollectable(Collider other)
    {
        // Assign the collectables position and rotation as Vector3 and Qauternion respectively..
        thisPosition = transform.position;
        thisRotation = transform.rotation;

        // A BETTER WAY TO GET COLLECTABLE INFORMATION 'LOOPING'!!!!
        if (other.gameObject == player)
        {
            foreach (GameObject collect in collectableObjects)
            {
                Debug.Log("Cube Absorbed");
                GameObject particle = Pickups_Particle_Pooling.pickupPool.GetPickupParticle();
                if (particle == null) return;
                particle.transform.position = thisPosition;
                particle.transform.rotation = thisRotation;
                particle.SetActive(true);
            }
        }

    }
}
