using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour {

    //------------ Shuriken Blade Part Variables ------------//
    public float bladeSpeed; // the speed the blade will rotate at
    public float yRotate; // the blade will rotate along the y axis
    private float time = 0.0f; // holds the time taken when the game starts



    [SerializeField]
    private float WaitTime; // how long it takes for the blades to switch direction?

    [SerializeField]
    Vector3 Rotation, startBladePos, endBladePos;

    [SerializeField]
    float minX, maxX, offSet; // the min/max x position the blade can move across the pivot point/axle point

    private bool rotateBlade = true; // is the blade rotating??

    [SerializeField]
    [Range(0.1f, 10)] // controls the speed between the ranges shown (min/max numbers)
    private float bladeBackForthSpeed; // variable that controls the speed of the blades back and forth

    private IEnumerator currentCoroutine;

    // Use this for initialization
    void Start()
    {
        Rotation = new Vector3(0, yRotate, 0); // set the rotation of the blade on the 'y axis'

        startBladePos = transform.position; // this is starting position for the blade


        // We minus the gameobject's position from the offset variable on the x axis component
        // This gives us the ability to manipulate the blades position from the axle within a range.
        endBladePos = new Vector3(transform.position.x - offSet, transform.position.y, transform.position.z);



        // The minimum X position will be the blades positions.
        minX = startBladePos.x;
        maxX = endBladePos.x;

        currentCoroutine = MoveBlade();
        StartCoroutine(currentCoroutine);
    }

    // Update is called once per frame
    void Update()
    {
        // if rotateBlade is true then we rotate the blades
        if (rotateBlade)
        {
            RotateShuriken();
        }
    }

    void SwitchDirection()
    {
        float temp = minX; // store variable to minimum x position
        minX = maxX; // initial pos is equals to max pos
        maxX = temp; // end pos is equal to min
    }

    void RotateShuriken()
    {
        this.transform.Rotate(Rotation * bladeSpeed * Time.smoothDeltaTime);
    }

    IEnumerator MoveBlade()
    {
        while (time < 1.0f)
        {
            transform.position = new Vector3(Mathf.Lerp(minX, maxX, time), startBladePos.y, startBladePos.z);
            time += Time.deltaTime * bladeBackForthSpeed;
            yield return 0;
        }

        StopCoroutine(currentCoroutine);
        currentCoroutine = WaitToSpin(MoveBlade());
        StartCoroutine(currentCoroutine);
    }

    IEnumerator WaitToSpin(IEnumerator moveDirection)
    {
        yield return new WaitForSeconds(WaitTime);
        SwitchDirection();
        time = 0.0f;
        StopCoroutine(currentCoroutine);
        currentCoroutine = moveDirection;
        StartCoroutine(currentCoroutine);
    }
}
