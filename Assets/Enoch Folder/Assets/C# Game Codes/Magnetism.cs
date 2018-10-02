using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetism : MonoBehaviour {

    public Collectables CollectablesCode;
    public Transform Player;
    public float Speed;
    public float Distance;
    private Vector3 velocity = Vector3.zero;

    // Use this for initialization
    void Start ()
    {
        //ThisTransform = transform.position;
        ///Collectable = transform; // attach the collectable gameobject to this transform.
	}
	
	// Update is called once per frame
	void Update ()
    {
        MoveToPlayer();

        //ThisTransform = Vector3.Lerp(ThisTransform, Player.position, Speed);
        //if(Mathf.Abs(Player.position.x - ThisTransform.x) < 0.05)
        //{
        //   Player.position = ThisTransform;
        //}

        //transform.position = Vector3.Lerp(transform.position, Player.position, Speed);
    }

    void MoveToPlayer()
    {
        Distance = Vector3.Distance(transform.position, Player.transform.position);
        if (Distance < 5)
        {
            CollectablesCode.RotationSpeed = 0;
            CollectablesCode.SinAmplitude = 0;
            Debug.Log("Player is near the collectable");
            //transform.position = Vector3.Lerp(transform.position, Player.position, Speed);

            Vector3 RandomPosition = new Vector3(Random.Range(-5.0f, 5.0f), 0, Random.Range(0, 5.0f));
            //transform.position = Vector3.Slerp(transform.position + RandomPosition, Player.position, Speed * Time.smoothDeltaTime);
            transform.position = Vector3.SmoothDamp(transform.position, Player.position, ref velocity, Speed);
            //CancelInvoke("")
        }
    }
}
