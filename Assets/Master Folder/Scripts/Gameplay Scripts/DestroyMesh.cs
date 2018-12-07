using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMesh : MonoBehaviour {

    public GameObject player;


    // TEST FOR INTERFACE DAMAGE
    //[SerializeField]
    //private int damageDealt;
    //public int DamageDealt { get { return damageDealt; } } 

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
