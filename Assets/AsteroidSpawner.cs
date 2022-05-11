using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroid;
    public float speedLower = -10.0f;
    public float speedUpper = 10.0f;
    public float spawnRate = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnAsteroid", 0, spawnRate);
    }

    private void SpawnAsteroid()
    {
        Quaternion rotation = Random.rotation;
        rotation.x = 0;
        rotation.y = 0;
        GameObject createdAsteroid = Instantiate(asteroid, transform.position, rotation);
        createdAsteroid.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(speedLower, speedUpper), Random.Range(speedLower, speedUpper)));
    }
}
