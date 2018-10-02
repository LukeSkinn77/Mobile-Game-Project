using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomEnemies : MonoBehaviour {

    public GameObject Player;
    public GameObject TriggerZoneEnter;
    public GameObject TriggerZoneExit;
    public GameObject[] SpawnEnemies;
    public Transform SpawnPoint;
    private Vector3 RandomPosition;
    private int RandomHolder;
    public int EnemySpawnTime;
    private float EnemySpeedFactor;
    public float EnemySpeed;
    private bool isEnteredTriggerZone = false;
    private bool isExitedTriggerZone = false;
 

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject == TriggerZoneEnter) && (isEnteredTriggerZone = true))
        {
            Debug.Log("Entered Trigger Zone");
            InvokeRepeating("SpawnEnemy", 1, EnemySpawnTime); // start from 1 second then repeat the function every 3 seconds
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject == TriggerZoneExit) && (isExitedTriggerZone = true))
        {
            Debug.Log("Exited Trigger Zone");
            CancelInvoke(); // stop spawning enemies
        }
    }


    void SpawnEnemy()
    {
            // Doing this equation helps the artists to type a smaller value
            // When adding force to a game object
            // Value of this equation is 1000 * EnemySpeed
            // So 50 * 1000 = 50000 units of force which is acceptable for example...
            // Instead of putting '50000' in the editor which can be frustrating...
            EnemySpeedFactor = (1.0f / 0.001f) * EnemySpeed;

            // Variable to control instantiation between the 'x axis' and the 'z axis'
            Vector3 RandomPosition = new Vector3(Random.Range(-5.0f, 5.0f), 0, Random.Range(0, 10.0f));

            // Instantiating Random Gameobjects and adding a force each time in the array 
            // In the forward vector 
            RandomHolder = Random.Range(0, SpawnEnemies.Length); 
            var Enemy = Instantiate(SpawnEnemies[RandomHolder], SpawnPoint.position + RandomPosition, SpawnPoint.rotation);
            Enemy.GetComponent<Rigidbody>().AddForce((transform.forward * -EnemySpeedFactor));
    }

}
