using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float moveSpeed = 0.5f;
    private GameInfo gameInfo;
    private Player player;

    private void Awake()
    {
        gameInfo = FindObjectOfType<GameInfo>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        Vector2 playerPos = player.GetPosition();
        transform.position = Vector2.MoveTowards(transform.position, playerPos, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
            TakeDamage();
    }

    private void TakeDamage()
    {

    }
}
