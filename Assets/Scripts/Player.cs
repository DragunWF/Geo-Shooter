using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private const float speed = 145.5f;
    private Vector2 rawInput;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = FindObjectOfType<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    private void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    private void OnFire(InputValue value)
    {

    }

    private void Move()
    {
        float rotationSpeed = rawInput.x * speed * Time.deltaTime;
        transform.Rotate(0, 0, rotationSpeed);
    }
}
