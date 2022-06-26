using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private ParticleSystem hitEffect;
    private const float despawnTime = 3.5f;

    private void Awake()
    {
        Destroy(gameObject, despawnTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
            Destroy(gameObject);
    }
}
