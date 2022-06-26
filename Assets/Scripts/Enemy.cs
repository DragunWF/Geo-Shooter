using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 50;
    [SerializeField] float moveSpeed = 0.5f;
    [SerializeField] bool isCircle = false;

    private GameInfo gameInfo;
    private Player player;

    private void Awake()
    {
        gameInfo = FindObjectOfType<GameInfo>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        Vector2 playerPos = new Vector2(0, 0);
        transform.position = Vector2.MoveTowards(transform.position, playerPos, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
            TakeDamage();
    }

    private void TakeDamage()
    {
        health -= player.BulletDamage;
        if (health <= 0)
            Destroy(gameObject);
    }
}