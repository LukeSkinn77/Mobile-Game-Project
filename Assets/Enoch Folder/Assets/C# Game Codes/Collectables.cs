using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour {

    public GameObject Player;
    public float RotationSpeed;
    public float RotationX;
    public float RotationY;
    public float RotationZ;
    public float FloatingSpeed; // how fast the collectable can bounce up and down
    public float SinAmplitude; // determines how high/low the collectable can go 

    Vector3 Rotation;
    Vector3 OriginalYpos;
    Vector3 SinYPos;

    // Use this for initialization
    void Start ()
    {
        SetFloating();
    }
	
	// Update is called once per frame
	void Update ()
    {
        RotateCollectable();
        FloatCollectable();
    }

    void SetFloating()
    {
        OriginalYpos = transform.position;
        Rotation = new Vector3(RotationX, RotationY, RotationZ);
    }

    void FloatCollectable()
    {
        SinYPos = OriginalYpos; // make this variable now the transform.position
        SinYPos.y += Mathf.Sin(Time.fixedTime * FloatingSpeed) * SinAmplitude; // Manipulate the y axis with sin function
        transform.position = SinYPos;
    }

    void RotateCollectable()
    {
        transform.Rotate(Rotation * Time.deltaTime * RotationSpeed);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            Debug.Log("Cube Absorbed");
            Destroy(this.gameObject);
        }
    }
}
