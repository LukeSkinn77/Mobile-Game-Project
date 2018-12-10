using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMesh : MonoBehaviour {

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        DestroyTheMesh(other);
    }

    void DestroyTheMesh(Collider other)
    {
        if (other.gameObject == player)
        {
            Destroy(this.gameObject); // destroy the mesh.
        }
    }
}
