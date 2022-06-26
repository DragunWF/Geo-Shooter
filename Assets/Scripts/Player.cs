using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 145.5f;
    private Vector2 rawInputMovement;
    private float rawInputFire;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = FindObjectOfType<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {

    }
}
