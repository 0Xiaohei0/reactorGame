using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Collider2D asteroidCollider;
    public int damage = 3;
    public int mineralContent = 10;

    private void OnEnable()
    {
        mineralContent = Random.Range(8, 15);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bound")
        {
            Destroy(gameObject);
        }
    }
}
