using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public void Interaction ()
    {
        //Sets game manager variable to the checkpoint's position then starts save function
        Game_Manager.Instance.savedPlayerLocation = this.transform.position;
        Game_Manager.Instance.SavePlayer();
        //Activates particle and disables object
        GameObject particle = Pickups_Particle_Pooling.pickupPool.GetPickupParticle();
        if (particle == null) return;
        particle.transform.position = this.transform.position;
        particle.transform.rotation = this.transform.rotation;
        particle.SetActive(true);
        gameObject.SetActive(false);
    }
}
