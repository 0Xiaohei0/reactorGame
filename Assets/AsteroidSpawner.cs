using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public Planet planet;
    public GameObject asteroid;
    public GameObject asteroidSmall;
    public GameObject asteroidLarge;
    public float speedLower;
    public float speedUpper;
    private static float spawnInterval = 0.3f;
    private float spawntimer;
    public float spawnRange = 10f;

    public static float SpawnRate { get => spawnInterval; set => spawnInterval = value; }

    // Start is called before the first frame update
    void Start()
    {
        spawntimer = spawnInterval;
        planet = FindObjectOfType<Planet>();
    }
    private void Update()
    {
        spawntimer -= Time.deltaTime;
        if (spawntimer < 0)
        {
            SpawnAsteroid();
            spawntimer = spawnInterval;
        }
    }
    private void SpawnAsteroid()
    {


        GameObject objectToSpawn = asteroid;

        float randomValue = Random.value;
        if (randomValue < planet.SmallSpawn)
        {
            objectToSpawn = asteroidSmall;
        }
        else
        {
            randomValue -= planet.SmallSpawn;
            if (randomValue < planet.MediumSpawn)
            {
                objectToSpawn = asteroid;
            }
            else
            {
                objectToSpawn = asteroidLarge;
            }
        }

        for (int i = 0; i < objectToSpawn.GetComponent<Asteroid>().spawnQuantity; i++)
        {
            Quaternion rotation = Random.rotation;
            rotation.x = 0;
            rotation.y = 0;
            speedLower = objectToSpawn.GetComponent<Asteroid>().speedLower;
            speedUpper = objectToSpawn.GetComponent<Asteroid>().speedUpper;
            int randomSign = 1;
            if (Random.value < 0.5)
                randomSign = -1;
            Vector3 spawnLoaction = new Vector3((Random.value * 2 - 1) * spawnRange, (Random.value * 2 - 1) * spawnRange, 0);
            Vector2 initialVelocity = new Vector2(randomSign * Random.Range(speedLower, speedUpper), randomSign * Random.Range(speedLower, speedUpper));
            if (objectToSpawn == asteroidSmall)
            {
                //initialVelocity = planet.transform.position - transform.position;
            }
            GameObject createdAsteroid = Instantiate(objectToSpawn, transform.position + spawnLoaction, rotation);
            createdAsteroid.GetComponent<Rigidbody2D>().AddForce(initialVelocity);
        }
    }
}
