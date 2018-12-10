using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetism : MonoBehaviour {

    private GameObject player;
    public float distance;

    [SerializeField]
    private float speedMultiplier;

    bool isFollowing = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        // Get the distance between the collectables position and the players position
        distance = Vector3.Distance(transform.position, player.transform.position);


        // if the collectable is following is true...
        // Then call the function that moves the collectable to the player.
        // Else do nothing...
        if (isFollowing)
        {
            MoveToPlayer();
        }

        // If the distance between the player is less than 10 units
        // Then is following is true
        // And the 'MoveToPlayer' function is called under this condition.
        // It gives space to create the magnet effect to follow the player around and destroy itself on contact
        if (distance < 10)
        {
            isFollowing = true;
        }

    }

    void MoveToPlayer()
    {
        // Moves object to player (with a nice slowdown effect)
        // Get how far you are off from the player's position and 'this' collectables position
        // Normalize this vector to only get a magnitude between 0 and 1 when the player is moving towards the collectable
        // Move to the player via transform.translate. 'toPlayer' times by the speed mulitiplier will give the collectable some speed to follow the player
        // Dividing this means that when the collectable is near the player it will move faster the closer the player is and vice versa
        Vector3 toPlayer = player.transform.position - transform.position;
        toPlayer.Normalize();
        transform.Translate(toPlayer * speedMultiplier / distance);
        Debug.Log("Player is near the collectable");
    }
}
