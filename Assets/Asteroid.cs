using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Collider2D asteroidCollider;
    public GameObject explosion;

    public int damage = 3;
    public int mineralContent = 10;
    public int mineralContentLow = 8;
    public int mineralContentHigh = 15;
    public float speedLower = 5.0f;
    public float speedUpper = 10.0f;
    public int spawnQuantity = 3;

    private void OnEnable()
    {
        mineralContent = Random.Range(mineralContentLow, mineralContentHigh);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DestoryCleanup();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bound")
        {
            DestoryCleanup();
        }
    }

    public void DestoryCleanup()
    {
        gameObject.SetActive(false);
        GameObject explosionInstance = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(explosionInstance, 0.5f);
        Destroy(gameObject, 2f);
    }
}
