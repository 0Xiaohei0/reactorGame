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
        GameObject createdAsteroid = Instantiate(asteroid, transform.position, transform.rotation);
        createdAsteroid.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(speedLower, speedUpper), Random.Range(speedLower, speedUpper)));
    }
}
