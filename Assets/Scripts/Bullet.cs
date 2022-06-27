using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float despawnTime = 3.5f;

    private void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        GameInfo gameInfo = FindObjectOfType<GameInfo>();
        if (gameInfo.FactionChosen == "RED")
            spriteRenderer.color = new Color32(245, 75, 75, 255);
        else
            spriteRenderer.color = new Color32(107, 178, 238, 255);

        Destroy(gameObject, despawnTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            ParticlesPlayer particlesPlayer = FindObjectOfType<ParticlesPlayer>();
            particlesPlayer.PlayHitEffect(transform.position);
            Destroy(gameObject);
        }
    }
}
