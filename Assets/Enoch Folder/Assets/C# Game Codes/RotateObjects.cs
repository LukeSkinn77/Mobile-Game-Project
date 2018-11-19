using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjects : MonoBehaviour {

    public float rotationSpeed;
    public float rotationX;
    public float rotationY;
    public float rotationZ;
    Vector3 Rotation;

    void Start()
    {
        Rotation = new Vector3(rotationX, rotationY, rotationZ);
    }

    // Update is called once per frame
    void Update ()
    {
        RotateCollectable();
    }

    void RotateCollectable()
    {
        transform.Rotate(Rotation * Time.deltaTime * rotationSpeed);
    }
}
