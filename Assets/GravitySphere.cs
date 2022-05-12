using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySphere : MonoBehaviour
{
    [SerializeField] private List<Collider2D> colliders = new List<Collider2D>();
    private void OnTriggerStay2D(Collider2D collision)
    {

    }
}
