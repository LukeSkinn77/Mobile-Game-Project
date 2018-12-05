using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public void Interaction ()
    {
        Game_Manager.Instance.savedPlayerLocation = this.transform.position;
        Game_Manager.Instance.SavePlayer();
        GameObject particle = Pickups_Particle_Pooling.pickupPool.GetPickupParticle();
        if (particle == null) return;
        particle.transform.position = this.transform.position;
        particle.transform.rotation = this.transform.rotation;
        particle.SetActive(true);
        gameObject.SetActive(false);
    }
}
