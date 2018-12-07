using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObjects : MonoBehaviour {

    public float floatingSpeed; // how fast the collectable can bounce up and down
    public float sinAmplitude; // determines how high/low the collectable can go 
    Vector3 originalYPos; // the starting position in the 'y axis' position
    Vector3 sinYPos; // the sin position that we would use manipulating the y position to create the bobbing effect

    // Use this for initialization
    void Start ()
    {
        SetFloating();
    }
	
	// Update is called once per frame
	void Update ()
    {
        FloatCollectable();
    }

    void SetFloating()
    {
        originalYPos = transform.position; // set the original y position to the transform of this gameobject
    }

    void FloatCollectable()
    {
        sinYPos = originalYPos; // assign the originalypos to the sinypos variable. Now is the transform.position
        sinYPos.y += Mathf.Sin(Time.fixedTime * floatingSpeed) * sinAmplitude; // Manipulate the y axis with sin function
        transform.position = sinYPos; // make the transfrom position the math calculation of the sinYpos to create the bobbing effect.
    }
}
