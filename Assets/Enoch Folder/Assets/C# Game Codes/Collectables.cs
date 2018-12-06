using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour {

    public GameObject player;
    public GameObject[] collectableObjects;
    private Vector3 thisPosition;
    private Quaternion thisRotation;

    void Start()
    {
        //collectableObjects =  GameObject.FindGameObjectsWithTag("Pickup");

    }

    void OnTriggerEnter(Collider other)
    {
        ScoringCollectable(other);
    }

    void ScoringCollectable(Collider other)
    {
        // Destroy the collectable if the player has contact with it
        // initialise a game object and call the method from the object pooler class created to get the particle gameobject.
        // Check if the particle is null to ensure no errors, else return back.
        // When the particle is in contact with the player
        // We want to instantiate the particle from the position and the rotation of where it hit the player from
        // So whichever angle it hits the player thats where the particle will instantiate from

        //if (other.gameObject == player)
        //{
        //    Debug.Log("Cube Absorbed");
        //    GameObject particle = Pickups_Particle_Pooling.pickupPool.GetPickupParticle();
        //    if (particle == null) return;
        //    particle.transform.position = thisPosition;
        //    particle.transform.rotation = thisRotation;
        //    particle.SetActive(true);
        //    Destroy(this.gameObject); // destroy the mesh.
        //}

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
