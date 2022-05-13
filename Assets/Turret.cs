using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private float fireRate = 1f;
    public float laserDuration = 0.3f;
    public bool turretReady = true;
    [SerializeField] private List<Collider2D> colliders = new List<Collider2D>();
    [SerializeField] Collider2D currentTarget;
    LineRenderer lineRenderer;

    public Planet planet;

    public float FireRate { get => fireRate; set { fireRate = value; } }

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        FireRate = 1f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!colliders.Contains(collision) && collision.gameObject.tag == "Asteroid") { colliders.Add(collision); }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        colliders.Remove(collision);
    }

    private void Update()
    {
        if (currentTarget != null)
        {
            lineRenderer.SetPosition(1, currentTarget.gameObject.transform.position);
        }
        if (turretReady)
        {
            shoot();
        }
    }

    private void shoot()
    {
        if (colliders.Count != 0)
        {
            for (int i = colliders.Count - 1; i >= 0; i--)
            {
                if (colliders[i] == null)
                {
                    colliders[i] = colliders[colliders.Count - 1];
                    colliders.RemoveAt(colliders.Count - 1);
                }
            }

            colliders.Sort(delegate (Collider2D a, Collider2D b)
            {
                return Vector2.Distance(this.transform.position, a.gameObject.transform.position)
                .CompareTo(
                  Vector2.Distance(this.transform.position, b.gameObject.transform.position));
            });
            currentTarget = colliders[0];
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, colliders[0].transform.position);
            lineRenderer.enabled = true;
            Invoke("laserHit", laserDuration);
            turretReady = false;
            Invoke("chargeTurret", FireRate);
        }
    }

    private void chargeTurret()
    {
        turretReady = true;
    }
    private void laserHit()
    {
        lineRenderer.enabled = false;
        if (currentTarget != null)
        {
            planet.Mineral += currentTarget.gameObject.GetComponent<Asteroid>().mineralContent;
            Destroy(currentTarget.gameObject);
        }
        if (colliders.Contains(currentTarget)) { colliders.Remove(currentTarget); }

    }
}
