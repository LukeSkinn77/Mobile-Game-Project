using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_ground_control : MonoBehaviour {

    //Vector3 rayheight = new Vector3(0.0f, 0.1f, 0.0f);
    //LineRenderer line;
    public bool ground;

	// Use this for initialization
	void Start ()
    {
       // line = GetComponent<LineRenderer>();
	}

    void FixedUpdate()
    {
        RaycastHit rhit;

		if (Physics.CapsuleCast(transform.position, new Vector3(transform.position.x, transform.position.y/* - 0.1f*/, transform.position.z), 0.1f, -transform.up, out rhit, 1.0f))
        {
            if (rhit.transform.gameObject.tag == "Terrain")
            {
                ground = true;
            }
        }
        else
        {
            ground = false;
        }
    }
}
