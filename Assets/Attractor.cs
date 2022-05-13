using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{

    const float G = 0.5f;

    public static List<Attractor> Attractors;

    public Rigidbody2D rb;

    void FixedUpdate()
    {
        if (tag == "Player")
            foreach (Attractor attractor in Attractors)
            {
                if (attractor != this)
                    Attract(attractor);
            }
    }

    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        if (Attractors == null)
            Attractors = new List<Attractor>();
        if (tag != "Player")
            Attractors.Add(this);
    }

    void OnDisable()
    {
        Attractors.Remove(this);
    }

    void Attract(Attractor objToAttract)
    {
        Rigidbody2D rbToAttract = objToAttract.rb;

        Vector2 direction = rb.position - rbToAttract.position;
        float distance = direction.sqrMagnitude;

        if (distance == 0f)
            return;

        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / distance;
        Vector3 force = direction.normalized * forceMagnitude;

        rbToAttract.AddForce(force);
    }

}
