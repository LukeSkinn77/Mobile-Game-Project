using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles_Damager : MonoBehaviour, IDamagerer {

    [SerializeField]
    private int damageDealt;
    public int DamageDealt { get { return damageDealt; } }
}
