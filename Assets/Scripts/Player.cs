using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody2D rigidBody;
    private Vector2 mousePos;

    private void Awake()
    {
        rigidBody = FindObjectOfType<Rigidbody2D>();
        mainCamera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        AimTowardsMouse();
        Shooting();
    }

    private void Shooting()
    {

    }

    private void AimTowardsMouse()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = mousePos - rigidBody.position;

        float rotationOffset = 90;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - rotationOffset;

        rigidBody.rotation = angle;
    }
}
