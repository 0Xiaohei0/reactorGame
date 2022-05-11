using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Collider2D asteroidCollider;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggerenter");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Triggerexit");
        Debug.Log(collision.gameObject.tag == "Bound");
        if (collision.gameObject.tag == "Bound")
        {
            Destroy(gameObject);
        }
    }
}
